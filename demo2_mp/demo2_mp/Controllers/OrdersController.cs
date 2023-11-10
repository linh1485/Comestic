using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demo2_mp.Models;

namespace demo2_mp.Controllers
{
    public class OrdersController : Controller
    {
        private dbEntities2 db = new dbEntities2();

        // GET: OrderProes
        public ActionResult Index()
        {
            var orderProes = db.OrderProes.Include(o => o.User);
            return View(orderProes.ToList());
        }


        // GET: OrderProes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderProes.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            return View(orderPro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderPro orderPro = db.OrderProes.Find(id);
            db.OrderProes.Remove(orderPro);
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