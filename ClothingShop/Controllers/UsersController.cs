using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothingShop.Models;

namespace ClothingShop.Controllers
{
    public class UsersController : Controller
    {
        ClothingStore1Entities db = new ClothingStore1Entities();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login( Customer cus)
        {
            if (ModelState.IsValid)
            {
                if(cus.EmailCus == "admin@gmail.com" && cus.Password == "12345")
                   return RedirectToAction("Index", "Admin/Orders");
                if (ModelState.IsValid)
                {
                    var account = db.Customers.FirstOrDefault(k => k.EmailCus == cus.EmailCus && k.Password == cus.Password);
                    if (account != null)
                    {
                        Session["Account"] = account;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["Account"] = null;
            Session["GioHang"] = null ;
            return RedirectToAction("Login", "Users");
        }
        [HttpGet]

        public ActionResult SignUp()
        {
           
            return View();
        }
        [HttpPost]

        public ActionResult SignUp(FormCollection account)
        {
            string pass = account["PassCus"].ToString();
            string rePass = account["Repass"].ToString();
            string emailCus = account["EmailCus"].ToString();
            if(pass != rePass)
            {
                ViewBag.Notif = "Mật khẩu không khớp";
                return View();
            }

            var cus = db.Customers.FirstOrDefault(k => k.EmailCus == emailCus);
            if (cus != null)
            {
                ViewBag.Warning = "Đã có người đăng ký bằng email này";
                return View();

            }
            else
            {
                Customer customer = new Customer();
                customer.CustomerName = account["NameCus"].ToString() ;
                customer.EmailCus = account["EmailCus"].ToString();
                customer.Password = account["PassCus"].ToString();
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Login","Users");

            }
            
        }
    }
}