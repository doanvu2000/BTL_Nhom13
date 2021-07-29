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
    public class AdminDanhMucsController : Controller
    {
        private TinhDauDB db = new TinhDauDB();

        public ActionResult Category(int? page)
        {
            var danhMucs = db.DanhMucs.Select(d => d);
            danhMucs = danhMucs.OrderBy(s => s.MaDM);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(danhMucs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/AdminDanhMucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminDanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDM,TenDM")] DanhMuc danhMuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DanhMucs.Add(danhMuc);
                    db.SaveChanges();
                }
                return RedirectToAction("Category");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(danhMuc);
            }
        }

        // GET: Admin/AdminDanhMucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/AdminDanhMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDM,TenDM")] DanhMuc danhMuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(danhMuc).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Category");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(danhMuc);
            }
        }

        // GET: Admin/AdminDanhMucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/AdminDanhMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            try
            {
                db.DanhMucs.Remove(danhMuc);
                db.SaveChanges();
                return RedirectToAction("Category");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi! " + ex.Message;
                return View(danhMuc);
            }
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
