using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using WebApp.Models;
using WebApp.Services;

namespace WebApp
{
  /// <summary>
  /// Specifies the Unity configuration for the main container.
  /// </summary>
  public static class WebApiUnityConfig
  {
    #region Unity Container
    private static Lazy<IUnityContainer> container =
      new Lazy<IUnityContainer>(() =>
      {
        var container = new UnityContainer();
        RegisterTypes(container);
        return container;
      });

    /// <summary>
    /// Configured Unity Container.
    /// </summary>
    public static IUnityContainer Container => container.Value;
    #endregion

    /// <summary>
    /// Registers the type mappings with the Unity container.
    /// </summary>
    /// <param name="container">The unity container to configure.</param>
    /// <remarks>
    /// There is no need to register concrete types such as controllers or
    /// API controllers (unless you want to change the defaults), as Unity
    /// allows resolving a concrete type even if it was not previously
    /// registered.
    /// </remarks>
    public static void RegisterTypes(IUnityContainer container)
    {
      // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
      // container.LoadConfiguration();

      // TODO: Register your types here
      //container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new HierarchicalLifetimeManager());
      //container.RegisterType<IDataContextAsync, StoreContext>(new HierarchicalLifetimeManager());
  
      container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager(), null);
      container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager(), null);

      //container.RegisterType<IRoleStore<ApplicationRole, string>, RoleStore<ApplicationRole>>(new HierarchicalLifetimeManager(), null);
      //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
      //container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
      container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager(), null);
      container.RegisterType<DbContext, StoreContext>(new PerRequestLifetimeManager(), null);
      container.RegisterInstance<HttpConfiguration>(GlobalConfiguration.Configuration);

      container.RegisterType<IRepositoryAsync<DataTableImportMapping>, Repository<DataTableImportMapping>>();
      container.RegisterType<IDataTableImportMappingService, DataTableImportMappingService>();


      container.RegisterType<IRepositoryAsync<Employee>, Repository<Employee>>();
      container.RegisterType<IEmployeeService, EmployeeService>();
      container.RegisterType<IRepositoryAsync<Company>, Repository<Company>>();
      container.RegisterType<ICompanyService, CompanyService>();
      container.RegisterType<IRepositoryAsync<Department>, Repository<Department>>();
      container.RegisterType<IDepartmentService, DepartmentService>();

      container.RegisterType<IRepositoryAsync<BaseCode>, Repository<BaseCode>>();
      container.RegisterType<IBaseCodeService, BaseCodeService>();
      container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();
      container.RegisterType<IMenuItemService, MenuItemService>();
      container.RegisterType<IRepositoryAsync<RoleMenu>, Repository<RoleMenu>>();
      container.RegisterType<IRoleMenuService, RoleMenuService>();

      container.RegisterType<IRepositoryAsync<DataTableImportMapping>, Repository<DataTableImportMapping>>();
      container.RegisterType<IDataTableImportMappingService, DataTableImportMappingService>();

      container.RegisterType<IRepositoryAsync<Notification>, Repository<Notification>>();
      container.RegisterType<INotificationService, NotificationService>();
      container.RegisterType<IRepositoryAsync<Message>, Repository<Message>>();
      container.RegisterType<IMessageService, MessageService>();


      container.RegisterType<IRepositoryAsync<CodeItem>, Repository<CodeItem>>();
      container.RegisterType<ICodeItemService, CodeItemService>();
    }
  }
}