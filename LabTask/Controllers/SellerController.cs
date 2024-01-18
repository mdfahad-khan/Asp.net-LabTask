using LabTask.EF;
using Product_catagories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabTask.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult SellerDashBoard()
        {
            
            var db = new ShopEntities();
            ViewBag.Catagory = db.Catagories.ToList();
            var data = db.Products.ToList();
            return View(data);
        }
        [Logged]
        [HttpGet]
        public ActionResult SellerAddProduct()
        {
            var db = new ShopEntities();
            ViewBag.Catagory = db.Catagories.ToList();
            ViewBag.product = db.Products.ToList();
            var products = db.Products.ToList();
            return View(products);
        }
        [Logged]
        [HttpPost]
        public ActionResult SellerAddProduct(Product p)
        {
            var db = new ShopEntities();
            db.Products.Add(p);
            db.SaveChanges();

            return RedirectToAction("SellerDashBoard");

        }


        [Logged]
        [HttpGet]
        public ActionResult SellerEditProduct(int Id)
        {
            var db = new ShopEntities();
            var data = db.Products.Find(Id);
            ViewBag.Catagory = db.Catagories.ToList();
            return View(data);
        }
        [Logged]
        [HttpPost]
        public ActionResult SellerEditProduct(Product d)
        {
            var db = new ShopEntities();
            var ex = db.Products.Find(d.Id);
            ex.Name = d.Name;
            ex.Price = d.Price;
            ex.Catagory = d.Catagory;
            ex.Quantity = d.Quantity;
            db.SaveChanges();
            return RedirectToAction("SellerDashBoard", "Seller");
        }

        [Logged]
        [HttpGet]
        public ActionResult deleteProduct(int Id)
        {
            var db = new ShopEntities();
            var data = db.Products.Find(Id);
            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("SellerDashBoard", "Seller");
        }
    }
}