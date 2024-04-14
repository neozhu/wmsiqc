using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Areas.AL.Controllers
{
    public class DefaultController : Controller
    {
        // GET: AL/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}