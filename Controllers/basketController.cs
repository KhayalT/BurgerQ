using BurgerQ.Controllers.Base;
using BurgerQ.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BurgerQ.Controllers
{
    public class basketController : LoginControllerBase
    {
        // GET: basket
        burgerqEntities db = new burgerqEntities();
        [HttpPost]
        public JsonResult AddProduct(int productID, int quantity)
        {
            db.basket.Add(new BurgerQ.Models.Entity.basket
            {
                ProductId = productID,
                Quantity=quantity,
                UserId=LoginUserID

            }
                );
            var save=db.SaveChanges();
            return Json(save,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var bskt = db.basket.Include("product").Where(x => x.UserId == LoginUserID).ToList();
            var list = db.basket.Include("product").Where(x => x.UserId == LoginUserID).ToList();
             ViewBag.total=bskt.Sum(x => x.product.price);

            return View(list);
        }
        public ActionResult Delete(int id)
        {
            var it = db.basket.Find(id);
            db.basket.Remove(it);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}