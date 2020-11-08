using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using _02Vydry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace _02Vydry.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly _02Vydry.Models.VydraDbContext _context;
        public CreateModel(_02Vydry.Models.VydraDbContext context)
        {
            _context = context;
        }

        public string GetUserId()
        {
            return HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? default;
        }

        public IActionResult OnGet()
        {
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


        [BindProperty]
        public Vydra Vydra { get; set; }
        [BindProperty]
        public Place Place { get; set; }


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
            _context.Vydras.Add(Vydra);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
