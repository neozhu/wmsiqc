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
/// File: RECEIPTDETAILsController.cs
/// Purpose:收货作业/收货单明细
/// Created Date: 2019/6/25 7:29:03
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<RECEIPTDETAIL>, Repository<RECEIPTDETAIL>>();
///    container.RegisterType<IRECEIPTDETAILService, RECEIPTDETAILService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("RECEIPTDETAILs")]
	public class RECEIPTDETAILsController : Controller
	{
		private readonly IRECEIPTDETAILService  rECEIPTDETAILService;
		private readonly IUnitOfWorkAsync unitOfWork;
		public RECEIPTDETAILsController (IRECEIPTDETAILService  rECEIPTDETAILService, IUnitOfWorkAsync unitOfWork)
		{
			this.rECEIPTDETAILService  = rECEIPTDETAILService;
			this.unitOfWork = unitOfWork;
		}
        		//GET: RECEIPTDETAILs/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "收货单明细 ", Order = 1)]
		public ActionResult Index() => this.View();

    //根据批号+日期获取明细数据
    [HttpGet]
    public async Task<JsonResult> GetData100ByID(int id) {
      //var date = DateTime.Parse(dt);
      var list=(await this.rECEIPTDETAILService.Queryable()
          .Where(x=>x.RECEIPTID == id && x.STATUS=="100")
          .ToListAsync())
          .Select(n => new {
            ID = n.ID,
            RECEIPTKEY = n.RECEIPTKEY,
            EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
            POKEY = n.POKEY,
            RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
            WHSEID = n.WHSEID,
            LOTTABLE1 = n.LOTTABLE1,
            LOTTABLE2 = n.LOTTABLE2,
            STORERKEY = n.STORERKEY,
            RECEIPTLINENUMBER = n.RECEIPTLINENUMBER,
            SKU = n.SKU,
            SKUNAME = n.SKUNAME,
            ALTSKU = n.ALTSKU,
            STATUS = n.STATUS,
            SUPPLIERCODE = n.SUPPLIERCODE,
            SUPPLIERNAME = n.SUPPLIERNAME,
            DATERECEIVED = n.DATERECEIVED?.ToString("yyyy/MM/dd HH:mm:ss"),
            QTYEXPECTED = n.QTYEXPECTED,
            CASECNT = n.CASECNT,
            PALLET = n.PALLET,
            QTYADJUSTED = n.QTYADJUSTED,
            QTYREJECTED = n.QTYREJECTED,
            QTYRECEIVED = ((n.LOTTABLE3!="-" && n.LOTTABLE3 != "")? n.QTYEXPECTED:0),
            UMO = n.UMO,
            PACKKEY = n.PACKKEY,
            GROSSWGT = n.GROSSWGT,
            NETWGT = n.NETWGT,
            CUBE = n.CUBE,
            NOTES = n.NOTES,
            LOTTABLE3 = n.LOTTABLE3,
            PQCQTYINSPECTED = n.PQCQTYINSPECTED,
            PQCQTYREJECTED = n.PQCQTYREJECTED,
            PQCSTATUS = n.PQCSTATUS,
            PQCWHO = n.PQCWHO,
            PQCDATE = n.PQCDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
            CONDITIONCODE = n.CONDITIONCODE,
            TOLOC = n.TOLOC,
            TOLOT = n.TOLOT,
            TOLPN = n.TOLPN,
            EXTERNLINENO = n.EXTERNLINENO,
            LOTTABLE4 = n.LOTTABLE4,
            LOTTABLE5 = n.LOTTABLE5,
            LOTTABLE6 = n.LOTTABLE6,
            LOTTABLE7 = n.LOTTABLE7,
            LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
            RECEIPTID = n.RECEIPTID
          }).ToList();
      return Json(list, JsonRequestBehavior.AllowGet);

    }

    [HttpGet]
    public async Task<JsonResult> GetData101ByID(int id)
    {
      //var date = DateTime.Parse(dt);
      //var array = new string[3]{ "103", "104", "105" };
      var list = ( await this.rECEIPTDETAILService.Queryable()
          .Where(x => x.RECEIPTID == id &&  x.STATUS=="101" )
          .ToListAsync() )
          .Select(n => new {
            ID = n.ID,
            RECEIPTKEY = n.RECEIPTKEY,
            EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
            POKEY = n.POKEY,
            RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
            WHSEID = n.WHSEID,
            LOTTABLE1 = n.LOTTABLE1,
            LOTTABLE2 = n.LOTTABLE2,
            STORERKEY = n.STORERKEY,
            RECEIPTLINENUMBER = n.RECEIPTLINENUMBER,
            SKU = n.SKU,
            SKUNAME = n.SKUNAME,
            ALTSKU = n.ALTSKU,
            STATUS = n.STATUS,
            SUPPLIERCODE = n.SUPPLIERCODE,
            SUPPLIERNAME = n.SUPPLIERNAME,
            DATERECEIVED = n.DATERECEIVED?.ToString("yyyy/MM/dd HH:mm:ss"),
            QTYEXPECTED = n.QTYEXPECTED,
            CASECNT = n.CASECNT,
            PALLET = n.PALLET,
            QTYADJUSTED = n.QTYADJUSTED,
            QTYREJECTED = n.QTYREJECTED,
            QTYRECEIVED = n.QTYRECEIVED,
            UMO = n.UMO,
            PACKKEY = n.PACKKEY,
            GROSSWGT = n.GROSSWGT,
            NETWGT = n.NETWGT,
            CUBE = n.CUBE,
            NOTES = n.NOTES,
            LOTTABLE3 = n.LOTTABLE3,
            PQCQTYINSPECTED = n.PQCQTYINSPECTED,
            PQCQTYREJECTED = n.PQCQTYREJECTED,
            PQCSTATUS = n.PQCSTATUS,
            PQCWHO = n.PQCWHO,
            PQCDATE = n.PQCDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
            CONDITIONCODE = n.CONDITIONCODE,
            TOLOC = n.TOLOC,
            TOLOT = n.TOLOT,
            TOLPN = n.TOLPN,
            EXTERNLINENO = n.EXTERNLINENO,
            LOTTABLE4 = n.LOTTABLE4,
            LOTTABLE5 = n.LOTTABLE5,
            LOTTABLE6 = n.LOTTABLE6,
            LOTTABLE7 = n.LOTTABLE7,
            LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
            RECEIPTID = n.RECEIPTID
          }).ToList();
      return Json(list, JsonRequestBehavior.AllowGet);

    }

    //Get :RECEIPTDETAILs/GetData
    //For Index View datagrid datasource url
    [HttpGet]
		 public async Task<JsonResult> GetDataAsync(int page = 1, int rows = 10, string sort = "ID", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.rECEIPTDETAILService
						               .Query(new RECEIPTDETAILQuery().Withfilter(filters)).Include(r => r.RECEIPT)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    RECEIPTRECEIPTKEY = n.RECEIPT?.RECEIPTKEY,
    ID = n.ID,
    RECEIPTKEY = n.RECEIPTKEY,
    EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
    POKEY = n.POKEY,
    RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
    WHSEID = n.WHSEID,
    LOTTABLE1 = n.LOTTABLE1,
    LOTTABLE2 = n.LOTTABLE2,
    STORERKEY = n.STORERKEY,
    RECEIPTLINENUMBER = n.RECEIPTLINENUMBER,
    SKU = n.SKU,
    SKUNAME = n.SKUNAME,
    ALTSKU = n.ALTSKU,
    STATUS = n.STATUS,
    SUPPLIERCODE = n.SUPPLIERCODE,
    SUPPLIERNAME = n.SUPPLIERNAME,
    DATERECEIVED = n.DATERECEIVED?.ToString("yyyy/MM/dd HH:mm:ss"),
    QTYEXPECTED = n.QTYEXPECTED,
    CASECNT = n.CASECNT,
    PALLET = n.PALLET,
    QTYADJUSTED = n.QTYADJUSTED,
    QTYREJECTED = n.QTYREJECTED,
    QTYRECEIVED = n.QTYRECEIVED,
    UMO = n.UMO,
    PACKKEY = n.PACKKEY,
    GROSSWGT = n.GROSSWGT,
    NETWGT = n.NETWGT,
    CUBE = n.CUBE,
    NOTES = n.NOTES,
    LOTTABLE3 = n.LOTTABLE3,
    PQCQTYINSPECTED = n.PQCQTYINSPECTED,
    PQCQTYREJECTED = n.PQCQTYREJECTED,
    PQCSTATUS = n.PQCSTATUS,
    PQCWHO = n.PQCWHO,
    PQCDATE = n.PQCDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
    CONDITIONCODE = n.CONDITIONCODE,
    TOLOC = n.TOLOC,
    TOLOT = n.TOLOT,
    TOLPN = n.TOLPN,
    EXTERNLINENO = n.EXTERNLINENO,
    LOTTABLE4 = n.LOTTABLE4,
    LOTTABLE5 = n.LOTTABLE5,
    LOTTABLE6 = n.LOTTABLE6,
    LOTTABLE7 = n.LOTTABLE7,
    LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
    RECEIPTID = n.RECEIPTID
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        public async Task<JsonResult> GetDataByRECEIPTIDAsync (int  receiptid ,int page = 1, int rows = 10, string sort = "ID", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.rECEIPTDETAILService
						               .Query(new RECEIPTDETAILQuery().ByRECEIPTIDWithfilter(receiptid,filters)).Include(r => r.RECEIPT)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    RECEIPTRECEIPTKEY = n.RECEIPT?.RECEIPTKEY,
    ID = n.ID,
    RECEIPTKEY = n.RECEIPTKEY,
    EXTERNRECEIPTKEY = n.EXTERNRECEIPTKEY,
    POKEY = n.POKEY,
    RECEIPTDATE = n.RECEIPTDATE.ToString("yyyy/MM/dd HH:mm:ss"),
    WHSEID = n.WHSEID,
    LOTTABLE1 = n.LOTTABLE1,
    LOTTABLE2 = n.LOTTABLE2,
    STORERKEY = n.STORERKEY,
    RECEIPTLINENUMBER = n.RECEIPTLINENUMBER,
    SKU = n.SKU,
    SKUNAME = n.SKUNAME,
    ALTSKU = n.ALTSKU,
    STATUS = n.STATUS,
    SUPPLIERCODE = n.SUPPLIERCODE,
    SUPPLIERNAME = n.SUPPLIERNAME,
    DATERECEIVED = n.DATERECEIVED?.ToString("yyyy/MM/dd HH:mm:ss"),
    QTYEXPECTED = n.QTYEXPECTED,
    CASECNT = n.CASECNT,
    PALLET = n.PALLET,
    QTYADJUSTED = n.QTYADJUSTED,
    QTYREJECTED = n.QTYREJECTED,
    QTYRECEIVED = n.QTYRECEIVED,
    UMO = n.UMO,
    PACKKEY = n.PACKKEY,
    GROSSWGT = n.GROSSWGT,
    NETWGT = n.NETWGT,
    CUBE = n.CUBE,
    NOTES = n.NOTES,
    LOTTABLE3 = n.LOTTABLE3,
    PQCQTYINSPECTED = n.PQCQTYINSPECTED,
    PQCQTYREJECTED = n.PQCQTYREJECTED,
    PQCSTATUS = n.PQCSTATUS,
    PQCWHO = n.PQCWHO,
    PQCDATE = n.PQCDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
    CONDITIONCODE = n.CONDITIONCODE,
    TOLOC = n.TOLOC,
    TOLOT = n.TOLOT,
    TOLPN = n.TOLPN,
    EXTERNLINENO = n.EXTERNLINENO,
    LOTTABLE4 = n.LOTTABLE4,
    LOTTABLE5 = n.LOTTABLE5,
    LOTTABLE6 = n.LOTTABLE6,
    LOTTABLE7 = n.LOTTABLE7,
    LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
    RECEIPTID = n.RECEIPTID
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> SaveDataAsync(RECEIPTDETAIL[] receiptdetails)
		{
            if (receiptdetails == null)
            {
                throw new ArgumentNullException(nameof(receiptdetails));
            }
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in receiptdetails)
               {
                 this.rECEIPTDETAILService.ApplyChanges(item);
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
		public async Task<JsonResult> GetRECEIPTSAsync(string q="")
		{
			var receiptRepository = this.unitOfWork.RepositoryAsync<RECEIPT>();
			var rows = await receiptRepository
                            .Queryable()
                            .Where(n=>n.RECEIPTKEY.Contains(q))
                            .OrderBy(n=>n.RECEIPTKEY)
                            .Select(n => new { ID = n.ID, RECEIPTKEY = n.RECEIPTKEY })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
								//GET: RECEIPTDETAILs/Details/:id
		public ActionResult Details(int id)
		{
			
			var rECEIPTDETAIL = this.rECEIPTDETAILService.Find(id);
			if (rECEIPTDETAIL == null)
			{
				return HttpNotFound();
			}
			return View(rECEIPTDETAIL);
		}
        //GET: RECEIPTDETAILs/GetItemAsync/:id
        [HttpGet]
        public async Task<JsonResult> GetItemAsync(int id) {
            var  rECEIPTDETAIL = await this.rECEIPTDETAILService.FindAsync(id);
            return Json(rECEIPTDETAIL,JsonRequestBehavior.AllowGet);
        }
		//GET: RECEIPTDETAILs/Create
        		public ActionResult Create()
				{
			var rECEIPTDETAIL = new RECEIPTDETAIL();
			//set default value
			var receiptRepository = this.unitOfWork.RepositoryAsync<RECEIPT>();
		   			ViewBag.RECEIPTID = new SelectList(receiptRepository.Queryable().OrderBy(n=>n.RECEIPTKEY), "ID", "RECEIPTKEY");
		   			return View(rECEIPTDETAIL);
		}
		//POST: RECEIPTDETAILs/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(RECEIPTDETAIL rECEIPTDETAIL)
		{
			if (rECEIPTDETAIL == null)
            {
                throw new ArgumentNullException(nameof(rECEIPTDETAIL));
            } 
            if (ModelState.IsValid)
			{
                try{ 
				this.rECEIPTDETAILService.Insert(rECEIPTDETAIL);
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
			    //DisplaySuccessMessage("Has update a rECEIPTDETAIL record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var receiptRepository = this.unitOfWork.RepositoryAsync<RECEIPT>();
			//ViewBag.RECEIPTID = new SelectList(await receiptRepository.Queryable().OrderBy(n=>n.RECEIPTKEY).ToListAsync(), "ID", "RECEIPTKEY", rECEIPTDETAIL.RECEIPTID);
			//return View(rECEIPTDETAIL);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItemAsync() {
            var rECEIPTDETAIL = await Task.Run(() => {
                return new RECEIPTDETAIL();
                });
            return Json(rECEIPTDETAIL, JsonRequestBehavior.AllowGet);
        }

         
		//GET: RECEIPTDETAILs/Edit/:id
		public ActionResult Edit(int id)
		{
			var rECEIPTDETAIL = this.rECEIPTDETAILService.Find(id);
			if (rECEIPTDETAIL == null)
			{
				return HttpNotFound();
			}
			var receiptRepository = this.unitOfWork.RepositoryAsync<RECEIPT>();
			ViewBag.RECEIPTID = new SelectList(receiptRepository.Queryable().OrderBy(n=>n.RECEIPTKEY), "ID", "RECEIPTKEY", rECEIPTDETAIL.RECEIPTID);
			return View(rECEIPTDETAIL);
		}
		//POST: RECEIPTDETAILs/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(RECEIPTDETAIL rECEIPTDETAIL)
		{
            if (rECEIPTDETAIL == null)
            {
                throw new ArgumentNullException(nameof(rECEIPTDETAIL));
            }
			if (ModelState.IsValid)
			{
				rECEIPTDETAIL.TrackingState = TrackingState.Modified;
				                try{
				this.rECEIPTDETAILService.Update(rECEIPTDETAIL);
				                
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
				
				//DisplaySuccessMessage("Has update a RECEIPTDETAIL record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var receiptRepository = this.unitOfWork.RepositoryAsync<RECEIPT>();
												//return View(rECEIPTDETAIL);
		}
        //删除当前记录
		//GET: RECEIPTDETAILs/Delete/:id
        [HttpGet]
		public async Task<ActionResult> DeleteAsync(int id)
		{
          try{
               await this.rECEIPTDETAILService.Queryable().Where(x => x.ID == id).DeleteAsync();
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
               await this.rECEIPTDETAILService.Queryable().Where(x => id.Contains(x.ID)).DeleteAsync();
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
			var fileName = "receiptdetails_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  this.rECEIPTDETAILService.ExportExcel(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
