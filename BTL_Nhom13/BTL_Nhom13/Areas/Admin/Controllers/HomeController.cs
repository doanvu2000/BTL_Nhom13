using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_Nhom13.Models;

namespace BTL_Nhom13.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        TinhDauDB db = new TinhDauDB();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if(Session["TenTaiKhoan"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string TenTaiKhoan, string MatKhau)
        {
            if (string.IsNullOrEmpty(TenTaiKhoan))
            {
                ViewBag.ErrorTenTaiKhoan = "Tên tài khoản không được để trống";
            }
            if (string.IsNullOrEmpty(TenTaiKhoan))
            {
                ViewBag.ErrorMatKhau = "Mật khẩu không được để trống";
            }

            var user = db.TaiKhoans.Where(t => t.TenTaiKhoan.Equals(TenTaiKhoan) && 
            t.MatKhau.Equals(MatKhau) && t.Quyen == 1 && t.TinhTrang == true).ToList();
            if (user.Count() > 0)
            {
                Session["TenTaiKhoan"] = user.FirstOrDefault().TenTaiKhoan;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.FailedMessage = "Thông tin đăng nhập không chính xác!";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("TenTaiKhoan");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}