using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothingShop.Models;

namespace ClothingShop.Controllers
{
    public class CartController : Controller
    {
        ClothingStore1Entities db = new ClothingStore1Entities();
        
        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as List<CartItem>;


            if (myCart == null)
            {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
            }
            return myCart;
        }


        public ActionResult AddToCart(FormCollection product)
        {
            List<CartItem> myCart = GetCart();

            int id = int.Parse(product["ProductID"]);
            int quantity = int.Parse(product["Quantity"]);

            CartItem Product = myCart.FirstOrDefault(p => p.ProductID == id);
            if (Product == null)
            {
                Product = new CartItem(id);
                Product.Number = quantity;
                myCart.Add(Product);
            }
            else
            {
                Product.Number += quantity; 
            }
            return RedirectToAction("GetCartInfo", "Cart");
        }

        public ActionResult AddSingleToCart(int id)
        {
            List<CartItem> myCart = GetCart();


            int quantity = 1;

            CartItem Product = myCart.FirstOrDefault(p => p.ProductID == id);
            if (Product == null)
            {
                Product = new CartItem(id);
                Product.Number = quantity;
                myCart.Add(Product);
            }
            else
            {
                Product.Number += quantity;
            }
            return RedirectToAction("GetCartInfo", "Cart");
        }




        public ActionResult Remove(int id)
        {
            List<CartItem> myCart = GetCart();
            CartItem Product = myCart.FirstOrDefault(p => p.ProductID == id);
            myCart.Remove(Product);
            return RedirectToAction("GetCartInfo", "Cart");
        }

        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalPrice = myCart.Sum(sp => sp.FinalPrice());
            return totalPrice;
        }
        private int GetTotalNumber()
        {
            int totalNumber = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalNumber = myCart.Sum(sp => sp.Number);
            return totalNumber;
        }

        public ActionResult GetCartInfo()
        {
            List<CartItem> myCart = GetCart();
            
            if (myCart == null || myCart.Count == 0)
            {
                return RedirectToAction("NullCart", "Cart");
            }
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart); 
        }

        public ActionResult Update(FormCollection product)
        {
            int id = int.Parse(product["ProductID"]);
            int quantity = int.Parse(product["Quantity"]);

            List<CartItem> myCart = GetCart();
            CartItem Product = myCart.FirstOrDefault(p => p.ProductID == id);
            Product.Number = quantity;
            return RedirectToAction("GetCartInfo", "Cart");
        }

        public ActionResult CartPartial()
        {
            ViewBag.TotalNumber = GetTotalNumber();

            return PartialView();
        }

        public ActionResult NullCart()
        {
            return View();
        }
        public ActionResult InsertOrder(string address)
        {
            DonHang order = new DonHang();
            var user = (Customer)Session["Account"];
            order.IDCus = user.IDCus;
            order.DateOrder = DateTime.Now.Date;
            order.Address = address;
            order.Status = 0;
            db.DonHangs.Add(order);
            db.SaveChanges();

            List<CartItem> cartItems = GetCart();
            foreach (var item in cartItems)
            {
                OrderDetail prod = new OrderDetail();
                prod.IDOrder = order.IDOrder;
                prod.ProductID = item.ProductID;
                prod.Quantity = item.Number;
                prod.UnitPrice = item.FinalPrice();
                db.OrderDetails.Add(prod);
                db.SaveChanges();
            }
            Session["GioHang"] = null;
            return RedirectToAction("Index/"+user.IDCus,"Order");
        }

    }
}