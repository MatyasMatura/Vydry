using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace _02Vydry.Models
{
    public class Vydra
    {
        public string Name { get; set; }

        //public Gender Gender { get; set; }

        public string Color { get; set; }
        [Key]
        public int? TattooID { get; set; }
        public Vydra Mother { get; set; }

        [ForeignKey("Mother")]
        public int? MotherId { get; set; }

        [Required]
        public Place Place { get; set; }

        public string PlaceName { get; set; }
        public Location Location { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public ICollection<Vydra> Children { get; set; }
        public IdentityUser founder { get; set; }
        [ForeignKey("founder")]
        public string founderID { get; set; }
    }
}
