﻿using System;
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
using WebApp.Repositories;
using WebApp.Services;
using Z.EntityFramework.Plus;
namespace WebApp.Controllers
{
  /// <summary>
  /// File: EmployeesController.cs
  /// Purpose:
  /// Created Date: 2019/6/6 15:34:36
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<Employee>, Repository<Employee>>();
  ///    container.RegisterType<IEmployeeService, EmployeeService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("Employees")]
  public class EmployeesController : Controller
  {
    private readonly IEmployeeService employeeService;
    private readonly IUnitOfWorkAsync unitOfWork;
    public EmployeesController(IEmployeeService employeeService, IUnitOfWorkAsync unitOfWork)
    {
      this.employeeService = employeeService;
      this.unitOfWork = unitOfWork;
    }
    //GET: Employees/Index
    [OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "员工信息", Order = 1)]
    public ActionResult Index() => this.View();

    //Get :Employees/GetData
    //For Index View datagrid datasource url
    [HttpGet]
    public async Task<JsonResult> GetDataAsync(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.employeeService
                                 .Query(new EmployeeQuery().Withfilter(filters)).Include(e => e.Company)
                                 .OrderBy(n => n.OrderBy(sort, order))
                                 .SelectPageAsync(page, rows, out var totalCount) )
                                 .Select(n => new
                                 {

                                   CompanyName = n.Company?.Name,
                                   Id = n.Id,
                                   Name = n.Name,
                                   Title = n.Title,
                                   Sex = n.Sex,
                                   Age = n.Age,
                                   Brithday = n.Brithday.ToString("yyyy/MM/dd HH:mm:ss"),
                                   IsDeleted = n.IsDeleted,
                                   CompanyId = n.CompanyId
                                 }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return this.Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    public async Task<JsonResult> GetDataByCompanyIdAsync(int companyid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.employeeService
                             .Query(new EmployeeQuery().ByCompanyIdWithfilter(companyid, filters)).Include(e => e.Company)
                             .OrderBy(n => n.OrderBy(sort, order))
                             .SelectPageAsync(page, rows, out var totalCount) )
                             .Select(n => new
                             {

                               CompanyName = n.Company?.Name,
                               Id = n.Id,
                               Name = n.Name,
                               Title = n.Title,
                               Sex = n.Sex,
                               Age = n.Age,
                               Brithday = n.Brithday.ToString("yyyy/MM/dd HH:mm:ss"),
                               IsDeleted = n.IsDeleted,
                               CompanyId = n.CompanyId
                             }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return this.Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveDataAsync(Employee[] employees)
    {
      if (employees == null)
      {
        throw new ArgumentNullException(nameof(employees));
      }
      if (this.ModelState.IsValid)
      {
        try
        {
          foreach (var item in employees)
          {
            this.employeeService.ApplyChanges(item);
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
    [OutputCache(Duration = 20, VaryByParam = "q")]
    public async Task<JsonResult> GetCompaniesAsync(string q = "")
    {
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      var rows = await companyRepository
                      .Queryable()
                      .Where(n => n.Name.Contains(q))
                      .OrderBy(n => n.Name)
                      .Select(n => new { Id = n.Id, Name = n.Name })
                      .ToListAsync();
      return this.Json(rows, JsonRequestBehavior.AllowGet);
    }
    //GET: Employees/Details/:id
    public ActionResult Details(int id)
    {

      var employee = this.employeeService.Find(id);
      if (employee == null)
      {
        return this.HttpNotFound();
      }
      return this.View(employee);
    }
    //GET: Employees/GetItemAsync/:id
    [HttpGet]
    public async Task<JsonResult> GetItemAsync(int id)
    {
      var employee = await this.employeeService.FindAsync(id);
      return this.Json(employee, JsonRequestBehavior.AllowGet);
    }
    //GET: Employees/Create
    public ActionResult Create()
    {
      var employee = new Employee();
      //set default value
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      this.ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n => n.Name), "Id", "Name");
      return this.View(employee);
    }
    //POST: Employees/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(Employee employee)
    {
      if (employee == null)
      {
        throw new ArgumentNullException(nameof(employee));
      }
      if (this.ModelState.IsValid)
      {
        try
        {
          this.employeeService.Insert(employee);
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
        //DisplaySuccessMessage("Has update a employee record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return this.Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      //ViewBag.CompanyId = new SelectList(await companyRepository.Queryable().OrderBy(n=>n.Name).ToListAsync(), "Id", "Name", employee.CompanyId);
      //return View(employee);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItemAsync()
    {
      var employee = await Task.Run(() =>
      {
        return new Employee();
      });
      return this.Json(employee, JsonRequestBehavior.AllowGet);
    }

    //GET: Employees/PopupEdit/:id
    //[OutputCache(Duration = 360, VaryByParam = "id")]
    [HttpGet]
    public async Task<JsonResult> PopupEditAsync(int id)
    {

      var employee = await this.employeeService.FindAsync(id);
      return this.Json(employee, JsonRequestBehavior.AllowGet);
    }

    //GET: Employees/Edit/:id
    public ActionResult Edit(int id)
    {
      var employee = this.employeeService.Find(id);
      if (employee == null)
      {
        return this.HttpNotFound();
      }
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      this.ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n => n.Name), "Id", "Name", employee.CompanyId);
      return this.View(employee);
    }
    //POST: Employees/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(Employee employee)
    {
      if (employee == null)
      {
        throw new ArgumentNullException(nameof(employee));
      }
      if (this.ModelState.IsValid)
      {
        employee.TrackingState = TrackingState.Modified;
        try
        {
          this.employeeService.Update(employee);

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

        //DisplaySuccessMessage("Has update a Employee record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return this.Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      //return View(employee);
    }
    //删除当前记录
    //GET: Employees/Delete/:id
    [HttpGet]
    public async Task<ActionResult> DeleteAsync(int id)
    {
      try
      {
        await this.employeeService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        await this.employeeService.Queryable().Where(x => id.Contains(x.Id)).DeleteAsync();
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
    public ActionResult ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "employees_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = this.employeeService.ExportExcel(filterRules, sort, order);
      return this.File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => this.TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => this.TempData["ErrorMessage"] = msgText;

  }
}
