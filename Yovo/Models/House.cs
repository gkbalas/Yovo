using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yovo.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int Rooms { get; set; }
        public int Visitors { get; set; }
        public decimal SquareMeters { get; set; }

        public decimal Price { get; set; }
        public ApplicationUser Owner { get; set; }

        public House()
        {
            this.Features = new HashSet<Feature>();
        }
        public virtual ICollection<Feature> Features { get; set; } 
    }
}