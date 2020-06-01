using E_CommerceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;

namespace E_CommerceProject.Controllers
{
    public class HomeController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());

        }

        public ActionResult Search(string searchBy, int temp=5, string search = "", string type = "")
        
        {
            List<Product> data;
            if (searchBy == "ByType")
            {
                data = db.Products.Where(p => p.Type == type).ToList();
            }
            else if (searchBy == "ByPrice")
            {
                if (temp == 1)
                {
                    data = db.Products.Where(p => p.Price >= 0 && p.Price <= 50).ToList();
                }
                else if (temp == 2)
                {
                    data = db.Products.Where(p => p.Price >= 50 && p.Price <= 100).ToList();
                }
                else if (temp == 3)
                {
                    data = db.Products.Where(p => p.Price >= 100 && p.Price <= 200).ToList();
                }
                else
                {
                    data = db.Products.Where(p => p.Price >= 250 && p.Price >= 0).ToList();
                }

            }
            else
            {
                data = db.Products.Where(p => p.Category.CategoryName.ToString() == search).ToList();
                if (data == null)
                {

                }
                else
                {
                    data = db.Products.Where(p => p.Name == search).ToList();

                }
            }

            return PartialView("_ShowProduct", data);
        }

        //public ActionResult SearchWithType(string type)
        //{
        //    List<Product> data;
        //    data= db.Products.Where(p=> p.Type == type).ToList();
        //    return PartialView("_ShowProduct",data);
        //}
        //public ActionResult SearchWithType(string type)
        //{
        //    List<Product> data;
        //    data = db.Products.Where(p => p.Type == type).ToList();
        //    return View("Shop", data);
        //}
        //public ActionResult SearchByPrice(int min, int max)
        //{
        //    List<Product> data;
        //    if(max==-1)
        //    {
        //        data = db.Products.Where(p => p.Price >= min).ToList();
        //    }
        //    else
        //    {
        //        data = db.Products.Where(p => p.Price >= min && p.Price <= max).ToList();
        //    }

        //    return View("Shop", data);
        //}
        //public ActionResult GeneralSearch(string search)
        //{

        //    List<Product> data = db.Products.Where(p => p.Category.CategoryName.ToString() == search).ToList();
        //    if (data == null)
        //    {
        //        return View("Index", data);
        //    }
        //    else
        //    {
        //        data = db.Products.Where(p => p.Name == search).ToList();
        //        return View("Index", data);
        //    }

        //}
        //public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        //{
        //    List<Product> productList = new List<Product>();
        //    if (SearchBy == "ID")
        //    {
        //        try
        //        {
        //            int Id = Convert.ToInt32(SearchValue);
        //            productList = db.Products.Where(x => x.ID == Id || SearchValue == null).ToList();
        //        }
        //        catch (FormatException)
        //        {
        //            Console.WriteLine("{0} Is Not A ID ", SearchValue);
        //        }
        //        return Json(productList, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        productList = db.Products.Where(x => x.Name.StartsWith(SearchValue) || SearchValue == null).ToList();
        //        return Json(productList, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult CartDetails()
        {
            return View();
        }


        [Authorize(Roles = "User")]
        public ActionResult ChecKOut()
        {
            return View();
        }





        
        public ActionResult ProductDetails(int? id)
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

         

        public ActionResult AddToCart(int productID, int caller = 1)
        {


            Product cModel = db.Products.FirstOrDefault(p => p.ID == productID);
            ProductDTO productDto = new ProductDTO()
            {
                ID = cModel.ID,
                Name = cModel.Name,
                Price = cModel.Price,
                Type = cModel.Type,
                QuantityInDataBase = cModel.Quantity,
                ProductImage = cModel.ProductImage,
                QuantityInOrder = 0,
                Categ = cModel.Category.CategoryName
            };

            if ((List<ProductDTO>)Session["cart"] == null && productDto.QuantityInDataBase > productDto.QuantityInOrder)
            {
                List<ProductDTO> cartList = new List<ProductDTO>();
                productDto.QuantityInOrder = 1;
                productDto.QuantityInDataBase -= 1;
                cartList.Add(productDto);
                Session["cart"] = cartList;
            }
            else if (!((List<ProductDTO>)Session["cart"] == null) && productDto.QuantityInDataBase > productDto.QuantityInOrder)
            {

                List<ProductDTO> cartList = (List<ProductDTO>)Session["cart"];

               if (cartList.Any(p => p.ID == productID))
                {

                    cartList.FirstOrDefault(p => p.ID == productID).QuantityInOrder += 1;
                    productDto.QuantityInDataBase -= 1;


                }
                else
                {
                    productDto.QuantityInOrder = 1;
                    productDto.QuantityInDataBase -= 1;

                   
                    cartList.Add(productDto);

                }

                Session["cart"] = cartList;
            }
            else
            {
                return View("CartDetails",new {ProdName= productDto.Name });
            }
                if (caller == 1)
                return RedirectToAction("CartDetails", new { ProdName = "EXIST" });
                 else
                return View("CartDetails", new { ProdName = "EXIST" });
           
             
        }
        public ActionResult RemoveFromCart(int productID)
        {

                List<ProductDTO> cartList = (List<ProductDTO>)Session["cart"];
                if (cartList.Any(p => p.ID == productID))
                {
                    cartList.FirstOrDefault(p => p.ID == productID).QuantityInOrder -= 1;
                }
            if (cartList.Find(p => p.ID == productID).QuantityInOrder==0)
            {
                cartList.Remove(cartList.Find(p => p.ID == productID));
            }
            Session["cart"] = cartList;
            
            return View("CartDetails");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ClearCart()
        {
            Session["Cart"] = null;
            return View("CartDetails");
        }

        public ActionResult Shop()
        {
            ViewBag.Message = "Your Shop page.";
            Session["Cats"] = db.Categories.ToList();
            return View(db.Products.ToList());
        }
        public ActionResult SaveOrder(string Address,string ZipCode)
        {
           
            Order order = new Order();
            order.DateOfOrder = DateTime.Now;
            order.Address = Address;
            order.ZipCode = ZipCode;
            order.PaymentType = "PayPal";
   
            double total = 0;


            Cart orderDetails;
            List<ProductDTO> cartList = (List<ProductDTO>)Session["cart"];
            foreach (var item in (List<ProductDTO>)Session["cart"])
            {
                if(item.QuantityInOrder <= item.QuantityInDataBase)
                { 
                orderDetails = new Cart();
                orderDetails.OrderID = order.ID;
                orderDetails.ProductID = item.ID;
                orderDetails.QuantityInOrder = item.QuantityInOrder;
                total += item.Price * item.QuantityInOrder;
                db.Products.FirstOrDefault(p => p.ID == item.ID).Quantity -= item.QuantityInOrder;
                order.UserID = db.Users.Where(u => u.UserName == this.User.Identity.Name).FirstOrDefault().Id;
                db.Carts.Add(orderDetails);
                }
            }

            order.AmountPaid = total;
            order.UserID = db.Users.Where(u => u.UserName == this.User.Identity.Name).FirstOrDefault().Id;
            db.Orders.Add(order);
            db.SaveChanges();
            //Session["Cart"] = cartList;
            //  this.User.Identity.Name
            Session["cart"] = null;
            return  RedirectToAction("ordercomplete" ,"Home");
        }

        public ActionResult ordercomplete()
        {
            return View();
        }
    }
}