using BTL_Nhom13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom13.Controllers
{
    public class HomeController : Controller
    {
        TinhDauDB db = new TinhDauDB();
        public ActionResult Index()
        {
            var sp = db.SanPhams.Select(s => s);
            return View(sp);
        }
<<<<<<< HEAD
=======

>>>>>>> 672f420d0f97dcd04773cc8b10ed9494048a8931
        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Signin()
        {

            return View();
        }
        public PartialViewResult _DanhMuc()
        {
            var danhmuc = db.DanhMucs.Select(d => d);
            return PartialView(danhmuc);
        }
        public PartialViewResult _SearchDanhMuc()
        {
            var danhmuc = db.DanhMucs.Select(d => d);
            return PartialView(danhmuc);
        }
    }
}