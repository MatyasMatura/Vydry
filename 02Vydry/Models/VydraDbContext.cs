using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace _02Vydry.Models
{
    public class VydraDbContext : IdentityDbContext
    {
        public VydraDbContext(DbContextOptions<VydraDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Vydra> Vydras { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>().HasData(
                new Location { LocationID = 111, Name = "NP Šumava", Area = 33233 },
                new Location { LocationID = 128, Name = "CHKO Jizerské hory", Area = 13165 },
                new Location { LocationID = 666, Name = "CHKO Čeký Les", Area = 15432 }
            );

            modelBuilder.Entity<Place>().HasKey(p => new { p.Name, p.LocationId });

            modelBuilder.Entity<Place>().HasData(
                    new Place { Name = "U Studánky", LocationId = 111 },
                    new Place { Name = "U Buku", LocationId = 111 },
                    new Place { Name = "Černé Jezero", LocationId = 128 },
                    new Place { Name = "U Studánky", LocationId = 128 },
                    new Place { Name = "Na Čihadlech", LocationId = 128 },
                    new Place { Name = "U Studánky", LocationId = 666 },
                    new Place { Name = "Český Pařez", LocationId = 666 }
                );

            modelBuilder.Entity<Vydra>(
                o =>
                {
                    o.HasOne(ot => ot.Mother)
                    .WithMany(m => m.Children)
                    .HasForeignKey(dt => dt.MotherId)
                    .OnDelete(DeleteBehavior.NoAction);

                    o.HasOne(ot => ot.Place)
                    .WithMany(pl => pl.Vydry)
                    .HasForeignKey(ot => new { ot.PlaceName, ot.LocationId })
                    .OnDelete(DeleteBehavior.Restrict);
                }
            );

            modelBuilder.Entity<Vydra>().HasData(
                new Vydra { Name = "Velká Máti", TattooID = 1, Color = "hnědá jako hodně", PlaceName = "U Studánky", LocationId = 111 },
                new Vydra { Name = "První Dcera", TattooID = 2, Color = "Hnědá taky", MotherId = 1, PlaceName = "U Studánky", LocationId = 111 },
                new Vydra { Name = "ZBloudilka", TattooID = 3, Color = "Hnědá trochu", MotherId = 1, PlaceName = "Černé Jezero", LocationId = 128 }
            );
        }
    }
}
