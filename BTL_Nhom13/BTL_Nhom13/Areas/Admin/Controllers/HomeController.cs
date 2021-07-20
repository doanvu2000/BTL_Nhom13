using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom13.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult Receipt()
        {
            return View();
        }
        public ActionResult Account()
        {
            return View();
        }
    }
}