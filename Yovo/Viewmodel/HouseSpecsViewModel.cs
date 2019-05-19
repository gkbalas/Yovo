using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yovo.Models;

namespace Yovo.Viewmodel
{
    public class HouseSpecsViewModel
    {
        public Address Address { get; set; }
        public House House { get; set; }
        public List<Feature> Features { get; set; }
        public HouseImg HouseImg { get; set; }
        //public Feature Feature { get; set; }
    }
}