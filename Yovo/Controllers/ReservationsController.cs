using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yovo.Models;
using Yovo.Viewmodel;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Yovo.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.House);
            return View(reservations.ToList());
        }

        // GET: Reservations
        public ActionResult MyReservations()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            var userId = User.Identity.GetUserId();
            var reservations = db.Reservations.Include(r => r.Renter).Include(r => r.House).Include(r => r.House.Owner).Where(r => r.UserId == userId).ToList();
            return View(reservations);
        }

        // GET: Reservations
        public ActionResult HouseReservations(int? id)
        {
            ViewBag.House = db.Houses.FirstOrDefault(h => h.Id == id);
            var reservations = db.Reservations.Include(r => r.House).Include(r => r.Renter).Where(r => r.HouseId == id).ToList();
            return View(reservations);
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Availability
        public ActionResult availability(int houseId)
        {
            ViewBag.House = db.Houses.Find(houseId);
            return View();
        }

        public JsonResult HidJson(int houseId)
        {
            ReservationCheckVM oldReservation = new ReservationCheckVM();
            oldReservation.OldReservations = db.Reservations.Where(r => r.HouseId == houseId).ToList();
            var x = Json(oldReservation.OldReservations, JsonRequestBehavior.AllowGet);
            return Json(oldReservation.OldReservations,JsonRequestBehavior.AllowGet);
        }

        // POST: Reservations/Availability
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Availability(ReservationCheckVM res)
        {
            ViewBag.House = TempData["House"];
            var x = (ModelState.IsValid);
            if (ModelState.IsValid)
            {
                res.NewReservation.BookingDate = DateTime.Now;
                res.NewReservation.HouseId = ViewBag.House.Id;
                res.NewReservation.UserId = User.Identity.GetUserId();
                db.Reservations.Add(res.NewReservation);
                db.SaveChanges();
                return RedirectToAction("MyHouses", "Houses");
            }
            int houseid = ViewBag.House.Id;
            res.OldReservations = db.Reservations.Where(r => r.HouseId == houseid).ToList();
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", ViewBag.House.Id);
            return View(res);
        }

        // GET: Reservations/Book
        [Authorize]
        public ActionResult Book(int houseId)
        {
            ViewBag.House = db.Houses.Find(houseId);
            return View();
        }

        // POST: Reservations/Book
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(ReservationCheckVM res)
        {
            ViewBag.House = TempData["House"];
            var x = (ModelState.IsValid);
                if (ModelState.IsValid)
                {
                    res.NewReservation.BookingDate = DateTime.Now;
                    res.NewReservation.Occupancy = db.Houses.Find(ViewBag.House.Id).Visitors;
                    res.NewReservation.Price = db.Houses.Find(ViewBag.House.Id).Price;
                    res.NewReservation.HouseId = ViewBag.House.Id;
                    res.NewReservation.UserId = User.Identity.GetUserId();
                    res.NewReservation.Renter = db.Users.Find(User.Identity.GetUserId());
                    db.Reservations.Add(res.NewReservation);
                    db.SaveChanges();
                    return RedirectToAction("ThankYou");
                }
                
            int houseid = ViewBag.House.Id;
            res.OldReservations = db.Reservations.Where(r => r.HouseId == houseid).ToList();
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", ViewBag.House.Id);
            return View(res);
        }
        public ActionResult ThankYou()
        {
            return View();
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", reservation.HouseId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromDate,ToDate,UserId,HouseId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", reservation.HouseId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
