using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAdventureAPI;

namespace SpaceAdventureAPI
{
    public class SpaceVentureContext : DbContext
    {
        public DbSet<About> Abouts { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<ShuttleGalleryItem> ShuttleGalleryItems { get; set; }
        public DbSet<Safety> Safeties { get; set; }
        public DbSet<Shuttle> Shuttles { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("datasource=SpaceVenture.db");
        }

        public DbSet<SpaceAdventureAPI.Planet> Planet { get; set; }
    }
}
