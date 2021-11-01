using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAdventureAPI;
using SpaceVentureAPI;

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
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("datasource=SpaceVenture.db");
        }



    }
}
