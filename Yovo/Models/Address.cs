using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yovo.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Street { get; set; }

        
        public string Latitude { get; set; }  // Ο χρηστης δεν πρεπει ποτε να δει/επεξεργαστει αυτο το στοιχειο. Ισως γινει private.

        
        public string Longitude { get; set; } // Ο χρηστης δεν πρεπει ποτε να δει/επεξεργαστει αυτο το στοιχειο. Ισως γινει private.

        public House House { get; set; }
        public int HouseId { get; set; }
    }
}