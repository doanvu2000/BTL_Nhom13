using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_Nhom13.Models;
using PagedList;

namespace BTL_Nhom13.Areas.Admin.Controllers
{
    public class AdminKhachHangsController : Controller
    {
        private TinhDauDB db = new TinhDauDB();

        // GET: Admin/AdminKhachHangs
        public ActionResult Customer(int? page)
        {
            // select only customer account
            var taiKhoans = db.TaiKhoans.Where(t => t.Quyen == 0).Select(t => t);
            taiKhoans = taiKhoans.OrderBy(t => t.TenTaiKhoan);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(taiKhoans.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/AdminKhachHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: Admin/AdminKhachHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/AdminKhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenTaiKhoan,MatKhau,Quyen,TinhTrang,TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Customer");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(taiKhoan);
            }
        }

        public bool toggleStatus(string id)
        {
            var user = db.TaiKhoans.Find(id);
            user.TinhTrang = !user.TinhTrang;
            db.SaveChanges();
            return user.TinhTrang;
        }

        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            var result = toggleStatus(id);
            return Json(new
            {
                status = result
            });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
