using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using BurgerQ.Controllers.Base;
using BurgerQ.Models.Entity;
namespace BurgerQ.Controllers
{
    public class HomeController : LoginControllerBase
    {
        // GET: Home
        burgerqEntities db = new burgerqEntities();
        public ActionResult Index()
        {
         
            return View();
        }
        public ActionResult Login(string Email, string password)
        {
            var data = db.User.Where(x => x.Email == Email && x.password == password & x.isactive == true & x.isadmin == false).ToList();
            if (data.Count == 1)
            {
                //giris tamam
                Session["LoginUserID"] = data.FirstOrDefault().ID;
                Session["LoginUser"] = data.FirstOrDefault();
                return Redirect("/");
            }
            else
            {
                //girmeyib
                return View();
            }
        }
          public ActionResult CreateUser()
            {

                return View();
            }
        [HttpPost]
        public ActionResult CreateUser(User data)
        {
            try
            {
                data.isactive = true;
                data.isadmin = false;
                db.User.Add(data);
                db.SaveChanges();
                return Redirect("/");
            }
            catch (Exception xx)
            {

                return View();
            }
     
        }

        public ActionResult Logout()
        {
            Session.Abandon();

          return RedirectToAction("Index","Home");

        }

    }
    }
