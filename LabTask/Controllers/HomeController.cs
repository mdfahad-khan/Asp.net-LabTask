using LabTask.EF;
using LabTask.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Product_catagories.Models;
using Product_catagories.Auth;


namespace LabTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Logged]
        [HttpGet]
        public ActionResult AddProduct()
        {
            var db = new ShopEntities();
            ViewBag.Catagory = db.Catagories.ToList();
            var products = db.Products.ToList();
            return View(products);
        }
        [Logged]
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            var db = new ShopEntities();
            db.Products.Add(p);
            db.SaveChanges();

            return RedirectToAction("Product");

        }

        [HttpGet]
        public ActionResult EditCatagory(int Id)
        {
            var db = new ShopEntities();
            var data = db.Catagories.Find(Id);
            return View(data);
        }
        [HttpPost]
        public ActionResult EditCatagory(Catagory d)
        {
            var db = new ShopEntities();
            var ex = db.Catagories.Find(d.Id);
            ex.Name = d.Name;
            db.SaveChanges();
            return RedirectToAction("Product");
        }

        [Logged]
        [HttpGet]
        public ActionResult EditProduct(int Id)
        {
            var db = new ShopEntities();
            var data = db.Products.Find(Id);
            ViewBag.Catagory = db.Catagories.ToList();
            return View(data);
        }
        [Logged]
        [HttpPost]
        public ActionResult EditProduct(Product d)
        {
            var db = new ShopEntities();
            var ex = db.Products.Find(d.Id);
            ex.Name = d.Name;
            ex.Price = d.Price;
            ex.Catagory = d.Catagory;
            ex.Quantity = d.Quantity;
            db.SaveChanges();
            return RedirectToAction("Product", "Home");
        }
        [Logged]
        [HttpGet]
        public ActionResult Product()
        {
            var db = new ShopEntities();
            var data = db.Products.ToList();
            ViewBag.Catagory = db.Catagories.ToList();
            ViewBag.Orders = db.Orders.ToList();
            ViewBag.productOrders = db.ProductOrders.ToList();

            //ViewBag.pendingOrders = db.Orders.Where(o => o.Status == "Pending").ToList();

            //var orders = (from o in db.orders
            //              select new
            //              {
            //                  id = o.id,
            //                  customer_name = o.customer_name,
            //                  status = o.status,
            //                  // add other fields as needed
            //              }).tolist();

            //// pass orders to the view
            //viewbag.orders = orders;

            //var customers = db.Users.Where(u => u.Usertype == "Customer").ToList();
            //ViewBag.Customers = customers;
            return View(data);
        }
        [Logged]
        public ActionResult AcceptOrder(int Id)
        {
            var db = new ShopEntities();         
                var data = db.ProductOrders.Find(Id);
                var product = db.Products.ToList();

                if (data != null)
                {
                    data.Status = "Accetpted";
                    db.SaveChanges();
                    foreach (var item in product)
                    {
                        if (data.P_Id == item.Id)
                        {
                            item.Quantity--;
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Product");    
        }
        [Logged]
        public ActionResult RejectOrder(int Id)
        {
            var db = new ShopEntities();

            var data = db.ProductOrders.Find(Id);
            var product = db.Products.ToList();

            if (data != null)
            {
                data.Status = "Rejected";
                db.SaveChanges();
            }
            return RedirectToAction("Product");
        }
        [HttpGet]
        public ActionResult Catagory()
        {
            var db = new ShopEntities();
            var data = db.Catagories.ToList();
            return View(data);
        }


        [HttpGet]
        public ActionResult AddCatagory()
        {
            return View();
        }
        [Logged]
        [HttpPost]
        public ActionResult AddCatagory(Catagory p)
        {
            var db = new ShopEntities();
            db.Catagories.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        [Logged]
        [HttpGet]
        public ActionResult deleteCatagory(int Id)
        {
            var db = new ShopEntities();
            var data = db.Catagories.Find(Id);
            db.Catagories.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Product", "Home");

        }
        [Logged]
        [HttpGet]
        public ActionResult deleteProduct(int Id)
        {
            var db = new ShopEntities();
            var data = db.Products.Find(Id);
            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Product", "Home");
        }
        [Logged]
        [HttpGet]
        public ActionResult ProductList()
        {
            var db = new ShopEntities();
            var products = db.Products.ToList();
            ViewBag.Catagory = db.Catagories.ToList();


            return View(products);
        }
        [Logged]

        [HttpPost]
        public ActionResult ProductList(FormCollection Form)
        {
            var db = new ShopEntities();
            ViewBag.Catagory = db.Catagories.ToList();
            ViewBag.Product = db.Products.ToList();
            Session["SelectedProductIds"] = "";

            string[] selectedProductIds = Form.GetValues("selectedProducts");

            if (selectedProductIds != null)
            {
                // Store the selected product IDs in the session
                Session["SelectedProductIds"] = selectedProductIds;
            }
           

            return RedirectToAction("Cart");
        }


        [Logged]
        [HttpGet]

        public ActionResult Cart()
        {
            string[] selectedProductIds = Session["SelectedProductIds"] as string[];

            if (selectedProductIds != null)
            {
                var db = new ShopEntities();
                var selectedProductIdsInt = selectedProductIds.Select(id => int.Parse(id)).ToArray();
                var selectedProducts = db.Products.Where(p => selectedProductIdsInt.Contains(p.Id)).ToList();
                ViewBag.SelectedProducts = selectedProducts;
                ViewBag.Catagory = db.Catagories.ToList();

            }

            return View();
        }


        [Logged]
        [HttpPost]
        public ActionResult Cart(FormCollection Form)
        {
            var db = new ShopEntities();
            ViewBag.Catagory = db.Catagories.ToList();
            ViewBag.Product = db.Products.ToList();
            var orders = db.Orders.ToList();
            Order o = new Order();
            o.Customer_Name = Session["Name"].ToString();
            db.Orders.Add(o);
            db.SaveChanges();
            ProductOrder po = new ProductOrder();
            string[] selectedProductIds = Session["SelectedProductIds"] as string[];

            foreach (var item in selectedProductIds)
            {
                po.O_Id = o.Id;
                po.P_Id = int.Parse(item);
                po.Status = "Pending";
                db.ProductOrders.Add(po);
                //var product = db.Products.FirstOrDefault(p => p.Id == po.P_Id);
                //if (product != null)
                //{
                //        product.Quantity -= 1;
                //}
               
                db.SaveChanges();

            }
            return View(orders);
        }
        [Logged]

        //public ActionResult CustomerOrder()
        //{
        //    var name = Session["Name"].ToString();
        //    var db = new ShopEntities();
        //    ViewBag.Catagory = db.Catagories.ToList();

        //    var products = (from order in db.Orders
        //                    join productOrder in db.ProductOrders on order.Id equals productOrder.O_Id
        //                    join product in db.Products on productOrder.P_Id equals product.Id
        //                    where order.Customer_Name == name
        //                    select product).ToList();
        //    ViewBag.Orders = db.Orders.ToList();

        //    TempData["OrderStatus"] = "Pending";

        //    return View(products);
        //}
        [HttpGet]
        public ActionResult CustomerOrder()
        {
            //DELETE FROM[MyTable];
            //DBCC CHECKIDENT('[MyTable]', RESEED, 0);


            var db = new ShopEntities();

            var name = Session["Name"].ToString();

                var orderInfo = (from order in db.Orders
                                 where order.Customer_Name == name
                                 select order).ToList();
                return View(orderInfo);
            
        }
        [Logged]

        [HttpGet]
        public ActionResult OrderDetails(int id)
        {
            var db = new ShopEntities();
            var products = (from order in db.Orders
                            join productOrder in db.ProductOrders on order.Id equals productOrder.O_Id
                            join product in db.Products on productOrder.P_Id equals product.Id
                            where order.Id == id
                            select product).ToList();
            ViewBag.order = db.Orders.ToList();
            ViewBag.category = db.Catagories.ToList();
            
            ViewBag.productOrder = (from ProductOrder in db.ProductOrders where ProductOrder.O_Id == id select ProductOrder).ToList();

          
            return View(products);

        }
        [Logged]

        [HttpGet]
        public ActionResult CancleOrder(int id)
        {
            var db = new ShopEntities();
            var a = db.ProductOrders.Find(id);
            a.Status = "Cancelled";
            db.SaveChanges();
            return RedirectToAction("OrderDetails", new { id = a.O_Id });

        }
        [Logged]
        [HttpGet]
        public ActionResult AddSeller()
        {
            return View();
        }
        [Logged]
        [HttpPost]
        public ActionResult AddSeller(SellerDTO emp)
        {
           
                using (var db = new ShopEntities())
                {
                    // Create a new User
                    var user = new User
                    {
                        Username = emp.S_name,
                        Password = emp.Password,
                        Usertype = "Seller"
                    };

                    // Create a new Employee associated with the User
                    var seller = new Seller
                    {
                        S_name = emp.S_name,
                        S_address = emp.S_address,
                        S_phone = emp.S_phone,
                        S_gender = emp.S_gender,
                        // Associate the User with the Employee
                    };

                    // Add both User and Employee to the database
                    db.Users.Add(user);
                    db.Sellers.Add(seller);

                    // Save changes to the database
                    db.SaveChanges();

                    return RedirectToAction("Product", "Home");
                }
          
            return View(emp);
        }

        public User convert(SellerDTO s)
        {
            var st = new User()
            {
                Username = s.S_name,
                Password = s.Password,
                Usertype = "Seller"
            };
            return st;
        }









    }
}