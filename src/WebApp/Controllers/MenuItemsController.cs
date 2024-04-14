///<summary>
///Provides functionality to the /MenuItem/ route.
///<date> 9/26/2018 5:56:36 PM </date>
///Create By SmartCode MVC5 Scaffolder for Visual Studio
///TODO: RegisterType UnityConfig.cs
///container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();
///container.RegisterType<IMenuItemService, MenuItemService>();
///
///Copyright (c) 2012-2018 neo.zhu
///Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
///and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
///</summary>
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
using WebApp.Repositories;
using WebApp.Services;
using Z.EntityFramework.Plus;
namespace WebApp.Controllers
{
  [Authorize]
  [RoutePrefix("MenuItems")]
  public class MenuItemsController : Controller
  {
    private readonly IMenuItemService menuItemService;
    private readonly IUnitOfWorkAsync unitOfWork;
    public MenuItemsController(IMenuItemService menuItemService, IUnitOfWorkAsync unitOfWork)
    {
      this.menuItemService = menuItemService;
      this.unitOfWork = unitOfWork;
    }
    //GET: MenuItems/Index
    //[OutputCache(Duration = 360, VaryByParam = "none")]
    [Route("Index", Name = "导航菜单管理", Order = 1)]
    public ActionResult Index() => this.View();
    //Get :MenuItems/GetData
    //For Index View datagrid datasource url
    [HttpGet]
    public async Task<JsonResult> GetDataAsync(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var totalCount = 0;
      //int pagenum = offset / limit +1;
      var pagerows = ( await this.menuItemService
                                 .Query(new MenuItemQuery().Withfilter(filters)).Include(m => m.Parent)
                                 .OrderBy(n => n.OrderBy(sort, order))
                                 .SelectPageAsync(page, rows, out totalCount) )
                                 .Select(n => new
                                 {

                                   ParentTitle = ( n.Parent == null ? "" : n.Parent.Title ),
                                   Id = n.Id,
                                   Title = n.Title,
                                   Description = n.Description,
                                   Code = n.Code,
                                   Url = n.Url,
                                   Controller = n.Controller,
                                   Action = n.Action,
                                   IconCls = n.IconCls,
                                   IsEnabled = n.IsEnabled,
                                   ParentId = n.ParentId
                                 }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return this.Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    public async Task<JsonResult> GetDataByParentIdAsync(int parentid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var totalCount = 0;
      var pagerows = ( await this.menuItemService
                 .Query(new MenuItemQuery().ByParentIdWithfilter(parentid, filters)).Include(m => m.Parent)
                 .OrderBy(n => n.OrderBy(sort, order))
                 .SelectPageAsync(page, rows, out totalCount) )
                 .Select(n => new
                 {

                   ParentTitle = ( n.Parent == null ? "" : n.Parent.Title ),
                   Id = n.Id,
                   Title = n.Title,
                   Description = n.Description,
                   Code = n.Code,
                   Url = n.Url,
                   Controller = n.Controller,
                   Action = n.Action,
                   IconCls = n.IconCls,
                   IsEnabled = n.IsEnabled,
                   ParentId = n.ParentId
                 }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return this.Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveDataAsync(MenuItemChangeViewModel menuitems)
    {
      if (menuitems == null)
      {
        throw new ArgumentNullException(nameof(menuitems));
      }
      if (this.ModelState.IsValid)
      {
        if (menuitems.updated != null)
        {
          foreach (var item in menuitems.updated)
          {
            this.menuItemService.Update(item);
          }
        }
        if (menuitems.deleted != null)
        {
          foreach (var item in menuitems.deleted)
          {
            this.menuItemService.Delete(item);
          }
        }
        if (menuitems.inserted != null)
        {
          foreach (var item in menuitems.inserted)
          {
            this.menuItemService.Insert(item);
          }
        }
        try
        {
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
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return this.Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
      }

    }
    //[OutputCache(Duration = 360, VaryByParam = "none")]
    public async Task<JsonResult> GetMenuItemsAsync(string q = "")
    {

      var menuitemRepository = this.unitOfWork.RepositoryAsync<MenuItem>();
      var rows = await menuitemRepository
                      .Queryable()
                      .Where(n => n.Title.Contains(q) && n.Controller == "#")
                      .OrderBy(n => n.Title)
                      .Select(n => new { Id = n.Id, Title = n.Title })
                      .ToListAsync();
      return this.Json(rows, JsonRequestBehavior.AllowGet);

    }
    //GET: MenuItems/Details/:id
    public ActionResult Details(int id)
    {

      var menuItem = this.menuItemService.Find(id);
      if (menuItem == null)
      {
        return this.HttpNotFound();
      }
      return this.View(menuItem);
    }
    //GET: MenuItems/GetItemAsync/:id
    [HttpGet]
    public async Task<JsonResult> GetItemAsync(int id)
    {
      var menuItem = await this.menuItemService.FindAsync(id);
      return this.Json(menuItem, JsonRequestBehavior.AllowGet);
    }
    //GET: MenuItems/Create
    public ActionResult Create()
    {
      var menuItem = new MenuItem();
      //set default value
      var menuitemRepository = this.unitOfWork.RepositoryAsync<MenuItem>();
      this.ViewBag.ParentId = new SelectList(menuitemRepository.Queryable().OrderBy(n => n.Title), "Id", "Title");
      return this.View(menuItem);
    }
    //POST: MenuItems/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(MenuItem menuItem)
    {
      if (menuItem == null)
      {
        throw new ArgumentNullException(nameof(menuItem));
      }
      if (this.ModelState.IsValid)
      {
        menuItem.TrackingState = TrackingState.Added;
        foreach (var item in menuItem.SubMenus)
        {
          item.ParentId = menuItem.Id;
          item.TrackingState = TrackingState.Added;
        }
        this.menuItemService.ApplyChanges(menuItem);
        try
        {
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
        //DisplaySuccessMessage("Has update a menuItem record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return this.Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var menuitemRepository = this.unitOfWork.RepositoryAsync<MenuItem>();
      //ViewBag.ParentId = new SelectList(await menuitemRepository.Queryable().OrderBy(n=>n.Title).ToListAsync(), "Id", "Title", menuItem.ParentId);
      //return View(menuItem);
    }
    //GET: MenuItems/PopupEdit/:id
    //[OutputCache(Duration = 360, VaryByParam = "id")]
    public async Task<JsonResult> PopupEditAsync(int id)
    {

      var menuItem = await this.menuItemService.FindAsync(id);
      return this.Json(menuItem, JsonRequestBehavior.AllowGet);
    }

    //GET: MenuItems/Edit/:id
    public ActionResult Edit(int id)
    {
      var menuItem = this.menuItemService.Find(id);
      if (menuItem == null)
      {
        return this.HttpNotFound();
      }
      var menuitemRepository = this.unitOfWork.RepositoryAsync<MenuItem>();
      this.ViewBag.ParentId = new SelectList(menuitemRepository.Queryable().OrderBy(n => n.Title), "Id", "Title", menuItem.ParentId);
      return this.View(menuItem);
    }
    //POST: MenuItems/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(MenuItem menuItem)
    {
      if (menuItem == null)
      {
        throw new ArgumentNullException(nameof(menuItem));
      }
      if (this.ModelState.IsValid)
      {
        menuItem.TrackingState = TrackingState.Modified;
        foreach (var item in menuItem.SubMenus)
        {
          item.ParentId = menuItem.Id;
          //set ObjectState with conditions
          if (item.Id <= 0)
            item.TrackingState = TrackingState.Added;
          else
            item.TrackingState = TrackingState.Modified;
        }

        this.menuItemService.ApplyChanges(menuItem);
        try
        {
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

        //DisplaySuccessMessage("Has update a MenuItem record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return this.Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var menuitemRepository = this.unitOfWork.RepositoryAsync<MenuItem>();
      //return View(menuItem);
    }
    //GET: MenuItems/Delete/:id
    public async Task<ActionResult> DeleteAsync(int id)
    {
      var menuItem = await this.menuItemService.FindAsync(id);
      if (menuItem == null)
      {
        return this.HttpNotFound();
      }
      return this.View(menuItem);
    }
    //POST: MenuItems/Delete/:id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      var menuItem = await this.menuItemService.FindAsync(id);
      this.menuItemService.Delete(menuItem);
      var result = await this.unitOfWork.SaveChangesAsync();
      if (this.Request.IsAjaxRequest())
      {
        return this.Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
      }
      this.DisplaySuccessMessage("Has delete a MenuItem record");
      return this.RedirectToAction("Index");
    }
    //Get Detail Row By Id For Edit
    //Get : MenuItems/EditMenuItem/:id
    [HttpGet]
    public async Task<ActionResult> EditMenuItem(int id)
    {
      var menuitemRepository = this.unitOfWork.RepositoryAsync<MenuItem>();
      var menuitem = await menuitemRepository.FindAsync(id);

      if (menuitem == null)
      {
        this.ViewBag.ParentId = new SelectList(await menuitemRepository.Queryable().OrderBy(n => n.Title).ToListAsync(), "Id", "Title");
        //return HttpNotFound();
        return this.PartialView("_MenuItemEditForm", new MenuItem());
      }
      else
      {
        this.ViewBag.ParentId = new SelectList(await menuitemRepository.Queryable().ToListAsync(), "Id", "Title", menuitem.ParentId);
      }
      return this.PartialView("_MenuItemEditForm", menuitem);
    }
    //Get Create Row By Id For Edit
    //Get : MenuItems/CreateMenuItem
    [HttpGet]
    public async Task<ActionResult> CreateMenuItemAsync()
    {
      var menuitemRepository = this.unitOfWork.RepositoryAsync<MenuItem>();
      this.ViewBag.ParentId = new SelectList(await menuitemRepository.Queryable().OrderBy(n => n.Title).ToListAsync(), "Id", "Title");
      return this.PartialView("_MenuItemEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : MenuItems/DeleteMenuItem/:id
    [HttpPost, ActionName("DeleteMenuItem")]
    public async Task<ActionResult> DeleteMenuItemConfirmedAsync(int id)
    {
      var menuitemRepository = this.unitOfWork.RepositoryAsync<MenuItem>();
      menuitemRepository.Delete(id);
      var result = await this.unitOfWork.SaveChangesAsync();
      if (this.Request.IsAjaxRequest())
      {
        return this.Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
      }
      this.DisplaySuccessMessage("Has delete a Order record");
      return this.RedirectToAction("Index");
    }

    //Get : MenuItems/GetSubMenusByParentId/:id
    [HttpGet]
    public async Task<JsonResult> GetSubMenusByParentIdAsync(int id)
    {
      var submenus = this.menuItemService.GetSubMenusByParentId(id);
      var data = await submenus.AsQueryable().ToListAsync();
      var rows = data.Select(n => new
      {

        ParentTitle = ( n.Parent == null ? "" : n.Parent.Title ),
        Id = n.Id,
        Title = n.Title,
        Description = n.Description,
        Code = n.Code,
        Url = n.Url,
        Controller = n.Controller,
        Action = n.Action,
        IconCls = n.IconCls,
        IsEnabled = n.IsEnabled,
        ParentId = n.ParentId
      });
      return this.Json(rows, JsonRequestBehavior.AllowGet);

    }
    [HttpPost]
    public ActionResult CreateWithController()
    {
      try
      {
        this.menuItemService.CreateWithController();
        this.unitOfWork.SaveChanges();
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
        await this.menuItemService.Queryable().Where(x => id.Contains(x.Id)).DeleteAsync();
        return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
      var fileName = "menuitems_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = this.menuItemService.ExportExcel(filterRules, sort, order);
      return this.File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => this.TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => this.TempData["ErrorMessage"] = msgText;

  }
}
