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
    public class BedroomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bedrooms
        public ActionResult Index()
        {
            var bedrooms = db.Bedrooms.Include(b => b.House);
            return View(bedrooms.ToList());
        }

        // GET: Bedrooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bedroom bedroom = db.Bedrooms.Find(id);
            if (bedroom == null)
            {
                return HttpNotFound();
            }
            return View(bedroom);
        }

        // GET: Bedrooms/Create
        public ActionResult Create()
        {
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title");
            return View();
        }

        // POST: Bedrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Room,BedType,HouseId")] Bedroom bedroom)
        {
            if (ModelState.IsValid)
            {
                db.Bedrooms.Add(bedroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", bedroom.HouseId);
            return View(bedroom);
        }

        // GET: Bedrooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bedroom bedroom = db.Bedrooms.Find(id);
            if (bedroom == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", bedroom.HouseId);
            return View(bedroom);
        }

        // POST: Bedrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Room,BedType,HouseId")] Bedroom bedroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bedroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", bedroom.HouseId);
            return View(bedroom);
        }

        // GET: Bedrooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bedroom bedroom = db.Bedrooms.Find(id);
            if (bedroom == null)
            {
                return HttpNotFound();
            }
            return View(bedroom);
        }

        // POST: Bedrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bedroom bedroom = db.Bedrooms.Find(id);
            db.Bedrooms.Remove(bedroom);
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
