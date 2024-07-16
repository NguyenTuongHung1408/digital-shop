using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClothingShop.Models;

namespace ClothingShop.Areas.Admin.Controllers
{
    public class NSXController : Controller
    {
        private ClothingStore1Entities db = new ClothingStore1Entities();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.NSXes.ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NSX nsx = db.NSXes.Find(id);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDnsx,TenNSX")] NSX nsx)
        {
            if (ModelState.IsValid)
            {
                db.NSXes.Add(nsx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nsx);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NSX nsx = db.NSXes.Find(id);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDnsx,TenNSX")] NSX nsx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nsx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nsx);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NSX nSX = db.NSXes.Find(id);
            if (nSX == null)
            {
                return HttpNotFound();
            }
            return View(nSX);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NSX nsx = db.NSXes.Find(id);
            db.NSXes.Remove(nsx);
            db.SaveChanges();
            return RedirectToAction("Index");
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