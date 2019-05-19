using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yovo.Models;

namespace Yovo.Viewmodel
{
    public class SearchVM
    {
        public IList<Address> Address { get; set; }
        public IList<House> House { get; set; }
        public IList<Feature> Features { get; set; }
        public IList<HouseImg> HouseImg { get; set; }
        public IList<Reservation> Reservation{ get; set; }

        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal Maxsqr { get; set; }
        public decimal Minsqr { get; set; }

    }
}