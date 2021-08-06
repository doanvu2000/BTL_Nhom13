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
    public class AdminTaiKhoansController : Controller
    {
        private TinhDauDB db = new TinhDauDB();

        public ActionResult Account(int? page)
        {
            // select only admin account
            var taiKhoans = db.TaiKhoans.Where(t => t.Quyen == 1 || t.Quyen == 2).Select(t => t);
            taiKhoans = taiKhoans.OrderBy(t => t.TenTaiKhoan);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(taiKhoans.ToPagedList(pageNumber, pageSize));
        }
        // GET: Admin/AdminTaiKhoans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenTaiKhoan,MatKhau," +
            "Quyen,TinhTrang,TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan)
        {
            /*taiKhoan.Quyen = 1;*/
            try
            {
                if (ModelState.IsValid)
                {
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                }
                return RedirectToAction("Account");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(taiKhoan);
            }
        }

        // GET: Admin/AdminTaiKhoans/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Admin/AdminTaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);

            db.TaiKhoans.Remove(taiKhoan);
            db.SaveChanges();
            return RedirectToAction("Account");

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

        [HttpPost]
        public JsonResult ChangePassword(string id, string pass)
        {
            var account = db.TaiKhoans.Find(id);
            var result = account.MatKhau.Equals(pass);
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
