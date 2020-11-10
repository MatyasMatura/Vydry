using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _02Vydry.Models
{
    public enum Gender
    {
        [Display(Name="Muž")]
        Male = 1,
        [Display(Name = "Žena")]
        Female = 2,
        [Display(Name = "Jiné")]
        Other = 3
    }
}
