using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yovo.Models;

namespace Yovo.Controllers
{
    public class HouseReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HouseReviews
        public ActionResult Index()
        {
            var houseReviews = db.HouseReviews.Include(h => h.House);
            return View(houseReviews.ToList());
        }

        // GET: HouseReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseReview houseReview = db.HouseReviews.Find(id);
            if (houseReview == null)
            {
                return HttpNotFound();
            }
            return View(houseReview);
        }

        // GET: HouseReviews/Create
        public ActionResult Create()
        {
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title");
            return View();
        }

        // POST: HouseReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Grade,Comment,HouseId,UserId")] HouseReview houseReview)
        {
            if (ModelState.IsValid)
            {
                db.HouseReviews.Add(houseReview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", houseReview.HouseId);
            return View(houseReview);
        }

        // GET: HouseReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseReview houseReview = db.HouseReviews.Find(id);
            if (houseReview == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", houseReview.HouseId);
            return View(houseReview);
        }

        // POST: HouseReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Grade,Comment,HouseId,UserId")] HouseReview houseReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", houseReview.HouseId);
            return View(houseReview);
        }

        // GET: HouseReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseReview houseReview = db.HouseReviews.Find(id);
            if (houseReview == null)
            {
                return HttpNotFound();
            }
            return View(houseReview);
        }

        // POST: HouseReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseReview houseReview = db.HouseReviews.Find(id);
            db.HouseReviews.Remove(houseReview);
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
