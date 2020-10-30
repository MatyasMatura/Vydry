using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _02Vydry.Models
{
    public class Location
    {
        public int Area { get; set; }
        [Key]
        public int LocationID { get; set; }
        public string Name { get; set; }

        public ICollection<Place> Places { get; set; }
    }
}
