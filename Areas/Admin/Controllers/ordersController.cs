using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BurgerQ.Controllers.Base;
using BurgerQ.Models.Entity;

namespace BurgerQ.Areas.Admin.Controllers
{
    public class ordersController : LoginControllerBase
    {
        private burgerqEntities db = new burgerqEntities();

        // GET: Admin/orders
        [Authorize]
        public ActionResult Index()
        {
            var orders = db.orders.Include(o => o.User).Include(o => o.useradress).Include(o => o.Status);
            return View(orders.ToList());
        }

        // GET: Admin/orders/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Admin/orders/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.User, "ID", "Name");
            ViewBag.UserAdressId = new SelectList(db.useradress, "ID", "Title");
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Name");
            return View();
        }

        // POST: Admin/orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,UserID,UserAdressId,TotalPrice,StatusID")] orders orders)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.User, "ID", "Name", orders.UserID);
            ViewBag.UserAdressId = new SelectList(db.useradress, "ID", "Title", orders.UserAdressId);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Name", orders.StatusID);
            return View(orders);
        }

        // GET: Admin/orders/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.User, "ID", "Name", orders.UserID);
            ViewBag.UserAdressId = new SelectList(db.useradress, "ID", "Title", orders.UserAdressId);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Name", orders.StatusID);
            return View(orders);
        }

        // POST: Admin/orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,UserID,UserAdressId,TotalPrice,StatusID")] orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.User, "ID", "Name", orders.UserID);
            ViewBag.UserAdressId = new SelectList(db.useradress, "ID", "Title", orders.UserAdressId);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Name", orders.StatusID);
            return View(orders);
        }

        // GET: Admin/orders/Delete/5
        [Authorize]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Admin/orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            orders orders = db.orders.Find(id);
            db.orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
