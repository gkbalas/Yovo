using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yovo.Models
{
    public class HouseImg
    {
        public int Id { get; set; }
        [Required]
        //public string Img { get; set; }
        public byte[] Image { get; set; }
        public House House { get; set; }
        [ForeignKey("House")]
        public int HouseId { get; set; }
    }
}