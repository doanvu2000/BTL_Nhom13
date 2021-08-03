using BTL_Nhom13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity;

namespace BTL_Nhom13.Controllers
{
    public class HomeController : Controller
    {
        TinhDauDB db = new TinhDauDB();
        public ActionResult Index(string sortOrder, int? madm, int? beginPrice, int? endPrice,string searchString,int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var sp = db.SanPhams.Select(s=>s).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.TenSP.ToLower().Contains(searchString.ToLower())).ToList();
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
            if (madm > 0)
            {
                sp = sp.Where(s => s.MaDM == madm).ToList();
                return View(sp.Where(s => s.MaDM == madm).ToPagedList(pageNumber, pageSize));
            }
            if (sortOrder != null)
            {
                switch (sortOrder)
                {
                    case "sortSL":
                        sp = sp.OrderBy(s => s.SoLuongTon).ToList(); break;
                    case "giaTang":
                        sp = sp.OrderBy(s => s.Gia).ToList(); break;
                    case "giaGiam":
                        sp = sp.OrderByDescending(s => s.Gia).ToList(); break;
                }
            }
            if (beginPrice > 0 && endPrice == 0)
            {
                sp = sp.Where(s => s.Gia <= beginPrice).OrderBy(s => s.Gia).ToList();
            }
            if (beginPrice > 0 && endPrice > 0)
            {
                sp = sp.Where(s => s.Gia >= beginPrice && s.Gia <= endPrice).OrderBy(s => s.Gia).ToList();
            }
            if (beginPrice == 0 && endPrice > 0)
            {
                sp = sp.Where(s => s.Gia >= endPrice).OrderBy(s => s.Gia).ToList();
            }
            return View(sp.ToPagedList(pageNumber, pageSize));
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
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoans.Where(t => t.TenTaiKhoan.Equals(TenTaiKhoan) && t.MatKhau.Equals(MatKhau) && t.Quyen == 0).ToList();
                if (user.Count() > 0)
                {
                    
                    if(user.FirstOrDefault().TinhTrang == false)
                    {
                        // Hien thi thong bao loi
                        ViewBag.error = "Tài khoản bị khóa. Đăng nhập không thành công";
                    } else
                    {
                        //Su dung session: add Session
                        Session["TaiKhoan"] = user.FirstOrDefault();
                        Session["TenKhachHang"] = user.FirstOrDefault().TenKhachHang;
                        Session["TenTaiKhoan"] = user.FirstOrDefault().TenTaiKhoan;
                        // Sang trang ch
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Signin()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin([Bind(Include = "TenTaiKhoan,MatKhau,Quyen,TinhTrang,TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var taiKhoanFind = db.TaiKhoans.Find(taiKhoan.TenTaiKhoan);
                if(taiKhoanFind == null)
                {
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                } else
                {
                    ViewBag.ErrorSign = "Tên tài khoản trùng. Vui lòng nhập tên khác";
                }
            }
            //ViewBag.Infor = taiKhoan.ToString();
            return View(taiKhoan);
        }
        [HttpGet]
        public ActionResult DetailAccount()
        {
            if(Session["TenTaiKhoan"] == null)
            {
                return RedirectToAction("Login");
            } else
            {
                TaiKhoan taiKhoan = (TaiKhoan)Session["TaiKhoan"];
                return View(taiKhoan);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailAccount([Bind(Include = "TenTaiKhoan,MatKhau,Quyen,TinhTrang,TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Cập nhật thông tin thành công";
            } else
            {
                ViewBag.Message = "Cập nhật thông tin thất bại";
            }
            return View(taiKhoan);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult DetailProduct(int? masp)
        {
            if (masp == 0)
            {
                return View("Index", db.SanPhams.ToList().ToPagedList(1, 10));
            }
            SanPham sp = db.SanPhams.Find(masp);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
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
        public PartialViewResult _SP_BanChay()
        {
            var sp = db.SanPhams.Select(d => d).OrderBy(s => s.Gia).Take(3);
            return PartialView(sp);
        }
        public ActionResult GioHang()
        {
            
            return View();
        }
        public ActionResult DatHang()
        {
            List<SanPhamDTO> ListSP = new List<SanPhamDTO>();
            if (Session["GioHang"] != null)
            {
                ListSP = (List<SanPhamDTO>)Session["GioHang"];
            }
            string tk = "";
            if (Session["TenTaiKhoan"] != null)
            {
                tk = Session["TenTaiKhoan"] as string;
            }
            if (tk==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                List<GioHang> gh = db.GioHangs.ToList();
                int magh = -1;
                foreach (var item in gh)
                {
                    if (item.TenTaiKhoan == tk)
                    {
                        magh = item.MaGioHang;
                    }
                }
                if (magh == -1)
                {
                    GioHang giohang = new GioHang();
                    giohang.TenTaiKhoan = tk;
                    db.GioHangs.Add(giohang);
                    db.SaveChanges();
                    gh = db.GioHangs.ToList();
                    foreach (var item in gh)
                    {
                        if (item.TenTaiKhoan == tk)
                        {
                            magh = item.MaGioHang;
                        }
                    }

                    foreach (var item in ListSP)
                    {
                        ChiTietGioHang ct = new ChiTietGioHang();
                        ct.MaGioHang = magh;
                        ct.MaSP = item.MaSP;
                        ct.SoLuongMua = item.SoLuongMua;
                        ct.Gia = item.Gia;
                        db.ChiTietGioHangs.Add(ct);
                        db.SaveChanges();
                    }
                    HoaDon hd = new HoaDon();
                    hd.NgayDat = DateTime.Now;
                    hd.TinhTrang = "Chờ xác nhận";
                    hd.PhiShip = 15000;
                    hd.GhiChu = "Thanh toán khi nhận hàng";
                    hd.MaGioHang = Convert.ToInt32(gh);
                    db.HoaDons.Add(hd);
                    db.SaveChanges();
                }
                else
                {
                    foreach (var item in ListSP)
                    {
                        ChiTietGioHang ct = new ChiTietGioHang();
                        ct.MaGioHang = magh;
                        ct.MaSP = item.MaSP;
                        ct.SoLuongMua = item.SoLuongMua;
                        ct.Gia = item.Gia;
                        db.ChiTietGioHangs.Add(ct);
                        db.SaveChanges();
                    }
                    HoaDon hd = new HoaDon();
                    hd.NgayDat = DateTime.Now;
                    hd.TinhTrang = "Chờ xác nhận";
                    hd.PhiShip = 15000;
                    hd.GhiChu = "Thanh toán khi nhận hàng";
                    hd.MaGioHang = magh;
                    db.HoaDons.Add(hd);
                    db.SaveChanges();
                }
            }
            Session["GioHang"] = null;
            Session["SoLuongSPGioHang"] = null;
            return RedirectToAction("Home");
        }
        public PartialViewResult _CT_GioHang()
        {
            List<SanPhamDTO> listSP = new List<SanPhamDTO>();
            if (Session["GioHang"] != null)
            {
                listSP = (List<SanPhamDTO>)Session["GioHang"];
            }
            return PartialView(listSP);
        }
        public PartialViewResult _DC_GiaoHang()
        {
            TaiKhoan tk = (TaiKhoan)Session["TaiKhoan"];
            if (tk == null)
            {
                return PartialView();
            }
            else
            {
                return PartialView(tk);
            }
        }
        public ActionResult Home(string sortOrder, int? madm, int? beginPrice, int? endPrice, string searchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var sp = db.SanPhams.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.TenSP.ToLower().Contains(searchString.ToLower())).ToList();
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
            if (madm > 0)
            {
                sp = sp.Where(s => s.MaDM == madm).ToList();
            }
            if (sortOrder != null)
            {
                switch (sortOrder)
                {
                    case "sortSL":
                        sp = sp.OrderBy(s => s.SoLuongTon).ToList(); break;
                    case "giaTang":
                        sp = sp.OrderBy(s => s.Gia).ToList(); break;
                    case "giaGiam":
                        sp = sp.OrderByDescending(s => s.Gia).ToList(); break;
                }
            }
            if (beginPrice > 0 && endPrice == 0)
            {
                sp = sp.Where(s => s.Gia <= beginPrice).OrderBy(s => s.Gia).ToList();
            }
            if (beginPrice > 0 && endPrice > 0)
            {
                sp = sp.Where(s => s.Gia >= beginPrice && s.Gia <= endPrice).OrderBy(s => s.Gia).ToList();
            }
            if (beginPrice == 0 && endPrice > 0)
            {
                sp = sp.Where(s => s.Gia >= endPrice).OrderBy(s => s.Gia).ToList();
            }
            return View(sp.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult About()
        {
            return View();
        }
    }
}