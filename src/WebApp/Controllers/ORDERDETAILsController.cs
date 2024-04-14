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
/// File: ORDERDETAILsController.cs
/// Purpose:出货作业/出货单明细
/// Created Date: 2019/6/27 8:28:14
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<ORDERDETAIL>, Repository<ORDERDETAIL>>();
///    container.RegisterType<IORDERDETAILService, ORDERDETAILService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("ORDERDETAILs")]
	public class ORDERDETAILsController : Controller
	{
		private readonly IORDERDETAILService  oRDERDETAILService;
		private readonly IUnitOfWorkAsync unitOfWork;
		public ORDERDETAILsController (IORDERDETAILService  oRDERDETAILService, IUnitOfWorkAsync unitOfWork)
		{
			this.oRDERDETAILService  = oRDERDETAILService;
			this.unitOfWork = unitOfWork;
		}
        		//GET: ORDERDETAILs/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "出货单明细 ", Order = 1)]
		public ActionResult Index() => this.View();

    //获取看板数据
    [HttpGet]
    public async Task<JsonResult> GetKanbanData(DateTime dt1, DateTime dt2, string supplier, string susr2)
    {
      var array = new string[] { "100", "102", "106" };
      var query = this.oRDERDETAILService.Queryable().Where(x =>
        DbFunctions.TruncateTime(x.REQUESTEDSHIPDATE) >= dt1 &&
        DbFunctions.TruncateTime(x.REQUESTEDSHIPDATE) <= dt2 
       );
      if (!string.IsNullOrEmpty(supplier))
      {
        query = query.Where(x => x.SUPPLIERCODE == supplier);
      }
      if (!string.IsNullOrEmpty(susr2))
      {
        query = query.Where(x => x.LOTTABLE2 == susr2);
      }
      var data = await query.OrderByDescending(x => x.REQUESTEDSHIPDATE).ToListAsync();
      var rows = query.Select(n =>new {
        REQUESTEDSHIPDATE= n.REQUESTEDSHIPDATE,
        SUPPLIERNAME = n.SUPPLIERNAME,
        SKUNAME= n.SKU + "|" + n.SKUNAME,
        EXTERNORDERKEY= n.EXTERNORDERKEY,
        n.LOTTABLE2,
        QTY=n.ORIGINALQTY,
        STATUS = n.STATUS,
        DELIVERYDATE = n.DELIVERYDATE,
        n.ACTUALSHIPDATE,
        n.NOTES
       
      }).ToList();

      return Json(rows, JsonRequestBehavior.AllowGet);


    }
    private  int dateDiffH(DateTime dt1, DateTime dt2)
    {
       
      try
      {
        var ts1 = new TimeSpan(dt1.Ticks);
        var ts2 = new TimeSpan(dt2.Ticks);
        var ts = ts1.Subtract(ts2).Duration();

        return ts.Hours;
      }
      catch
      {

      }
      return 0;
    }
    private int dateDiffM(DateTime dt1, DateTime dt2)
    {

      try
      {
        var ts1 = new TimeSpan(dt1.Ticks);
        var ts2 = new TimeSpan(dt2.Ticks);
        var ts = ts1.Subtract(ts2).Duration();
        return ts.Minutes;
      }
      catch
      {

      }
      return 0;
    }
    [HttpGet]
    public async Task<JsonResult> GetDataByID(int id) {
      var data=(await this.oRDERDETAILService.Queryable().Where(x=>x.ORDERID==id
                && x.STATUS!="108"
      )
          .ToListAsync())
          .Select(n => new {
            ORDERORDERKEY = n.ORDER?.ORDERKEY,
            ID = n.ID,
            ORDERKEY = n.ORDERKEY,
            WHSEID = n.WHSEID,
            STORERKEY = n.STORERKEY,
            EXTERNORDERKEY = n.EXTERNORDERKEY,
            ORDERLINENUMBER = n.ORDERLINENUMBER,
            STATUS = n.STATUS,
            LOTTABLE2 = n.LOTTABLE2,
            SUPPLIERCODE = n.SUPPLIERCODE,
            SUPPLIERNAME = n.SUPPLIERNAME,
            SKU = n.SKU,
            SKUNAME = n.SKUNAME,
            ALTSKU = n.ALTSKU,
            ORIGINALQTY = n.ORIGINALQTY,
            UMO = n.UMO,
            CASECNT = n.CASECNT,
            OPENQTY = n.OPENQTY,
            SHIPPEDQTY = n.SHIPPEDQTY,
            QTYPICKED = n.QTYPICKED,
            PALLET = n.PALLET,
            PACKKEY = n.PACKKEY,
            GROSSWGT = n.GROSSWGT,
            NETWGT = n.NETWGT,
            CUBE = n.CUBE,
            NOTES = n.NOTES,
            PQCQTYINSPECTED = n.PQCQTYINSPECTED,
            PQCQTYREJECTED = n.PQCQTYREJECTED,
            PQCSTATUS = n.PQCSTATUS,
            PQCWHO = n.PQCWHO,
            PQCDATE = n.PQCDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
            LOTTABLE3 = n.LOTTABLE3,
            LOTTABLE1 = n.LOTTABLE1,
            REQUESTEDSHIPDATE = n.REQUESTEDSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
            DELIVERYDATE = n.DELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
            EXTERNLINENO = n.EXTERNLINENO,
            LOTTABLE4 = n.LOTTABLE4,
            LOTTABLE5 = n.LOTTABLE5,
            LOTTABLE6 = n.LOTTABLE6,
            LOTTABLE7 = n.LOTTABLE7,
            LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
            ORDERID = n.ORDERID,
            ACTUALSHIPDATE=n.ACTUALSHIPDATE,
            ACTUALDELIVERYDATE=n.ACTUALDELIVERYDATE,
            LOTTABLE9=n.LOTTABLE9,
            LOTTABLE10=n.LOTTABLE10,
            TYPE =n.TYPE
          }).ToList();

      return Json(data, JsonRequestBehavior.AllowGet);
    }
        //Get :ORDERDETAILs/GetData
        //For Index View datagrid datasource url
        [HttpGet]
		 public async Task<JsonResult> GetDataAsync(int page = 1, int rows = 10, string sort = "ID", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.oRDERDETAILService
						               .Query(new ORDERDETAILQuery().Withfilter(filters)).Include(o => o.ORDER)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ORDERORDERKEY = n.ORDER?.ORDERKEY,
    ID = n.ID,
    ORDERKEY = n.ORDERKEY,
    WHSEID = n.WHSEID,
    STORERKEY = n.STORERKEY,
    EXTERNORDERKEY = n.EXTERNORDERKEY,
    ORDERLINENUMBER = n.ORDERLINENUMBER,
    STATUS = n.STATUS,
    LOTTABLE2 = n.LOTTABLE2,
    SUPPLIERCODE = n.SUPPLIERCODE,
    SUPPLIERNAME = n.SUPPLIERNAME,
    SKU = n.SKU,
    SKUNAME = n.SKUNAME,
    ALTSKU = n.ALTSKU,
    ORIGINALQTY = n.ORIGINALQTY,
    UMO = n.UMO,
    CASECNT = n.CASECNT,
    OPENQTY = n.OPENQTY,
    SHIPPEDQTY = n.SHIPPEDQTY,
    QTYPICKED = n.QTYPICKED,
    PALLET = n.PALLET,
    PACKKEY = n.PACKKEY,
    GROSSWGT = n.GROSSWGT,
    NETWGT = n.NETWGT,
    CUBE = n.CUBE,
    NOTES = n.NOTES,
    PQCQTYINSPECTED = n.PQCQTYINSPECTED,
    PQCQTYREJECTED = n.PQCQTYREJECTED,
    PQCSTATUS = n.PQCSTATUS,
    PQCWHO = n.PQCWHO,
    PQCDATE = n.PQCDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
    LOTTABLE3 = n.LOTTABLE3,
    LOTTABLE1 = n.LOTTABLE1,
    REQUESTEDSHIPDATE = n.REQUESTEDSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
    DELIVERYDATE = n.DELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
    EXTERNLINENO = n.EXTERNLINENO,
    LOTTABLE4 = n.LOTTABLE4,
    LOTTABLE5 = n.LOTTABLE5,
    LOTTABLE6 = n.LOTTABLE6,
    LOTTABLE7 = n.LOTTABLE7,
                                         ACTUALSHIPDATE = n.ACTUALSHIPDATE,
                                         ACTUALDELIVERYDATE = n.ACTUALDELIVERYDATE,
                                         LOTTABLE9 = n.LOTTABLE9,
                                         LOTTABLE10 = n.LOTTABLE10,
                                         LOTTABLE8 = n.LOTTABLE8?.ToString("yyyy/MM/dd HH:mm:ss"),
                                         TYPE = n.TYPE,
    ORDERID = n.ORDERID
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        public async Task<JsonResult> GetDataByORDERIDAsync (int  orderid ,int page = 1, int rows = 10, string sort = "ID", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.oRDERDETAILService
						               .Query(new ORDERDETAILQuery().ByORDERIDWithfilter(orderid,filters)).Include(o => o.ORDER)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    ORDERORDERKEY = n.ORDER?.ORDERKEY,
    ID = n.ID,
    ORDERKEY = n.ORDERKEY,
    WHSEID = n.WHSEID,
    STORERKEY = n.STORERKEY,
    EXTERNORDERKEY = n.EXTERNORDERKEY,
    ORDERLINENUMBER = n.ORDERLINENUMBER,
    STATUS = n.STATUS,
    LOTTABLE2 = n.LOTTABLE2,
    SUPPLIERCODE = n.SUPPLIERCODE,
    SUPPLIERNAME = n.SUPPLIERNAME,
    SKU = n.SKU,
    SKUNAME = n.SKUNAME,
    ALTSKU = n.ALTSKU,
    ORIGINALQTY = n.ORIGINALQTY,
    UMO = n.UMO,
    CASECNT = n.CASECNT,
    OPENQTY = n.OPENQTY,
    SHIPPEDQTY = n.SHIPPEDQTY,
    QTYPICKED = n.QTYPICKED,
    PALLET = n.PALLET,
    PACKKEY = n.PACKKEY,
    GROSSWGT = n.GROSSWGT,
    NETWGT = n.NETWGT,
    CUBE = n.CUBE,
    NOTES = n.NOTES,
    PQCQTYINSPECTED = n.PQCQTYINSPECTED,
    PQCQTYREJECTED = n.PQCQTYREJECTED,
    PQCSTATUS = n.PQCSTATUS,
    PQCWHO = n.PQCWHO,
    PQCDATE = n.PQCDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
    LOTTABLE3 = n.LOTTABLE3,
    LOTTABLE1 = n.LOTTABLE1,
    REQUESTEDSHIPDATE = n.REQUESTEDSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
    DELIVERYDATE = n.DELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
    EXTERNLINENO = n.EXTERNLINENO,
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
		public async Task<JsonResult> SaveDataAsync(ORDERDETAIL[] orderdetails)
		{
            if (orderdetails == null)
            {
                throw new ArgumentNullException(nameof(orderdetails));
            }
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in orderdetails)
               {
                 this.oRDERDETAILService.ApplyChanges(item);
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
								//GET: ORDERDETAILs/Details/:id
		public ActionResult Details(int id)
		{
			
			var oRDERDETAIL = this.oRDERDETAILService.Find(id);
			if (oRDERDETAIL == null)
			{
				return HttpNotFound();
			}
			return View(oRDERDETAIL);
		}
        //GET: ORDERDETAILs/GetItemAsync/:id
        [HttpGet]
        public async Task<JsonResult> GetItemAsync(int id) {
            var  oRDERDETAIL = await this.oRDERDETAILService.FindAsync(id);
            return Json(oRDERDETAIL,JsonRequestBehavior.AllowGet);
        }
		//GET: ORDERDETAILs/Create
        		public ActionResult Create()
				{
			var oRDERDETAIL = new ORDERDETAIL();
			//set default value
			var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
		   			ViewBag.ORDERID = new SelectList(orderRepository.Queryable().OrderBy(n=>n.ORDERKEY), "ID", "ORDERKEY");
		   			return View(oRDERDETAIL);
		}
		//POST: ORDERDETAILs/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(ORDERDETAIL oRDERDETAIL)
		{
			if (oRDERDETAIL == null)
            {
                throw new ArgumentNullException(nameof(oRDERDETAIL));
            } 
            if (ModelState.IsValid)
			{
                try{ 
				this.oRDERDETAILService.Insert(oRDERDETAIL);
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
			    //DisplaySuccessMessage("Has update a oRDERDETAIL record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
			//ViewBag.ORDERID = new SelectList(await orderRepository.Queryable().OrderBy(n=>n.ORDERKEY).ToListAsync(), "ID", "ORDERKEY", oRDERDETAIL.ORDERID);
			//return View(oRDERDETAIL);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItemAsync() {
            var oRDERDETAIL = await Task.Run(() => {
                return new ORDERDETAIL();
                });
            return Json(oRDERDETAIL, JsonRequestBehavior.AllowGet);
        }

         
		//GET: ORDERDETAILs/Edit/:id
		public ActionResult Edit(int id)
		{
			var oRDERDETAIL = this.oRDERDETAILService.Find(id);
			if (oRDERDETAIL == null)
			{
				return HttpNotFound();
			}
			var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
			ViewBag.ORDERID = new SelectList(orderRepository.Queryable().OrderBy(n=>n.ORDERKEY), "ID", "ORDERKEY", oRDERDETAIL.ORDERID);
			return View(oRDERDETAIL);
		}
		//POST: ORDERDETAILs/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(ORDERDETAIL oRDERDETAIL)
		{
            if (oRDERDETAIL == null)
            {
                throw new ArgumentNullException(nameof(oRDERDETAIL));
            }
			if (ModelState.IsValid)
			{
				oRDERDETAIL.TrackingState = TrackingState.Modified;
				                try{
				this.oRDERDETAILService.Update(oRDERDETAIL);
				                
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
				
				//DisplaySuccessMessage("Has update a ORDERDETAIL record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
												//return View(oRDERDETAIL);
		}
        //删除当前记录
		//GET: ORDERDETAILs/Delete/:id
        [HttpGet]
		public async Task<ActionResult> DeleteAsync(int id)
		{
          try{
               await this.oRDERDETAILService.Queryable().Where(x => x.ID == id).DeleteAsync();
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
               await this.oRDERDETAILService.Queryable().Where(x => id.Contains(x.ID)).DeleteAsync();
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
			var fileName = "orderdetails_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  this.oRDERDETAILService.ExportExcel(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
