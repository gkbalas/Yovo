using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yovo.Models
{
    public class HouseReview
    {
        public int Id { get; set; }
        [Required]
        public int Grade { get; set; }
        public int Comment { get; set; }

        public House House { get; set; }
        [ForeignKey("House")]
        public int? HouseId { get; set; }

        public int? UserId { get; set; }
    }
}