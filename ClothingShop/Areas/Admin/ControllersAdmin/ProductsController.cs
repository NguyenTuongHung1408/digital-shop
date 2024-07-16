using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClothingShop.Models;
using System.IO;

namespace ClothingShop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ClothingStore1Entities db = new ClothingStore1Entities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "CategoryID", "CategoryName");
            ViewBag.IDnsx = new SelectList(db.NSXes.ToList(), "IDnsx", "TenNSX");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product, HttpPostedFileBase imageProduct)
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.IDnsx = new SelectList(db.NSXes, "IDnsx", "TenNSX", product.NSX);
            if (ModelState.IsValid)
            {
                if (imageProduct != null)
                {
                    var fileName = Path.GetFileName(imageProduct.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    product.ImagePro = fileName;
                    imageProduct.SaveAs(path);
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
            /* return RedirectToAction("Index");*/
            /*
                        if (imageProduct == null)
                        {
                            ViewBag.ThongBao = "Vui lòng chọn ảnh";
                            return View();
                        }
                        else
                        {
                            if (ModelState.IsValid)
                            {
                                //lấy tên file của hình được up lên
                                var filename = Path.GetFileName(imageProduct.FileName);

                                //Tạo đường dẫn đến File
                                var path = Path.Combine(Server.MapPath("~/Images"), filename);

                                //kiểm tra hình đã tồn tại trong hệ thống ?
                                if (System.IO.File.Exists(path))
                                {
                                    ViewBag.ThongBao = "Hình đã tồn tại";
                                }
                                else
                                {
                                    imageProduct.SaveAs(path);// Lưu vào hệ thống
                                }
                                product.ImagePro = filename;
                                db.Products.Add(product);
                                db.SaveChanges();

                            }
                            return RedirectToAction("Index");
                        }*/
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.IDnsx = new SelectList(db.NSXes, "IDnsx", "TenNSX", product.IDnsx);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CategoryID,ProductName,DecriptionPro,price,ImagePro,IDnsx")] Product product, HttpPostedFileBase imageProduct)
        {
            if (ModelState.IsValid)
            {
                if (imageProduct != null)
                {
                    var fileName = Path.GetFileName(imageProduct.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    product.ImagePro = fileName;
                    imageProduct.SaveAs(path);
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.IDnsx = new SelectList(db.NSXes, "IDnsx", "TenNSX", product.NSX);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
