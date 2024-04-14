using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Areas.AW.Controllers
{
    public class DefaultController : Controller
    {
        // GET: AW/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}