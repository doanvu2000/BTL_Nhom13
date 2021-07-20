using BTL_Nhom13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace BTL_Nhom13.Controllers
{
    public class HomeController : Controller
    {
        TinhDauDB db = new TinhDauDB();
        public ActionResult Index(string sortOrder, int? madm, int? beginPrice, int? endPrice,string searchString,int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var sp = db.SanPhams.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                sp = (List<SanPham>)sp.Where(s => s.TenSP.Contains(searchString) );
                return View(sp.ToList());
            }
            if (madm > 0)
            {
                sp = (List<SanPham>)sp.Where(s => s.MaDM == madm);
            }
            if (sortOrder != null)
            {
                switch (sortOrder)
                {
                    case "sortSL":
                        sp = (List<SanPham>)sp.OrderBy(s => s.SoLuongTon); break;
                    case "giaTang":
                        sp = (List<SanPham>)sp.OrderBy(s => s.Gia); break;
                    case "giaGiam":
                        sp = (List<SanPham>)sp.OrderByDescending(s => s.Gia); break;
                }
            }
            if (beginPrice > 0 && endPrice == 0)
            {
                sp = (List<SanPham>)sp.Where(s => s.Gia <= beginPrice).OrderBy(s => s.Gia);
            }
            if (beginPrice > 0 && endPrice > 0)
            {
                sp = (List<SanPham>)sp.Where(s => s.Gia >= beginPrice && s.Gia <= endPrice).OrderBy(s => s.Gia);
            }
            if (beginPrice == 0 && endPrice > 0)
            {
                sp = (List<SanPham>)sp.Where(s => s.Gia >= endPrice).OrderBy(s => s.Gia);
            }
            return View(sp.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Signin()
        {

            return View();
        }
<<<<<<< HEAD
=======
        public ActionResult DetailProduct()
        {
            return View();
        }
>>>>>>> 634691d (change-home)
        public PartialViewResult _DanhMuc()
        {
            var danhmuc = db.DanhMucs.Select(d => d);
            return PartialView(danhmuc);
        }
        public PartialViewResult _SearchDanhMuc()
        {
            var danhmuc = db.DanhMucs.Select(d => d);
            return PartialView(danhmuc);
<<<<<<< HEAD
        }
        public ActionResult DetailProduct()
        {
            return View();
=======
>>>>>>> 634691d (change-home)
        }
    }
}