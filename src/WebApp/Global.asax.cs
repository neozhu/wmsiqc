using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SqlHelper2;
using WebApp.Models;


namespace WebApp
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
   
    }

    protected void Application_Error()
    {
      var exception = this.Server.GetLastError();

      if (EFMigrationsManagerConfig.HandleEFMigrationException(exception, this.Server, this.Response, this.Context))
      {
        return;
      }

      var message = exception.Message;
      var stackTrace = exception.StackTrace;
      var Source = exception.Source;

      var httpContext = HttpContext.Current;
      var controllerName = string.Empty;
      var actionName = string.Empty;
      var messageTag = string.Empty;
      var code = string.Empty;
     
      if (exception is HttpException)
      {
        code = $"{( exception as HttpException)?.ErrorCode}:{( exception as HttpException )?.WebEventCode}";
      }
      if (httpContext.CurrentHandler is MvcHandler)
      {
        var requestContext = ( (MvcHandler)httpContext.CurrentHandler ).RequestContext;
        controllerName = requestContext.RouteData.GetRequiredString("controller");
        actionName = requestContext.RouteData.GetRequiredString("action");
        /* when the request is ajax the system can automatically handle a mistake with a JSON response. then overwrites the default response */
        if (requestContext.HttpContext.Request.IsAjaxRequest())
        {
          messageTag = "Ajax";
        }
        else
        {
          messageTag = "Mvc";
        }
      }
      DatabaseFactory.CreateDatabase().ExecuteSPNonQuery("[dbo].[SP_InsertMessages]",
          new
          {
            Group = MessageGroup.System,
            ExtensionKey1 = "",
            Type = MessageType.Error,
            Content = message,
            Tags = messageTag,
            Code = code,
            Method = Source,
            StackTrace = stackTrace,
            User = this.User?.Identity?.Name
          }
          );
    }
  }
}
