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
namespace WebApp.Controllers
{
/// <summary>
/// File: PICKDETAILsController.cs
/// Purpose:出库作业/拣货明细
/// Created Date: 2019/6/27 8:22:36
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<PICKDETAIL>, Repository<PICKDETAIL>>();
///    container.RegisterType<IPICKDETAILService, PICKDETAILService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("PICKDETAILs")]
	public class PICKDETAILsController : Controller
	{
		private readonly IPICKDETAILService  pICKDETAILService;
		private readonly IUnitOfWorkAsync unitOfWork;
		public PICKDETAILsController (IPICKDETAILService  pICKDETAILService, IUnitOfWorkAsync unitOfWork)
		{
			this.pICKDETAILService  = pICKDETAILService;
			this.unitOfWork = unitOfWork;
		}
        		//GET: PICKDETAILs/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "拣货明细 ", Order = 1)]
		public ActionResult Index() => this.View();


    [HttpGet]
    public async Task<JsonResult> GetDataByID(int id)
    {
      var data = ( await this.pICKDETAILService.Queryable().Where(x => x.ORDERID == id
                   && x.STATUS != "108"
      )
          .ToListAsync() )
          .Select(n => new
          {
            ORDERORDERKEY = n.ORDER?.ORDERKEY,
            ID = n.ID,
            PICKDETAILKEY = n.PICKDETAILKEY,
            WHSEID = n.WHSEID,
            ORDERKEY = n.ORDERKEY,
            ORDERLINENUMBER = n.ORDERLINENUMBER,
            EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
            STORERKEY = n.STORERKEY,
            STATUS = n.STATUS,
            SKU = n.SKU,
            SKUNAME = n.SKUNAME,
            QTY = n.QTY,
            UMO = n.UMO,
            CASECNT = n.CASECNT,
            LOC = n.LOC,
            LOT = n.LOT,
            LPN = n.LPN,
            SUPPLIERCODE = n.SUPPLIERCODE,
            SUPPLIERNAME = n.SUPPLIERNAME,
            RECEIPTKEY = n.RECEIPTKEY,
            POKEY = n.POKEY,
            RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
            PALLET = n.PALLET,
            PACKKEY = n.PACKKEY,
            GROSSWGT = n.GROSSWGT,
            NETWGT = n.NETWGT,
            CUBE = n.CUBE,
            LOTTABLE1 = n.LOTTABLE1,
            LOTTABLE2 = n.LOTTABLE2,
            LOTTABLE3 = n.LOTTABLE3,
            LOTTABLE4 = n.LOTTABLE4,
            LOTTABLE5 = n.LOTTABLE5,
            LOTTABLE6 = n.LOTTABLE6,
            LOTTABLE7 = n.LOTTABLE7,
            LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
            ORDERID = n.ORDERID
          }).ToList();

      return Json(data, JsonRequestBehavior.AllowGet);
    }
        //Get :PICKDETAILs/GetData
        //For Index View datagrid datasource url
    [HttpGet]
		 public async Task<JsonResult> GetDataAsync(int page = 1, int rows = 10, string sort = "ID", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.pICKDETAILService
						               .Query(new PICKDETAILQuery().Withfilter(filters)).Include(p => p.ORDER)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ORDERORDERKEY = n.ORDER?.ORDERKEY,
    ID = n.ID,
    PICKDETAILKEY = n.PICKDETAILKEY,
    WHSEID = n.WHSEID,
    ORDERKEY = n.ORDERKEY,
                                         ORDERLINENUMBER=n.ORDERLINENUMBER,
    EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
    STORERKEY = n.STORERKEY,
    STATUS = n.STATUS,
    SKU = n.SKU,
    SKUNAME = n.SKUNAME,
    QTY = n.QTY,
    UMO = n.UMO,
    CASECNT = n.CASECNT,
    LOC = n.LOC,
    LOT = n.LOT,
    LPN = n.LPN,
    SUPPLIERCODE = n.SUPPLIERCODE,
    SUPPLIERNAME = n.SUPPLIERNAME,
    RECEIPTKEY = n.RECEIPTKEY,
    POKEY = n.POKEY,
    RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
    PALLET = n.PALLET,
    PACKKEY = n.PACKKEY,
    GROSSWGT = n.GROSSWGT,
    NETWGT = n.NETWGT,
    CUBE = n.CUBE,
    LOTTABLE1 = n.LOTTABLE1,
    LOTTABLE2 = n.LOTTABLE2,
    LOTTABLE3 = n.LOTTABLE3,
    LOTTABLE4 = n.LOTTABLE4,
    LOTTABLE5 = n.LOTTABLE5,
    LOTTABLE6 = n.LOTTABLE6,
    LOTTABLE7 = n.LOTTABLE7,
    LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
    ORDERID = n.ORDERID
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        public async Task<JsonResult> GetDataByORDERIDAsync (int  orderid ,int page = 1, int rows = 10, string sort = "ID", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.pICKDETAILService
						               .Query(new PICKDETAILQuery().ByORDERIDWithfilter(orderid,filters)).Include(p => p.ORDER)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ORDERORDERKEY = n.ORDER?.ORDERKEY,
    ID = n.ID,
    PICKDETAILKEY = n.PICKDETAILKEY,
    WHSEID = n.WHSEID,
    ORDERKEY = n.ORDERKEY,
                                         ORDERLINENUMBER = n.ORDERLINENUMBER,
                                         EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
    STORERKEY = n.STORERKEY,
    STATUS = n.STATUS,
    SKU = n.SKU,
    SKUNAME = n.SKUNAME,
    QTY = n.QTY,
    UMO = n.UMO,
    CASECNT = n.CASECNT,
    LOC = n.LOC,
    LOT = n.LOT,
    LPN = n.LPN,
    SUPPLIERCODE = n.SUPPLIERCODE,
    SUPPLIERNAME = n.SUPPLIERNAME,
    RECEIPTKEY = n.RECEIPTKEY,
    POKEY = n.POKEY,
    RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
    PALLET = n.PALLET,
    PACKKEY = n.PACKKEY,
    GROSSWGT = n.GROSSWGT,
    NETWGT = n.NETWGT,
    CUBE = n.CUBE,
    LOTTABLE1 = n.LOTTABLE1,
    LOTTABLE2 = n.LOTTABLE2,
    LOTTABLE3 = n.LOTTABLE3,
    LOTTABLE4 = n.LOTTABLE4,
    LOTTABLE5 = n.LOTTABLE5,
    LOTTABLE6 = n.LOTTABLE6,
    LOTTABLE7 = n.LOTTABLE7,
    LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
    ORDERID = n.ORDERID
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> SaveDataAsync(PICKDETAIL[] pickdetails)
		{
            if (pickdetails == null)
            {
                throw new ArgumentNullException(nameof(pickdetails));
            }
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in pickdetails)
               {
                 this.pICKDETAILService.ApplyChanges(item);
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
				        //[OutputCache(Duration = 20, VaryByParam = "q")]
		public async Task<JsonResult> GetORDERSAsync(string q="")
		{
			var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
			var rows = await orderRepository
                            .Queryable()
                            .Where(n=>n.ORDERKEY.Contains(q))
                            .OrderBy(n=>n.ORDERKEY)
                            .Select(n => new { ID = n.ID, ORDERKEY = n.ORDERKEY })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
								//GET: PICKDETAILs/Details/:id
		public ActionResult Details(int id)
		{
			
			var pICKDETAIL = this.pICKDETAILService.Find(id);
			if (pICKDETAIL == null)
			{
				return HttpNotFound();
			}
			return View(pICKDETAIL);
		}
        //GET: PICKDETAILs/GetItemAsync/:id
        [HttpGet]
        public async Task<JsonResult> GetItemAsync(int id) {
            var  pICKDETAIL = await this.pICKDETAILService.FindAsync(id);
            return Json(pICKDETAIL,JsonRequestBehavior.AllowGet);
        }
		//GET: PICKDETAILs/Create
        		public ActionResult Create()
				{
			var pICKDETAIL = new PICKDETAIL();
			//set default value
			var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
		   			ViewBag.ORDERID = new SelectList(orderRepository.Queryable().OrderBy(n=>n.ORDERKEY), "ID", "ORDERKEY");
		   			return View(pICKDETAIL);
		}
		//POST: PICKDETAILs/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(PICKDETAIL pICKDETAIL)
		{
			if (pICKDETAIL == null)
            {
                throw new ArgumentNullException(nameof(pICKDETAIL));
            } 
            if (ModelState.IsValid)
			{
                try{ 
				this.pICKDETAILService.Insert(pICKDETAIL);
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
			    //DisplaySuccessMessage("Has update a pICKDETAIL record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
			//ViewBag.ORDERID = new SelectList(await orderRepository.Queryable().OrderBy(n=>n.ORDERKEY).ToListAsync(), "ID", "ORDERKEY", pICKDETAIL.ORDERID);
			//return View(pICKDETAIL);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItemAsync() {
            var pICKDETAIL = await Task.Run(() => {
                return new PICKDETAIL();
                });
            return Json(pICKDETAIL, JsonRequestBehavior.AllowGet);
        }

         
		//GET: PICKDETAILs/Edit/:id
		public ActionResult Edit(int id)
		{
			var pICKDETAIL = this.pICKDETAILService.Find(id);
			if (pICKDETAIL == null)
			{
				return HttpNotFound();
			}
			var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
			ViewBag.ORDERID = new SelectList(orderRepository.Queryable().OrderBy(n=>n.ORDERKEY), "ID", "ORDERKEY", pICKDETAIL.ORDERID);
			return View(pICKDETAIL);
		}
		//POST: PICKDETAILs/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(PICKDETAIL pICKDETAIL)
		{
            if (pICKDETAIL == null)
            {
                throw new ArgumentNullException(nameof(pICKDETAIL));
            }
			if (ModelState.IsValid)
			{
				pICKDETAIL.TrackingState = TrackingState.Modified;
				                try{
				this.pICKDETAILService.Update(pICKDETAIL);
				                
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
				
				//DisplaySuccessMessage("Has update a PICKDETAIL record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
												//return View(pICKDETAIL);
		}
        //删除当前记录
		//GET: PICKDETAILs/Delete/:id
        [HttpGet]
		public async Task<ActionResult> DeleteAsync(int id)
		{
          try{
               await this.pICKDETAILService.Queryable().Where(x => x.ID == id).DeleteAsync();
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
               await this.pICKDETAILService.Queryable().Where(x => id.Contains(x.ID)).DeleteAsync();
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
			var fileName = "pickdetails_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  this.pICKDETAILService.ExportExcel(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
