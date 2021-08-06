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
            if (Session["TenTaiKhoan"] == null)
            {
                return RedirectToAction("Login");
            }
            var products = db.SanPhams.ToList();
            var categories = db.DanhMucs.ToList();
            var receipts = db.HoaDons.ToList();
            DateTime today = DateTime.Today;
            List<HoaDon> hds = db.HoaDons.Where(h => h.NgayDat.Month == today.Month &&
                    h.NgayDat.Year == today.Year && h.TinhTrang.Equals("Đã giao")).ToList();
            decimal tongTienNum = 0;
            foreach (var item in hds)
            {
                tongTienNum += item.GioHang.ChiTietGioHangs.Select(c => c.SoLuongMua * c.Gia).Sum();
            }

            List<HoaDon> hd_trong_nam = hds.Where(h => h.NgayDat.Year == today.Year
                    && h.TinhTrang.Equals("Đã giao")).ToList();
            decimal tongTienTrongNamNum = 0;
            foreach (var item in hd_trong_nam)
            {
                tongTienTrongNamNum += item.GioHang.ChiTietGioHangs.Select(c => c.SoLuongMua * c.Gia).Sum();
            }

            var accounts = db.TaiKhoans.Where(t => t.Quyen == 0).ToList();

            ViewBag.soLuongSp = products.Count;
            ViewBag.soLuongDm = categories.Count;
            ViewBag.soLuongDh = hds.Count;
            ViewBag.soLuongTk = accounts.Count;
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            string tongTien = string.Format("{0:0,000}", tongTienNum);
            string tongTienTrongNam = string.Format("{0:0,000}", tongTienTrongNamNum);
            ViewBag.tongTienThangNay = tongTien;
            ViewBag.tongTienTrongNam = tongTienTrongNam;

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
            t.MatKhau.Equals(MatKhau) && t.TinhTrang == true).ToList();
            if (user.Count() > 0)
            {
                if (user.First().Quyen == 0)
                {
                    ViewBag.FailedMessage = "Thông tin đăng nhập không chính xác!";
                    return View();
                }
                Session["TenTaiKhoan"] = user.FirstOrDefault().TenTaiKhoan;
                if (user.First().Quyen == 1)
                {
                    Session["Quyen"] = "Admin";

                }
                if (user.First().Quyen == 2)
                {
                    Session["Quyen"] = "Employee";

                }
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
            Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public ActionResult ChangePassword(string tenTK)
        {
            var account = db.TaiKhoans.Find(tenTK);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "TenTaiKhoan,MatKhau,Quyen,TinhTrang,TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                }
                if(Session["Quyen"].Equals("Admin"))
                    return RedirectToAction("Account", "AdminTaiKhoans");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(taiKhoan);
            }
        }
    }
}