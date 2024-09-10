using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSwimmingPool.Models;

namespace WebSwimmingPool.Controllers
{
    public class BookingsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.bookings.Include(b => b.Pools).Include(b => b.TimeClass);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.bookings.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.Pool_id = new SelectList(db.pools, "Pool_id", "Pool_Name");
            ViewBag.Time_id = new SelectList(db.timeClass, "Time_id", "Day_Time");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Booking_Id,Cust_Name,No_Visitors,Pool_id,Time_id,Cost")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                bookings.Cost = bookings.CalcTotalCost();
                db.bookings.Add(bookings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pool_id = new SelectList(db.pools, "Pool_id", "Pool_Name", bookings.Pool_id);
            ViewBag.Time_id = new SelectList(db.timeClass, "Time_id", "Day_Time", bookings.Time_id);
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.bookings.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pool_id = new SelectList(db.pools, "Pool_id", "Pool_Name", bookings.Pool_id);
            ViewBag.Time_id = new SelectList(db.timeClass, "Time_id", "Day_Time", bookings.Time_id);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Booking_Id,Cust_Name,No_Visitors,Pool_id,Time_id,Cost")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pool_id = new SelectList(db.pools, "Pool_id", "Pool_Name", bookings.Pool_id);
            ViewBag.Time_id = new SelectList(db.timeClass, "Time_id", "Day_Time", bookings.Time_id);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.bookings.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookings bookings = db.bookings.Find(id);
            db.bookings.Remove(bookings);
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
