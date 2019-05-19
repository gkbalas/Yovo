using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yovo.Models
{
    public class Feature
    {
        public Feature()
        {
            this.Houses = new HashSet<House>();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        
       
      
        //public bool TV { get; set; }
        //public bool Kitchen { get; set; }
        //public bool Iron { get; set; }
        //public bool Dryer { get; set; }
        //public bool Heating { get; set; }
        //public bool Washer { get; set; }
        //public bool CableTv { get; set; }
        //public bool PrivateBathroom { get; set; }
        //public bool PrivateLivingRoom { get; set; }
        //public bool HotWater { get; set; }
        //public bool Essentials { get; set; }
        //public bool AirConditioning { get; set; }
        //public bool HairDryer { get; set; }
        //public bool IndoorFireplace { get; set; }
        //public bool PrivateEntrance { get; set; }
        //public bool Hangers { get; set; }

        public virtual ICollection<House> Houses { get; set; }
    }
}