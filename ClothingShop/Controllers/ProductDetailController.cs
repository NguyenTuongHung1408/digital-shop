using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothingShop.Models;

namespace ClothingShop.Controllers
{
    public class ProductDetailController : Controller
    {
        ClothingStore1Entities db = new ClothingStore1Entities(); /*Đại diện cho context của cơ sở dữ liệu
                                                                   Cho phép truy cập và tương tác với cơ sở dữ liệu thông qua 
                                                                   các bảng được định nghĩa trong mô hình của ứng dụng.*/

        // GET: ProductDetail
        public ActionResult Index(int id)
        {
            ViewBag.Product = db.Products.FirstOrDefault(p => p.ProductID == id); /*Lấy sản phẩm đầu tiên có ProductID trùng với id từ cơ sở dữ liệu 
                                                                                   * và gán nó cho ViewBag.Product. 
                                                                                   * ViewBag là một đối tượng động được sử dụng để truyền dữ liệu từ controller đến view 
                                                                                   * mà không cần định nghĩa một lớp cụ thể.*/


            ViewBag.ProdList = db.Products.ToList().Take(4); /*lấy tất cả các sản phẩm (thông qua db.Products.ToList()) 
                                                              * và sau đó sử dụng phương thức Take(4) để lấy 4 phần tử đầu tiên.*/
            return View();
        }
    }
}