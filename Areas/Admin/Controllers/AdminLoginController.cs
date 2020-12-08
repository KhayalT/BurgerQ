using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BurgerQ.Models.Entity;
namespace BurgerQ.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: Admin/AdminLogin
        burgerqEntities db = new burgerqEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string name,string Email, string password,int telephone)
        {
            var data = db.User.Where(x => x.Name == name && x.Email == Email && x.password == password && telephone == telephone & x.isactive == true & x.isadmin == true).ToList();
            if (data.Count == 1)
            {
                FormsAuthentication.SetAuthCookie(Email,false);
                Session["AdminLoginUser"] = data.FirstOrDefault();
                return RedirectToAction("Index","orders");
            }
            else 
            { 
         
            return View();
        }
    }

        public ActionResult AdminLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Users");


        }

    }
    
}