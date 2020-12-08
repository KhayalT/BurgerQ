using BurgerQ.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BurgerQ.Models.Entity;
using System.Data.Entity;

namespace BurgerQ.Controllers
{
    public class OrderController : LoginControllerBase
    {
        // GET: Order
        burgerqEntities db = new burgerqEntities();
        public ActionResult AdressList()
        {
            var data = db.useradress.Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }
        public ActionResult CreateAdress()
        {


            return View();
        }
        [HttpPost]
        public ActionResult CreateAdress(useradress entity)
        {
            entity.UserID = LoginUserID;
            entity.isactive = true;
            db.useradress.Add(entity);
            db.SaveChanges();
            return RedirectToAction("AdressList");
        }
        public ActionResult DeleteAdress(int id)
        {
            var delete = db.useradress.Find(id);
            db.useradress.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("AdressList","Order");

        }

        public ActionResult CreateOrder(int id)
        {
            var bskt = db.basket.Include("product").Where(x => x.UserId == LoginUserID).ToList();
            orders order = new orders();
            order.UserID = LoginUserID;
            order.StatusID = 2;
            order.TotalPrice = bskt.Sum(x => x.product.price);
            order.UserAdressId = id;
            order.orderproduct = new List<orderproduct>();
            foreach (var item in bskt)
            {
                order.orderproduct.Add(new orderproduct
                {
                    ProductId=item.ProductId,
                    quantity=item.Quantity,
                    
                });
                db.basket.Remove(item);
            }
            db.orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Detail", new { id=order.ID });

        }

        public ActionResult Detail(int id)
        {

            var data = db.orders.Include("orderproduct").Include("orderproduct.product").Include("Status").Include("useradress").Where(x => x.ID == id).FirstOrDefault();


            return View(data);

        }
        public ActionResult Index()
        {
            var data = db.orders.Include("Status").Where(x => x.UserID == LoginUserID).ToList();
            return View(data);

            
        }
        }
}