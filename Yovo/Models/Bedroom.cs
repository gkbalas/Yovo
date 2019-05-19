using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yovo.Models
{
    public class Bedroom
    {
        public int Id { get; set; }
        [Required]
        public int Room { get; set; }
        public int BedType { get; set; }

        public House House { get; set; }
        [ForeignKey("House")]
        public int? HouseId { get; set; }
    }
}