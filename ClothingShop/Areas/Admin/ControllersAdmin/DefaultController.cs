using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothingShop.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["Account"] = null;
            Session["GioHang"] = null;
            return Redirect("~/Home/Index");
        }
    }
}