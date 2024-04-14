using System.Web.Mvc;

namespace WebApp.Areas.AL
{
    public class ALAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AL_default",
                "ALS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebApp.Areas.AL.Controllers" }
            );
        }
    }
}