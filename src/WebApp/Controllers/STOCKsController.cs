using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using Z.EntityFramework.Plus;
using TrackableEntities;
using WebApp.Models;
using WebApp.Services;
using WebApp.Repositories;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
/// <summary>
/// File: STOCKsController.cs
/// Purpose:库存作业/库存管理
/// Created Date: 2019/6/26 13:33:40
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<STOCK>, Repository<STOCK>>();
///    container.RegisterType<ISTOCKService, STOCKService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("STOCKs")]
	public class STOCKsController : Controller
	{
		private readonly ISTOCKService  sTOCKService;
		private readonly IUnitOfWorkAsync unitOfWork;
		public STOCKsController (ISTOCKService  sTOCKService, IUnitOfWorkAsync unitOfWork)
		{
			this.sTOCKService  = sTOCKService;
			this.unitOfWork = unitOfWork;
		}
        		//GET: STOCKs/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "库存管理 ", Order = 1)]
		public ActionResult Index() => this.View();
    [Route("Qc", Name = "QC操作", Order = 2)]
    public ActionResult Qc() => this.View();

    //获取检查数据
    [HttpGet]
    public async Task<JsonResult> GetQCData(string suppilercode,string po, string extkey, string sku, DateTime dt1, DateTime dt2, string status) {
      var query = this.sTOCKService.Queryable()
        .Where(x => DbFunctions.TruncateTime(x.RECEIPTDATE) >= dt1 &&
        DbFunctions.TruncateTime(x.RECEIPTDATE) <= dt2 
        );
      if (!string.IsNullOrEmpty(po))
      {
        query = query.Where(x => x.POKEY.Contains(po));
      }
      if (!string.IsNullOrEmpty(suppilercode))
      {
        query = query.Where(x => x.SUPPLIERCODE== suppilercode);
      }
      if (string.IsNullOrEmpty(status))
      {
        var array = new string[] { "104", "105", "103" };
        query = query.Where(x => array.Contains(x.STATUS));
      }
      else
      {
        query = query.Where(x =>  x.STATUS==status);
      }
      if (!string.IsNullOrEmpty(extkey))
      {
        query = query.Where(x => x.EXTERNRECEIPTKEY.Contains(extkey));
      }
      if (!string.IsNullOrEmpty(sku))
      {
        query = query.Where(x => x.SKU.Contains(sku));
      }
      var data = ( await query.OrderByDescending(x => x.INPUTDATETIME).ToListAsync() ).Select(n => new
      {
        ID = n.ID,
        WHSEID = n.WHSEID,
        LOT = n.LOT,
        EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
        POKEY = n.POKEY,
        STORERKEY = n.STORERKEY,
        RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
        LOC = n.LOC,
        LPN = n.LPN,
        LOTTABLE3 = n.LOTTABLE3,
        STATUS = n.STATUS,
        SKU = n.SKU,
        SKUNAME = n.SKUNAME,
        QTY = n.QTY,
        PQCQTYINSPECTED =(n.PQCQTYINSPECTED==0? n.QTY: n.PQCQTYINSPECTED),
        UMO = n.UMO,
        CASECNT = n.CASECNT,
        PALLET = n.PALLET,
        PACKKEY = n.PACKKEY,
        GROSSWGT = n.GROSSWGT,
        NETWGT = n.NETWGT,
        CUBE = n.CUBE,
        RECEIPTKEY = n.RECEIPTKEY,
        ALTSKU = n.ALTSKU,
        SUPPLIERCODE = n.SUPPLIERCODE,
        SUPPLIERNAME = n.SUPPLIERNAME,
        INPUTDATETIME = n.INPUTDATETIME.ToString("yyyy/MM/dd HH:mm:ss"),
        EXTERNORDERKEY = n.EXTERNORDERKEY,
        PICKDETAILKEY = n.PICKDETAILKEY,
        ORDERKEY = n.ORDERKEY,
        OUTPUTDATETIME = n.OUTPUTDATETIME?.ToString("yyyy/MM/dd HH:mm:ss"),
        NOTES = n.NOTES,
        LOTTABLE1 = n.LOTTABLE1,
        LOTTABLE2 = n.LOTTABLE2,
        LOTTABLE4 = n.LOTTABLE4,
        LOTTABLE5 = n.LOTTABLE5,
        LOTTABLE6 = n.LOTTABLE6,
        LOTTABLE7 = n.LOTTABLE7,
        LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
        LOTTABLE9 = n.LOTTABLE9,
        LOTTABLE10 = n.LOTTABLE10,
        LOTTABLE11 = n.LOTTABLE11,
        LOTTABLE12 = n.LOTTABLE12
      });
      return Json(data, JsonRequestBehavior.AllowGet);


    }

    //更新状态(合格/不合格)
    [HttpPost]
    public async Task<JsonResult> ChangeSate(POSTQCQTY[] data, string state)
    {
      try
      {
        this.sTOCKService.ChangeSate(data, state, this.User.Identity.Name);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get :STOCKs/GetData
    //For Index View datagrid datasource url
    [HttpGet]
		 public async Task<JsonResult> GetDataAsync(int page = 1, int rows = 10, string sort = "ID", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.sTOCKService
						               .Query(new STOCKQuery().Withfilter(filters))
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ID = n.ID,
    WHSEID = n.WHSEID,
    LOT = n.LOT,
    EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
    POKEY = n.POKEY,
    STORERKEY = n.STORERKEY,
    RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
    LOC = n.LOC,
    LPN = n.LPN,
    LOTTABLE3 = n.LOTTABLE3,
    STATUS = n.STATUS,
    SKU = n.SKU,
    SKUNAME = n.SKUNAME,
    QTY = n.QTY,
    UMO = n.UMO,
    CASECNT = n.CASECNT,
    PALLET = n.PALLET,
    PACKKEY = n.PACKKEY,
    GROSSWGT = n.GROSSWGT,
    NETWGT = n.NETWGT,
    CUBE = n.CUBE,
    RECEIPTKEY = n.RECEIPTKEY,
    ALTSKU = n.ALTSKU,
    SUPPLIERCODE = n.SUPPLIERCODE,
    SUPPLIERNAME = n.SUPPLIERNAME,
    INPUTDATETIME = n.INPUTDATETIME.ToString("yyyy/MM/dd HH:mm:ss"),
    EXTERNORDERKEY = n.EXTERNORDERKEY,
    PICKDETAILKEY = n.PICKDETAILKEY,
    ORDERKEY = n.ORDERKEY,
    OUTPUTDATETIME = n.OUTPUTDATETIME?.ToString("yyyy/MM/dd HH:mm:ss"),
    NOTES = n.NOTES,
    LOTTABLE1 = n.LOTTABLE1,
    LOTTABLE2 = n.LOTTABLE2,
    LOTTABLE4 = n.LOTTABLE4,
    LOTTABLE5 = n.LOTTABLE5,
    LOTTABLE6 = n.LOTTABLE6,
    LOTTABLE7 = n.LOTTABLE7,
    LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
    LOTTABLE9 = n.LOTTABLE9,
    LOTTABLE10 = n.LOTTABLE10,
    LOTTABLE11 = n.LOTTABLE11,
    LOTTABLE12 = n.LOTTABLE12
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> SaveDataAsync(STOCK[] stocks)
		{
            if (stocks == null)
            {
                throw new ArgumentNullException(nameof(stocks));
            }
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in stocks)
               {
                 this.sTOCKService.ApplyChanges(item);
               }
			   var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
                 return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
                }
		    }
            else
            {
                var modelStateErrors = string.Join(",", ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
        
        }
						//GET: STOCKs/Details/:id
		public ActionResult Details(int id)
		{
			
			var sTOCK = this.sTOCKService.Find(id);
			if (sTOCK == null)
			{
				return HttpNotFound();
			}
			return View(sTOCK);
		}
        //GET: STOCKs/GetItemAsync/:id
        [HttpGet]
        public async Task<JsonResult> GetItemAsync(int id) {
            var  sTOCK = await this.sTOCKService.FindAsync(id);
            return Json(sTOCK,JsonRequestBehavior.AllowGet);
        }
		//GET: STOCKs/Create
        		public ActionResult Create()
				{
			var sTOCK = new STOCK();
			//set default value
			return View(sTOCK);
		}
		//POST: STOCKs/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(STOCK sTOCK)
		{
			if (sTOCK == null)
            {
                throw new ArgumentNullException(nameof(sTOCK));
            } 
            if (ModelState.IsValid)
			{
                try{ 
				this.sTOCKService.Insert(sTOCK);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                   var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
                   return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a sTOCK record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//return View(sTOCK);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItemAsync() {
            var sTOCK = await Task.Run(() => {
                return new STOCK();
                });
            return Json(sTOCK, JsonRequestBehavior.AllowGet);
        }

         
		//GET: STOCKs/Edit/:id
		public ActionResult Edit(int id)
		{
			var sTOCK = this.sTOCKService.Find(id);
			if (sTOCK == null)
			{
				return HttpNotFound();
			}
			return View(sTOCK);
		}
		//POST: STOCKs/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(STOCK sTOCK)
		{
            if (sTOCK == null)
            {
                throw new ArgumentNullException(nameof(sTOCK));
            }
			if (ModelState.IsValid)
			{
				sTOCK.TrackingState = TrackingState.Modified;
				                try{
				this.sTOCKService.Update(sTOCK);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
                    return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a STOCK record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//return View(sTOCK);
		}
        //删除当前记录
		//GET: STOCKs/Delete/:id
        [HttpGet]
		public async Task<ActionResult> DeleteAsync(int id)
		{
          try{
               await this.sTOCKService.Queryable().Where(x => x.ID == id).DeleteAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (System.Data.Entity.Validation.DbEntityValidationException e)
           {
                var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
                return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
           }
		}
		 
       
 

        //删除选中的记录
        [HttpPost]
        public async Task<JsonResult> DeleteCheckedAsync(int[] id) {
           if (id == null)
           {
                throw new ArgumentNullException(nameof(id));
           }
           try{
               await this.sTOCKService.Queryable().Where(x => id.Contains(x.ID)).DeleteAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (System.Data.Entity.Validation.DbEntityValidationException e)
           {
                    var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
                    return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                    return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
           }
        }
		//导出Excel
		[HttpPost]
		public ActionResult ExportExcel( string filterRules = "",string sort = "ID", string order = "asc")
		{
			var fileName = "stocks_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  this.sTOCKService.ExportExcel(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
