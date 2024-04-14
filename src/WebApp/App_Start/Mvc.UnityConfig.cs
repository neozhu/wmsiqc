using System;
using System.Data.Entity;
using AutoMapper;
using LazyCache;
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
  public class MvcUnityConfig
  {
    #region Unity Container
    private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
    {
      var container = new UnityContainer();
      RegisterTypes(container);
      return container;
    });

    /// <summary>
    /// Configured Unity Container.
    /// </summary>
    public static IUnityContainer Container => container.Value;
    /// <summary>
    /// Gets the configured Unity container.
    /// </summary>
    public static IUnityContainer GetConfiguredContainer() => container.Value;
    #endregion

    /// <summary>Registers the type mappings with the Unity container.</summary>
    /// <param name="container">The unity container to configure.</param>
    /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
    /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
    public static void RegisterTypes(IUnityContainer container)
    {
      //注册automapper
      var config = new MapperConfiguration(cfg =>
      {
        //Create all maps here
        cfg.AddProfile(new AutoMapperProfile());
      });
      container.RegisterInstance(config.CreateMapper());
      container.RegisterType<IAppCache, CachingService>(new HierarchicalLifetimeManager(), new InjectionConstructor(CachingService.DefaultCacheProvider));

      container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager());
      container.RegisterType<DbContext, StoreContext>(new PerRequestLifetimeManager());


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
      container.RegisterType<IRepositoryAsync<CodeItem>, Repository<CodeItem>>();



      container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();
      container.RegisterType<IMenuItemService, MenuItemService>();
      container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();

      container.RegisterType<IRepositoryAsync<RoleMenu>, Repository<RoleMenu>>();
      container.RegisterType<IRoleMenuService, RoleMenuService>();
      container.RegisterType<IRepositoryAsync<RoleMenu>, Repository<RoleMenu>>();



      container.RegisterType<IRepositoryAsync<DataTableImportMapping>, Repository<DataTableImportMapping>>();
      container.RegisterType<IDataTableImportMappingService, DataTableImportMappingService>();

      container.RegisterType<IRepositoryAsync<Notification>, Repository<Notification>>();
      container.RegisterType<INotificationService, NotificationService>();
      container.RegisterType<IRepositoryAsync<Message>, Repository<Message>>();
      container.RegisterType<IMessageService, MessageService>();


      container.RegisterType<IRepositoryAsync<CodeItem>, Repository<CodeItem>>();
      container.RegisterType<ICodeItemService, CodeItemService>();
      container.RegisterType<IRepositoryAsync<RECEIPTDETAIL>, Repository<RECEIPTDETAIL>>();
      container.RegisterType<IRECEIPTDETAILService, RECEIPTDETAILService>();
      container.RegisterType<IRepositoryAsync<RECEIPT>, Repository<RECEIPT>>();
      container.RegisterType<IRECEIPTService, RECEIPTService>();
      container.RegisterType<IRepositoryAsync<STOCK>, Repository<STOCK>>();
      container.RegisterType<ISTOCKService, STOCKService>();
      container.RegisterType<IRepositoryAsync<PICKDETAIL>, Repository<PICKDETAIL>>();
      container.RegisterType<IPICKDETAILService, PICKDETAILService>();
      container.RegisterType<IRepositoryAsync<ORDERDETAIL>, Repository<ORDERDETAIL>>();
      container.RegisterType<IORDERDETAILService, ORDERDETAILService>();
      container.RegisterType<IRepositoryAsync<ORDER>, Repository<ORDER>>();
      container.RegisterType<IORDERService, ORDERService>();
      container.RegisterType<IRepositoryAsync<SUPPLIER>, Repository<SUPPLIER>>();
      container.RegisterType<ISUPPLIERService, SUPPLIERService>();
      container.RegisterType<IRepositoryAsync<STOCKGROUPVIEW>, Repository<STOCKGROUPVIEW>>();
      container.RegisterType<ISTOCKGROUPVIEWService, STOCKGROUPVIEWService>();
    }
  }
}
