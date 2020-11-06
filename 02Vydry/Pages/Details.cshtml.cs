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
    public class DetailsModel : PageModel
    {
        private readonly _02Vydry.Models.VydraDbContext _context;

        public DetailsModel(_02Vydry.Models.VydraDbContext context)
        {
            _context = context;
        }

        public Vydra Vydra { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vydra = await _context.Vydras
                .Include(v => v.Location)
                .Include(v => v.Mother)
                .Include(v => v.Place)
                .Include(v => v.founder).AsNoTracking().FirstOrDefaultAsync(m => m.TattooID == id);

            if (Vydra == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
