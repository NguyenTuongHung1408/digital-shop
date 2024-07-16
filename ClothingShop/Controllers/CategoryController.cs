using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothingShop.Models;

namespace ClothingShop.Controllers
{
    
    public class CategoryController : Controller
    {
        ClothingStore1Entities db = new ClothingStore1Entities();
        // GET: Category
        public ActionResult Index()
        {
            var prodList = db.Products.ToList();
            return View(prodList);
        }

        public ActionResult ListProductCate(int id)
        {
            ViewBag.Category = db.Categories.FirstOrDefault(c => c.CategoryID == id).CategoryName;
            var prodList = db.Products.Where(p => p.CategoryID == id).ToList();
            return View(prodList);
        }
        public ActionResult ListProductNSX(int id)
        {
            ViewBag.NSX = db.NSXes.FirstOrDefault(c => c.IDnsx == id).TenNSX;
            var prodList = db.Products.Where(p => p.IDnsx == id).ToList();
            return View(prodList);
        }
        public ActionResult Search(string inputSearch)
        {
            var result = db.Products.Where(s => s.ProductName.Contains(inputSearch)).ToList();

            return View(result);
        }
    }
}