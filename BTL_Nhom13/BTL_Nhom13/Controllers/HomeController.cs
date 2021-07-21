﻿using BTL_Nhom13.Models;
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

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Signin()
        {

            return View();
        }
        public ActionResult DetailProduct(int masp)
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
            var sp = db.SanPhams.Select(d => d).OrderBy(s => s.SoLuongTon).Take(3);
            return PartialView(sp);
        }
        public ActionResult GioHang()
        {
            return View();
        }
        public PartialViewResult _CT_GioHang()
        {
            int magh = 2;//mã giỏ hàng lấy được khi đăng nhập.
            var ctgh = db.ChiTietGioHangs.Where(gh=>gh.MaGioHang==magh).Select(gh=>gh).ToList();
            var sp = db.SanPhams.Select(s => s).ToList();
            List<GioTam> list = new List<GioTam>();
            foreach (var gh in ctgh)
            {
                foreach (var s in sp)
                {
                    if (gh.MaSP==s.MaSP)
                    {
                        GioTam gt = new GioTam();
                        gt.masp = s.MaSP;
                        gt.tensp = s.TenSP;
                        gt.anh = s.Anh;
                        gt.gia = Convert.ToInt32(s.Gia);
                        gt.sl = gh.SoLuongMua;
                        gt.tt = gt.gia * gt.sl;
                        list.Add(gt);
                    }
                }
            }
            return PartialView(list);
        }
        public PartialViewResult _DC_GiaoHang()
        {
            string tentk = "ngocnam";//tên tài khoản lấy đk khi đăng nhập.
            TaiKhoan tk = db.TaiKhoans.Find(tentk);
            if (tk == null)
            {
                return PartialView();
            }
            else
            {
                return PartialView(tk);
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