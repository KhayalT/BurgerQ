using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BurgerQ.Controllers.Base;
using BurgerQ.Models.Entity;
namespace BurgerQ.Controllers
{
    public class MenuController : LoginControllerBase
    {
        // GET: Menu
        burgerqEntities db = new burgerqEntities();
        public ActionResult Index()
        {
            ViewBag.IsLogin = this.IsLogin;
            var list = db.product.Where(x => x.category == 1 & x.isactive == true).ToList();
            return View(list);
        }

       
        public ActionResult Apper()
        {
            ViewBag.IsLogin = this.IsLogin;
            var list = db.product.Where(x => x.category == 2 & x.isactive == true).ToList();
            return View(list);
        }
        public ActionResult Drinks()
        {
            ViewBag.IsLogin = this.IsLogin;
            var list = db.product.Where(x => x.category == 3 & x.isactive == true).ToList();
            return View(list);
        }
    }

}