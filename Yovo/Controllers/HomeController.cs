using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yovo.Models;
using Yovo.Viewmodel;

namespace Yovo.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            SearchVM src = new SearchVM();
            src.Address = db.Addresses.Include(adrs=> adrs.House).ToList();
            src.House = db.Houses.Include(hss => hss.Owner).ToList();
            src.HouseImg = db.HouseImgs.ToList();


            //Loop for Price & SquareMeters Slides.
            decimal maxP = 0;
            decimal minP = 99999;
            decimal maxS = 0;
            decimal minS = 99999;
            foreach(var item in db.Houses)
            {
                if (item.Price > maxP)
                {
                    maxP = item.Price;
                    src.MaxPrice = item.Price;
                }
                if(item.Price < minP)
                {
                    minP = item.Price;
                    src.MinPrice = item.Price;
                }
                if (item.SquareMeters> maxS)
                {
                    maxS = item.SquareMeters;
                    src.Maxsqr= item.SquareMeters;
                }
                if (item.SquareMeters < minS)
                {
                    minS = item.SquareMeters;
                    src.Minsqr= item.SquareMeters;
                }

            }



            return View(src);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}