﻿using BTL_Nhom13.Models;
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
            var sp = db.SanPhams.Select(s => s).ToList();
            return View(sp);
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Signin()
        {

            return View();
        }
    }
}