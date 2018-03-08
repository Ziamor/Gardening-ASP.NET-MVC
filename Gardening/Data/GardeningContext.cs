using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gardening.Models;

namespace Gardening.Models
{
    public class GardeningContext : DbContext
    {
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<HardinessZone> HardinessZone { get; set; }
        public DbSet<PlantType> PlantType { get; set; }

        public GardeningContext(DbContextOptions<GardeningContext> options)
            : base(options)
        {
        }

        public DbSet<Gardening.Models.PlantPlantType> PlantPlantType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HardinessZone>()
                .HasMany(p => p.Plans)
                .WithOne(p => p.HardinessZone)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
