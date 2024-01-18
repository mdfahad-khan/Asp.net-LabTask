using LabTask.EF;
using LabTask.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;


namespace LabTask.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Homepage()
        {
            return View();
        }
    

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        ////[HttpPost]
        ////public ActionResult Login(Product_catagories.Models.Login p)
        ////{
        ////    Session["Name"] = p.Name;
        ////    var db = new ShopEntities();


        ////    return RedirectToAction("ProductList", "Home");

        ////}

        //[HttpPost]
        //public ActionResult Login(Product_catagories.Models.Login p)
        //{
        //    var db = new ShopEntities(); // Make sure you have an instance of your data context (ShopEntities).
        //    var user = db.Users.FirstOrDefault(u => u.Username == p.Name && u.Password == p.Password);

        //    if (user != null)
        //    {
        //        Session["Name"] = p.Name;
        //        // 
        //        Session["UserType"] = user.Usertype; // Assuming "UserType" is a field in your User model.

        //        if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
        //        {
      
        //            return RedirectToAction("Product", "Home");
        //        }
        //        else if (Session["UserType"] != null && Session["UserType"].ToString() == "Customer")
        //        {
        //            return RedirectToAction("ProductList", "Home");
        //        }
        //    }

        //    // Handle login failure here, e.g., return to the login page with an error message.
        //    return View("Login");
        //}


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<LoginDTO, User>();
                });

                var mapper = new Mapper(config);
                var data = mapper.Map<User>(login);

                using (var db = new ShopEntities())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == data.Password);

                    if (user != null)
                    {
                        Session["Name"] = user.Username;

                        if (user.Usertype.Equals("Admin"))
                        {
                            return RedirectToAction("Product", "Home");
                        }
                        else if (user.Usertype.Equals("Customer"))
                        {
                            return RedirectToAction("ProductList", "Home");
                        }
                        else if (user.Usertype.Equals("Seller"))
                        {
                            return RedirectToAction("SellerDashboard", "Seller");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Username and password do not match.");
                    }
                }
            }

            return View(login);
        }


        public ActionResult Registration()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationDTO signUp)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ShopEntities())
                {
                    var user = new User
                    {
                        Username = signUp.C_name,
                        Password = signUp.Password,
                        Usertype = "Customer"
                    };

                    var details = new Customer_details
                    {
                        C_name = signUp.C_name,
                        C_address = signUp.C_address,
                        C_phone = signUp.C_phone,
                    };
                    db.Users.Add(user);
                    db.Customer_details.Add(details);
                    db.SaveChanges();

                    return RedirectToAction("Login", "Login");
                }
            }
            return View(signUp);
        }









    }
}