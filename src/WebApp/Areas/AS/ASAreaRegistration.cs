using System.Web.Mvc;

namespace WebApp.Areas.AS
{
    public class ASAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AS_default",
                "AS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}