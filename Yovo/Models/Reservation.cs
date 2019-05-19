using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yovo.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Display(Name = "Reservation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? BookingDate { get; set; }
        public int? Occupancy { get; set; }
        public decimal? Price { get; set; }

        public ApplicationUser Renter { get; set; }
        public string UserId { get; set; }

        public House House { get; set; }
        [ForeignKey("House")]
        public int HouseId { get; set; }
    }
}