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
    public class AdminSanPhamsController : Controller
    {
        private TinhDauDB db = new TinhDauDB();

        public ActionResult Product(int? page)
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc);
            sanPhams = sanPhams.OrderBy(s => s.MaSP);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(sanPhams.ToPagedList(pageNumber, pageSize));
        }
        // GET: Admin/AdminSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/AdminSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM");
            return View();
        }

        // POST: Admin/AdminSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaDM,TenSP,NhaSX,TrongLuong,SoLuongTon,Gia,ChatLuong,MoTa,Anh")] SanPham sanPham)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    sanPham.Anh = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/wwwroot/image/" + FileName);
                        f.SaveAs(UploadPath);
                        sanPham.Anh = FileName;
                    }
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                }
                return RedirectToAction("Product");
            } catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                return View(sanPham);
            }
        }

        // GET: Admin/AdminSanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
            return View(sanPham);
        }

        // POST: Admin/AdminSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaDM,TenSP,NhaSX,TrongLuong,SoLuongTon,Gia,ChatLuong,MoTa,Anh")] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/wwwroot/image/" + FileName);
                        f.SaveAs(UploadPath);
                        sanPham.Anh = FileName;
                    }
                    /*else
                    {
                        string UploadPath = Server.MapPath("~/wwwroot/image/" + sanPham.Anh);
                    }*/
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Product");
            } catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                return View(sanPham);
            }
            /*ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
            return View(sanPham);*/
        }

        // GET: Admin/AdminSanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/AdminSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);

            try
            {
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
                return RedirectToAction("Product");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể xóa sản phẩm này, Mã lỗi: " + ex.Message;
                return View(sanPham);
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
