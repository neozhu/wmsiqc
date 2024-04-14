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
/// File: STOCKGROUPVIEWsController.cs
/// Purpose:库存作业/库存汇总查询
/// Created Date: 2019/7/9 7:29:46
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<STOCKGROUPVIEW>, Repository<STOCKGROUPVIEW>>();
///    container.RegisterType<ISTOCKGROUPVIEWService, STOCKGROUPVIEWService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("STOCKGROUPVIEWs")]
	public class STOCKGROUPVIEWsController : Controller
	{
		private readonly ISTOCKGROUPVIEWService  sTOCKGROUPVIEWService;
		private readonly IUnitOfWorkAsync unitOfWork;
		public STOCKGROUPVIEWsController (ISTOCKGROUPVIEWService  sTOCKGROUPVIEWService, IUnitOfWorkAsync unitOfWork)
		{
			this.sTOCKGROUPVIEWService  = sTOCKGROUPVIEWService;
			this.unitOfWork = unitOfWork;
		}
        		//GET: STOCKGROUPVIEWs/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "库存汇总查询 ", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :STOCKGROUPVIEWs/GetData
		//For Index View datagrid datasource url
		[HttpGet]
		 public async Task<JsonResult> GetDataAsync(int page = 1, int rows = 10, string sort = "RID", string order = "asc", string filterRules = "")
		{
      try
      {
        var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
        var pagerows = ( await this.sTOCKGROUPVIEWService
                                   .Query(new STOCKGROUPVIEWQuery().Withfilter(filters))
                                   .OrderBy(n => n.OrderBy(sort, order))
                                   .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     RID = n.RID,
                                     SUPPLIERCODE = n.SUPPLIERCODE,
                                     SUPPLIERNAME = n.SUPPLIERNAME,
                                     SKU = n.SKU,
                                     SKUNAME = n.SKUNAME,
                                     TOTAL = n.TOTAL,
                                     STATUS = n.STATUS
                                   }).ToList();
        var pagelist = new { total = totalCount, rows = pagerows };
        return Json(pagelist, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e) {
        throw e;
      }
		}
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> SaveDataAsync(STOCKGROUPVIEW[] stockgroupviews)
		{
            if (stockgroupviews == null)
            {
                throw new ArgumentNullException(nameof(stockgroupviews));
            }
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in stockgroupviews)
               {
                 this.sTOCKGROUPVIEWService.ApplyChanges(item);
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
						//GET: STOCKGROUPVIEWs/Details/:id
		public ActionResult Details(int id)
		{
			
			var sTOCKGROUPVIEW = this.sTOCKGROUPVIEWService.Find(id);
			if (sTOCKGROUPVIEW == null)
			{
				return HttpNotFound();
			}
			return View(sTOCKGROUPVIEW);
		}
        //GET: STOCKGROUPVIEWs/GetItemAsync/:id
        [HttpGet]
        public async Task<JsonResult> GetItemAsync(int id) {
            var  sTOCKGROUPVIEW = await this.sTOCKGROUPVIEWService.FindAsync(id);
            return Json(sTOCKGROUPVIEW,JsonRequestBehavior.AllowGet);
        }
		//GET: STOCKGROUPVIEWs/Create
        		public ActionResult Create()
				{
			var sTOCKGROUPVIEW = new STOCKGROUPVIEW();
			//set default value
			return View(sTOCKGROUPVIEW);
		}
		//POST: STOCKGROUPVIEWs/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(STOCKGROUPVIEW sTOCKGROUPVIEW)
		{
			if (sTOCKGROUPVIEW == null)
            {
                throw new ArgumentNullException(nameof(sTOCKGROUPVIEW));
            } 
            if (ModelState.IsValid)
			{
                try{ 
				this.sTOCKGROUPVIEWService.Insert(sTOCKGROUPVIEW);
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
			    //DisplaySuccessMessage("Has update a sTOCKGROUPVIEW record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//return View(sTOCKGROUPVIEW);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItemAsync() {
            var sTOCKGROUPVIEW = await Task.Run(() => {
                return new STOCKGROUPVIEW();
                });
            return Json(sTOCKGROUPVIEW, JsonRequestBehavior.AllowGet);
        }

         
		//GET: STOCKGROUPVIEWs/Edit/:id
		public ActionResult Edit(int id)
		{
			var sTOCKGROUPVIEW = this.sTOCKGROUPVIEWService.Find(id);
			if (sTOCKGROUPVIEW == null)
			{
				return HttpNotFound();
			}
			return View(sTOCKGROUPVIEW);
		}
		//POST: STOCKGROUPVIEWs/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(STOCKGROUPVIEW sTOCKGROUPVIEW)
		{
            if (sTOCKGROUPVIEW == null)
            {
                throw new ArgumentNullException(nameof(sTOCKGROUPVIEW));
            }
			if (ModelState.IsValid)
			{
				sTOCKGROUPVIEW.TrackingState = TrackingState.Modified;
				                try{
				this.sTOCKGROUPVIEWService.Update(sTOCKGROUPVIEW);
				                
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
				
				//DisplaySuccessMessage("Has update a STOCKGROUPVIEW record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//return View(sTOCKGROUPVIEW);
		}
        //删除当前记录
		//GET: STOCKGROUPVIEWs/Delete/:id
        [HttpGet]
		public async Task<ActionResult> DeleteAsync(int id)
		{
          try{
               await this.sTOCKGROUPVIEWService.Queryable().Where(x => x.RID == id).DeleteAsync();
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
		public ActionResult ExportExcel( string filterRules = "",string sort = "RID", string order = "asc")
		{
			var fileName = "stockgroupviews_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream=  this.sTOCKGROUPVIEWService.ExportExcel(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
