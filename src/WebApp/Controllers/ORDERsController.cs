using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using TrackableEntities;
using WebApp.Models;
using WebApp.Models.ViewModel;
using WebApp.Repositories;
using WebApp.Services;
using Z.EntityFramework.Plus;
namespace WebApp.Controllers
{
  /// <summary>
  /// File: ORDERsController.cs
  /// Purpose:出货作业/出货单管理
  /// Created Date: 2019/6/27 8:34:15
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<ORDER>, Repository<ORDER>>();
  ///    container.RegisterType<IORDERService, ORDERService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("ORDERs")]
  public class ORDERsController : Controller
  {
    private readonly IORDERService oRDERService;
    private readonly IUnitOfWorkAsync unitOfWork;
    public ORDERsController(IORDERService oRDERService, IUnitOfWorkAsync unitOfWork)
    {
      this.oRDERService = oRDERService;
      this.unitOfWork = unitOfWork;
    }
    //GET: ORDERs/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "出货单管理 ", Order = 1)]
    public ActionResult Index() => this.View();

    [Route("Picking", Name = "扫描拣货 ", Order = 1)]
    public ActionResult Picking() => this.View();
    

    //扫描比对拣货
    [HttpGet]
    public async Task<JsonResult> ScanPicking(int orderid,string barcode1,string barcode2,decimal qty)
    {
      try
      {
        this.oRDERService.ScanPicking(orderid, barcode1, barcode2, qty, this.User.Identity.Name);
        await this.unitOfWork.SaveChangesAsync();
        this.oRDERService.UpdateQty(orderid);
        await this.unitOfWork.SaveChangesAsync();
        return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e) {
        return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }

    }
    [HttpGet]
    public async Task<JsonResult> GetOrderDateList() {
      var array = new string[] { "100", "102", "107","106" };
      var list = ( await this.oRDERService.Queryable().Where(x => array.Contains(x.STATUS))
               .Select(x => new { value = x.ORDERDATE })
               .Distinct()
               .ToListAsync() )
               .Select(x => new { value = x.value.ToString("yyyy-MM-dd")}).Distinct();
      return Json(list, JsonRequestBehavior.AllowGet);
    }
    //生成出货单号
    [HttpGet]
    public async Task<JsonResult> GetOrderKey()
    {
      var key = await Task.Run(() => { return KeyGenerator.GetNextSOKey(); });
      return Json(key, JsonRequestBehavior.AllowGet);
    }
    //获取收货单明细根据日期
    [HttpGet]
    public async Task<JsonResult> GetOrderGrid(string dt)
    {
      var array = new string[] { "100", "102", "107", "106" };
      var date = DateTime.Parse(dt);
      var list = (await this.oRDERService.Queryable()
                     .Where(x => DbFunctions.TruncateTime(x.ORDERDATE) == date &&
                      array.Contains(x.STATUS) )
                     .ToListAsync())
               .Select(n => new {
                 ORDERDETAILS = n.ORDERDETAILS,
                 PICKDETAILS = n.PICKDETAILS,
                 ID = n.ID,
                 ORDERKEY = n.ORDERKEY,
                 WHSEID = n.WHSEID,
                 EXTERNORDERKEY = n.EXTERNORDERKEY,
                 STATUS = n.STATUS,
                 ORDERDATE = n.ORDERDATE.ToString("yyyy/MM/dd HH:mm:ss"),
                 REQUESTEDSHIPDATE = n.REQUESTEDSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                 DELIVERYDATE = n.DELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                 STORERKEY = n.STORERKEY,
                 TYPE = n.TYPE,
                 SUPPLIERCODE = n.SUPPLIERCODE,
                 SUPPLIERNAME = n.SUPPLIERNAME,
                 SUSR2 = n.SUSR2,
                 CONSIGNEEKEY = n.CONSIGNEEKEY,
                 CONSIGNEENAME = n.CONSIGNEENAME,
                 CONSIGNEEADDRESS = n.CONSIGNEEADDRESS,
                 CARRIERNAME = n.CARRIERNAME,
                 DRIVERNAME = n.DRIVERNAME,
                 CARRIERPHONE = n.CARRIERPHONE,
                 TRAILERNUMBER = n.TRAILERNUMBER,
                 ACTUALSHIPDATE = n.ACTUALSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                 ACTUALDELIVERYDATE = n.ACTUALDELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                 CLOSEDDATE = n.CLOSEDDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                 TOTALQTY = n.TOTALQTY,
                 TOTALQTYPICKED=n.TOTALQTYPICKED,
                 OPENQTY = n.OPENQTY,
                 TOTALPACKAGE = n.TOTALPACKAGE,
                 TOTALGROSSWEIGHT = n.TOTALGROSSWEIGHT,
                 TOTALVOLUME = n.TOTALVOLUME,
                 NOTES = n.NOTES,
                 SUSR1 = n.SUSR1,
                 SUSR3 = n.SUSR3,
                 SUSR4 = n.SUSR4,
                 SUSR5 = n.SUSR5,
                 SUSR6 = n.SUSR6,
                 SUSR7 = n.SUSR7,
                 SUSR8 = n.SUSR8?.ToString("yyyy/MM/dd HH:mm:ss")
               }).ToList();

      return Json(list, JsonRequestBehavior.AllowGet);
    }
    //出货发运
    [HttpPost]
    public async Task<JsonResult> PostShipping(POSTQTYSHIPPED[] data) {
      try
      {
         this.oRDERService.PostShipping(data,this.User.Identity.Name);
        await this.unitOfWork.SaveChangesAsync();
        this.oRDERService.UpdateQty(data[0].ORDERID);
        await this.unitOfWork.SaveChangesAsync();
        return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e) {
        return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }

    }
    //取消拣货
    [HttpGet]
    public async Task<JsonResult> UndoPicked(int pickid,int orderid) {
      try
      {
        this.oRDERService.UndoPicked(pickid, this.User.Identity.Name);
        await this.unitOfWork.SaveChangesAsync();
        this.oRDERService.UpdateQty(orderid);
        await this.unitOfWork.SaveChangesAsync();
        return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get :ORDERs/GetData
    //For Index View datagrid datasource url
    [HttpGet]
    public async Task<JsonResult> GetDataAsync(int page = 1, int rows = 10, string sort = "ID", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.oRDERService
                                 .Query(new ORDERQuery().Withfilter(filters))
                                 .OrderBy(n => n.OrderBy(sort, order))
                                 .SelectPageAsync(page, rows, out var totalCount) )
                                 .Select(n => new
                                 {

                                   ORDERDETAILS = n.ORDERDETAILS,
                                   PICKDETAILS = n.PICKDETAILS,
                                   ID = n.ID,
                                   ORDERKEY = n.ORDERKEY,
                                   WHSEID = n.WHSEID,
                                   EXTERNORDERKEY = n.EXTERNORDERKEY,
                                   STATUS = n.STATUS,
                                   ORDERDATE = n.ORDERDATE.ToString("yyyy/MM/dd HH:mm:ss"),
                                   REQUESTEDSHIPDATE = n.REQUESTEDSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                                   DELIVERYDATE = n.DELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                                   STORERKEY = n.STORERKEY,
                                   TYPE = n.TYPE,
                                   SUPPLIERCODE = n.SUPPLIERCODE,
                                   SUPPLIERNAME = n.SUPPLIERNAME,
                                   SUSR2 = n.SUSR2,
                                   CONSIGNEEKEY = n.CONSIGNEEKEY,
                                   CONSIGNEENAME = n.CONSIGNEENAME,
                                   CONSIGNEEADDRESS = n.CONSIGNEEADDRESS,
                                   CARRIERNAME = n.CARRIERNAME,
                                   DRIVERNAME = n.DRIVERNAME,
                                   CARRIERPHONE = n.CARRIERPHONE,
                                   TRAILERNUMBER = n.TRAILERNUMBER,
                                   ACTUALSHIPDATE = n.ACTUALSHIPDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                                   ACTUALDELIVERYDATE = n.ACTUALDELIVERYDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                                   CLOSEDDATE = n.CLOSEDDATE?.ToString("yyyy/MM/dd HH:mm:ss"),
                                   TOTALQTY = n.TOTALQTY,
                                   TOTALQTYPICKED = n.TOTALQTYPICKED,
                                   OPENQTY = n.OPENQTY,
                                   TOTALPACKAGE = n.TOTALPACKAGE,
                                   TOTALGROSSWEIGHT = n.TOTALGROSSWEIGHT,
                                   TOTALVOLUME = n.TOTALVOLUME,
                                   NOTES = n.NOTES,
                                   SUSR1 = n.SUSR1,
                                   SUSR3 = n.SUSR3,
                                   SUSR4 = n.SUSR4,
                                   SUSR5 = n.SUSR5,
                                   SUSR6 = n.SUSR6,
                                   SUSR7 = n.SUSR7,
                                   SUSR8 = n.SUSR8?.ToString("yyyy/MM/dd HH:mm:ss")
                                 }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return this.Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveDataAsync(ORDER[] orders)
    {
      if (orders == null)
      {
        throw new ArgumentNullException(nameof(orders));
      }
      if (this.ModelState.IsValid)
      {
        try
        {
          foreach (var item in orders)
          {
            this.oRDERService.ApplyChanges(item);
          }
          var result = await this.unitOfWork.SaveChangesAsync();
          return this.Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (System.Data.Entity.Validation.DbEntityValidationException e)
        {
          var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
          return this.Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
        }
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return this.Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
      }

    }
    //[OutputCache(Duration = 20, VaryByParam = "q")]
    public async Task<JsonResult> GetORDERSAsync(string q = "")
    {
      var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
      var rows = await orderRepository
                      .Queryable()
                      .Where(n => n.ORDERKEY.Contains(q))
                      .OrderBy(n => n.ORDERKEY)
                      .Select(n => new { ID = n.ID, ORDERKEY = n.ORDERKEY })
                      .ToListAsync();

      return this.Json(rows, JsonRequestBehavior.AllowGet);
    }
    //[OutputCache(Duration = 20, VaryByParam = "q")]

    //GET: ORDERs/Details/:id
    public ActionResult Details(int id)
    {

      var oRDER = this.oRDERService.Find(id);
      if (oRDER == null)
      {
        return this.HttpNotFound();
      }
      return this.View(oRDER);
    }
    //GET: ORDERs/GetItemAsync/:id
    [HttpGet]
    public async Task<JsonResult> GetItemAsync(int id)
    {
      var oRDER = await this.oRDERService.FindAsync(id);
      return this.Json(oRDER, JsonRequestBehavior.AllowGet);
    }
    //GET: ORDERs/Create
    public ActionResult Create()
    {
      var oRDER = new ORDER();
      //set default value
      return this.View(oRDER);
    }
    //POST: ORDERs/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(ORDER oRDER)
    {
      if (oRDER == null)
      {
        throw new ArgumentNullException(nameof(oRDER));
      }
      if (this.ModelState.IsValid)
      {
        oRDER.TrackingState = TrackingState.Added;
        foreach (var item in oRDER.ORDERDETAILS)
        {
          item.ORDERID = oRDER.ID;
          item.TrackingState = TrackingState.Added;
        }
        foreach (var item in oRDER.PICKDETAILS)
        {
          item.ORDERID = oRDER.ID;
          item.TrackingState = TrackingState.Added;
        }
        try
        {
          this.oRDERService.ApplyChanges(oRDER);
          var result = await this.unitOfWork.SaveChangesAsync();
          return this.Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (System.Data.Entity.Validation.DbEntityValidationException e)
        {
          var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
          return this.Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a oRDER record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return this.Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(oRDER);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItemAsync()
    {
      var oRDER = await Task.Run(() =>
      {
        return new ORDER();
      });
      return this.Json(oRDER, JsonRequestBehavior.AllowGet);
    }


    //GET: ORDERs/Edit/:id
    public ActionResult Edit(int id)
    {
      var oRDER = this.oRDERService.Find(id);
      if (oRDER == null)
      {
        return this.HttpNotFound();
      }
      return this.View(oRDER);
    }
    //POST: ORDERs/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(ORDER oRDER)
    {
      if (oRDER == null)
      {
        throw new ArgumentNullException(nameof(oRDER));
      }
      if (this.ModelState.IsValid)
      {
        oRDER.TrackingState = TrackingState.Modified;
        foreach (var item in oRDER.ORDERDETAILS)
        {
          item.ORDERID = oRDER.ID;
        }
        foreach (var item in oRDER.PICKDETAILS)
        {
          item.ORDERID = oRDER.ID;
        }

        try
        {
          this.oRDERService.ApplyChanges(oRDER);

          var result = await this.unitOfWork.SaveChangesAsync();
          return this.Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (System.Data.Entity.Validation.DbEntityValidationException e)
        {
          var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
          return this.Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a ORDER record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return this.Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(oRDER);
    }
    //删除当前记录
    //GET: ORDERs/Delete/:id
    [HttpGet]
    public async Task<ActionResult> DeleteAsync(int id)
    {
      try
      {
        await this.oRDERService.Queryable().Where(x => x.ID == id).DeleteAsync();
        return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.Entity.Validation.DbEntityValidationException e)
      {
        var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
        return this.Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get Detail Row By Id For Edit
    //Get : ORDERs/EditORDERDETAIL/:id
    [HttpGet]
    public async Task<ActionResult> EditORDERDETAIL(int id)
    {
      var orderdetailRepository = this.unitOfWork.RepositoryAsync<ORDERDETAIL>();
      var orderdetail = await orderdetailRepository.FindAsync(id);
      var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
      if (orderdetail == null)
      {
        this.ViewBag.ORDERID = new SelectList(await orderRepository.Queryable().OrderBy(n => n.ORDERKEY).ToListAsync(), "ID", "ORDERKEY");
        //return HttpNotFound();
        return this.PartialView("_ORDERDETAILEditForm", new ORDERDETAIL());
      }
      else
      {
        this.ViewBag.ORDERID = new SelectList(await orderRepository.Queryable().ToListAsync(), "ID", "ORDERKEY", orderdetail.ORDERID);
      }
      return this.PartialView("_ORDERDETAILEditForm", orderdetail);
    }
    //Get Create Row By Id For Edit
    //Get : ORDERs/CreateORDERDETAIL
    [HttpGet]
    public async Task<ActionResult> CreateORDERDETAILAsync(int orderid)
    {
      var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
      this.ViewBag.ORDERID = new SelectList(await orderRepository.Queryable().OrderBy(n => n.ORDERKEY).ToListAsync(), "ID", "ORDERKEY");
      return this.PartialView("_ORDERDETAILEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : ORDERs/DeleteORDERDETAIL/:id
    [HttpGet]
    public async Task<ActionResult> DeleteORDERDETAILAsync(int id)
    {
      try
      {
        var orderdetailRepository = this.unitOfWork.RepositoryAsync<ORDERDETAIL>();
        orderdetailRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return this.Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.Entity.Validation.DbEntityValidationException e)
      {
        var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
        return this.Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get Detail Row By Id For Edit
    //Get : ORDERs/EditPICKDETAIL/:id
    [HttpGet]
    public async Task<ActionResult> EditPICKDETAIL(int id)
    {
      var pickdetailRepository = this.unitOfWork.RepositoryAsync<PICKDETAIL>();
      var pickdetail = await pickdetailRepository.FindAsync(id);
      var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
      if (pickdetail == null)
      {
        this.ViewBag.ORDERID = new SelectList(await orderRepository.Queryable().OrderBy(n => n.ORDERKEY).ToListAsync(), "ID", "ORDERKEY");
        //return HttpNotFound();
        return this.PartialView("_PICKDETAILEditForm", new PICKDETAIL());
      }
      else
      {
        this.ViewBag.ORDERID = new SelectList(await orderRepository.Queryable().ToListAsync(), "ID", "ORDERKEY", pickdetail.ORDERID);
      }
      return this.PartialView("_PICKDETAILEditForm", pickdetail);
    }
    //Get Create Row By Id For Edit
    //Get : ORDERs/CreatePICKDETAIL
    [HttpGet]
    public async Task<ActionResult> CreatePICKDETAILAsync(int orderid)
    {
      var orderRepository = this.unitOfWork.RepositoryAsync<ORDER>();
      this.ViewBag.ORDERID = new SelectList(await orderRepository.Queryable().OrderBy(n => n.ORDERKEY).ToListAsync(), "ID", "ORDERKEY");
      return this.PartialView("_PICKDETAILEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : ORDERs/DeletePICKDETAIL/:id
    [HttpGet]
    public async Task<ActionResult> DeletePICKDETAILAsync(int id)
    {
      try
      {
        var pickdetailRepository = this.unitOfWork.RepositoryAsync<PICKDETAIL>();
        pickdetailRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return this.Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.Entity.Validation.DbEntityValidationException e)
      {
        var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
        return this.Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get : ORDERs/GetORDERDETAILSByORDERID/:id
    [HttpGet]
    public async Task<JsonResult> GetORDERDETAILSByORDERIDAsync(int id)
    {
      var orderdetails = this.oRDERService.GetORDERDETAILSByORDERID(id);
      var data = await orderdetails.AsQueryable().ToListAsync();
      var rows = data.Select(n => new
      {

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
      });
      return this.Json(rows, JsonRequestBehavior.AllowGet);

    }
    //Get : ORDERs/GetPICKDETAILSByORDERID/:id
    [HttpGet]
    public async Task<JsonResult> GetPICKDETAILSByORDERIDAsync(int id)
    {
      var pickdetails = this.oRDERService.GetPICKDETAILSByORDERID(id);
      var data = await pickdetails.AsQueryable().ToListAsync();
      var rows = data.Select(n => new
      {

        ORDERORDERKEY = n.ORDER?.ORDERKEY,
        ID = n.ID,
        PICKDETAILKEY = n.PICKDETAILKEY,
        WHSEID = n.WHSEID,
        ORDERKEY = n.ORDERKEY,
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
      });
      return this.Json(rows, JsonRequestBehavior.AllowGet);

    }


    //删除选中的记录
    [HttpPost]
    public async Task<JsonResult> DeleteCheckedAsync(int[] id)
    {
      if (id == null)
      {
        throw new ArgumentNullException(nameof(id));
      }
      try
      {
        await this.oRDERService.Queryable().Where(x => id.Contains(x.ID)).DeleteAsync();
        return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.Entity.Validation.DbEntityValidationException e)
      {
        var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
        return this.Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return this.Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //导出Excel
    [HttpPost]
    public ActionResult ExportExcel(string filterRules = "", string sort = "ID", string order = "asc")
    {
      var fileName = "orders_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = this.oRDERService.ExportExcel(filterRules, sort, order);
      return this.File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => this.TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => this.TempData["ErrorMessage"] = msgText;

  }
}
