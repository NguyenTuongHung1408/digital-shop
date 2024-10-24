using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothingShop.Areas.Admin.ControllersAdmin
{
    public class ReportController : Controller
    {
        private ClothingStore1Entities db = new ClothingStore1Entities(); // Thay thế YourDbContext bằng context của bạn

        public ActionResult Report()
        {
            return View();
        }

        public JsonResult GetReportData()
        {
            var data = db.OrderDetails
                         .GroupBy(o => 1)
                         .Select(g => new ReportModel
                         {
                             Revenue = (decimal)g.Sum(o => o.UnitPrice),
                             TotalOrders = g.Count()
                         }).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // Model được đặt bên trong controller
        public class ReportModel
        {
            public decimal Revenue { get; set; }
            public int TotalOrders { get; set; }
        }
    }
}