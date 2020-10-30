using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _02Vydry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace _02Vydry.Pages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly _02Vydry.Models.VydraDbContext _context;

        public DeleteModel(_02Vydry.Models.VydraDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
                .Include(v => v.founder).FirstOrDefaultAsync(m => m.TattooID == id);

            if (Vydra == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vydra = await _context.Vydras.FindAsync(id);

            if (Vydra != null)
            {
                _context.Vydras.Remove(Vydra);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
