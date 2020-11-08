using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _02Vydry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace _02Vydry.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly _02Vydry.Models.VydraDbContext _context;

        public EditModel(_02Vydry.Models.VydraDbContext context)
        {
            _context = context;
        }
        public string GetUserId()
        {
            return HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? default;
        }


        [BindProperty]
        public Vydra Vydra { get; set; }
        [BindProperty]
        public Place Place { get; set; }


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
           LocationId = new SelectList(_context.Locations, "LocationID", "LocationID");
           MotherId = new SelectList(_context.Vydras, "TattooID", "Name");
           PlaceName = new SelectList(_context.Places, "Name", "Name");
           founderID = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            return Page();
        }

        public SelectList LocationId { get; set; }
        public SelectList MotherId { get; set; }
        public SelectList PlaceName { get; set; }
        public SelectList founderID { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            Vydra.founderID = GetUserId();
            Place = _context.Places.Include(p => p.Location).Include(p => p.Vydry).AsNoTracking().FirstOrDefault(p => p.Name == Vydra.PlaceName);
            Place.Name = Vydra.PlaceName;
            Vydra.LocationId = Place.LocationId;
            _context.Attach(Vydra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VydraExists(Vydra.TattooID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VydraExists(int? id)
        {
            return _context.Vydras.Any(e => e.TattooID == id);
        }
    }
}
