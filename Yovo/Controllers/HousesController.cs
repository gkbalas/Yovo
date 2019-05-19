using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Yovo.Models;
using Yovo.Viewmodel;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace Yovo.Controllers
{
    public class HousesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Houses
        public ActionResult Index()
        {
            ViewBag.Owners = db.Users.Where(u => u.Roles.ToString() == "Owner");
            var x = db.Houses.Include(h => h.Owner).ToList();
            return View(db.Houses.Include(h => h.Owner));
        }



        [HttpGet]
        public ActionResult SearchResults(string region, string city, decimal? price, decimal? SquareMeters)
        {
            SearchVM src = new SearchVM();
            var userid = User.Identity.GetUserId();
            var query = db.Addresses.Include(adr => adr.House).Include(adr => adr.House.Owner).Where(adr => adr.House.Owner.Id != userid);
            var x = query.ToList();
            if (region != "0"){query = query.Where(adr => adr.Region == region);}

            if (city != "0"){query = query.Where(adr => adr.Country == city);}

            if (price != null){query = query.Where(adr => adr.House.Price == price);}

            if (SquareMeters != null){query = query.Where(adr => adr.House.SquareMeters == SquareMeters);}
                       
            src.Address = query.ToList();
            src.HouseImg = db.HouseImgs.ToList();
            return View(src);
        }


        // GET: Houses
        public ActionResult MyHouses()
        {
            var CurrentUser = User.Identity.GetUserId();
            var houses = new SearchVM();
            houses.House = db.Houses.Include(h => h.Features).Where(h => h.Owner.Id == CurrentUser).ToList();
            houses.HouseImg = db.HouseImgs.ToList();
            //var CurrentUserId = ac.UserManager.FindById(User.Identity.GetUserId());
            return View(houses);
        }

        // GET: Houses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Because it is an array of bits we need to instantiate it before to save it.
            HouseSpecsViewModel house = new HouseSpecsViewModel();
            house.House = db.Houses.Include(h => h.Owner).Include(h => h.Features).SingleOrDefault(h => h.Id == id);
            house.Address = db.Addresses.SingleOrDefault(a => a.HouseId == id);
            house.HouseImg = db.HouseImgs.SingleOrDefault(i => i.HouseId == id);

            ViewBag.UserId = User.Identity.GetUserId();
            if (house == null)
            {
                return HttpNotFound();
            }
            var x = house;
            return View(house);
        }

        // GET: Houses/Create
        public ActionResult Create()
        {
            HouseSpecsViewModel h = new HouseSpecsViewModel();
            h.Features = db.Features.ToList();
            h.HouseImg = new HouseImg();
            return View(h);
        }

        // POST: Houses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HouseSpecsViewModel model, HttpPostedFileBase image1)
        {
            var x = image1;
            if (ModelState.IsValid)
            {
                var y = Request.Files[0];
                House house = model.House;
                Address address = model.Address;
                foreach (var item in model.Features)
                {
                    if (item.IsChecked)
                    {
                        house.Features.Add(db.Features.Find(item.Id));
                        item.IsChecked = false;
                    }
                }
                if (image1 != null)
                {
                    model.HouseImg = new HouseImg();
                    model.HouseImg.Image = new byte[image1.ContentLength];
                    image1.InputStream.Read(model.HouseImg.Image, 0, image1.ContentLength);
                    model.HouseImg.HouseId = model.House.Id;
                    db.HouseImgs.Add(model.HouseImg);
                }

                house.Owner = db.Users.Find(User.Identity.GetUserId());
                address.HouseId = house.Id;
                db.Houses.Add(house);
                db.Addresses.Add(address);
                db.SaveChanges();
              

                var ctrl = new AccountController();
                ctrl.ControllerContext = ControllerContext;
                var test = User.Identity.GetUserId();
                ctrl.UserManager.RemoveFromRole(User.Identity.GetUserId(), "Renter");
                ctrl.UserManager.AddToRole(User.Identity.GetUserId(), "Owner");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Details", new { id = model.House.Id});
        }

        // GET: Houses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        // POST: Houses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Rooms,Visitors,SquareMeters,Price,UserId")] House house)
        {
            if (ModelState.IsValid)
            {
                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(house);
        }

        // GET: Houses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        // POST: Houses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            House house = db.Houses.Find(id);
            db.Houses.Remove(house);
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
