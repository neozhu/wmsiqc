using System.Web.Mvc;

namespace WebApp.Areas.AW
{
    public class WorkAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AW";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AW_default",
                "AW/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}