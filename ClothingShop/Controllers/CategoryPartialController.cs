using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothingShop.Models;

namespace ClothingShop.Controllers
{
    public class CategoryPartialController : Controller
    {
        ClothingStore1Entities db = new ClothingStore1Entities();
        // GET: CategoryPartial
        public ActionResult PartialCate()
        {
            ViewBag.CategoryList = db.Categories.ToList();
            return PartialView();
        }

        public ActionResult NSX()
        {
            ViewBag.NsxList = db.NSXes.ToList();
            return PartialView();
        }
    }
}