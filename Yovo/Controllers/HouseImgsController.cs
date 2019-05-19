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
    public class HouseImgsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HouseImgs
        public ActionResult Index()
        {
            var houseImgs = db.HouseImgs.Include(h => h.House);
            return View(houseImgs.ToList());
        }

        // GET: HouseImgs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseImg houseImg = db.HouseImgs.Find(id);
            if (houseImg == null)
            {
                return HttpNotFound();
            }
            return View(houseImg);
        }

        // GET: HouseImgs/Create
        public ActionResult Create()
        {
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title");
            return View();
        }

        // POST: HouseImgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Img,HouseId")] HouseImg houseImg)
        {
            if (ModelState.IsValid)
            {
                db.HouseImgs.Add(houseImg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", houseImg.HouseId);
            return View(houseImg);
        }

        // GET: HouseImgs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseImg houseImg = db.HouseImgs.Find(id);
            if (houseImg == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", houseImg.HouseId);
            return View(houseImg);
        }

        // POST: HouseImgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Img,HouseId")] HouseImg houseImg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseImg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", houseImg.HouseId);
            return View(houseImg);
        }

        // GET: HouseImgs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseImg houseImg = db.HouseImgs.Find(id);
            if (houseImg == null)
            {
                return HttpNotFound();
            }
            return View(houseImg);
        }

        // POST: HouseImgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseImg houseImg = db.HouseImgs.Find(id);
            db.HouseImgs.Remove(houseImg);
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
