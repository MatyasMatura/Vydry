using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _02Vydry.Models;
using Microsoft.AspNetCore.Identity;


namespace _02Vydry.Pages
{
    public class IndexModel : PageModel
    {
        private readonly _02Vydry.Models.VydraDbContext _context;

        public IndexModel(_02Vydry.Models.VydraDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Vydra> Vydra { get;set; }

        public void OnGet()
        {
            Vydra = _context.Vydras
                .Include(v => v.Location)
                .Include(v => v.Mother)
                .Include(v => v.Place)
                .Include(v => v.founder).AsNoTracking().AsEnumerable();
        }
    }
}
