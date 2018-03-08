using Gardening.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardening.Data
{
    public static class GardenInitilizer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<GardeningContext>();
            context.Database.EnsureCreated();
            if (!context.HardinessZone.Any())
            {
                for (int i = 0; i <= 12; i++)
                {
                    context.HardinessZone.Add(new HardinessZone() { Name = (i + "a") });
                    context.HardinessZone.Add(new HardinessZone() { Name = (i + "b") });
                    context.SaveChanges();
                }
            }

            if (!context.PlantType.Any())
            {
                context.PlantType.Add(new PlantType() { Name = "Annual" });
                context.PlantType.Add(new PlantType() { Name = "Perennial" });
                context.PlantType.Add(new PlantType() { Name = "Fruit" });
                context.PlantType.Add(new PlantType() { Name = "Vegetable" });
                context.PlantType.Add(new PlantType() { Name = "Root Vegetable" });
                context.PlantType.Add(new PlantType() { Name = "Herb" });
                context.PlantType.Add(new PlantType() { Name = "Vine" });
                context.PlantType.Add(new PlantType() { Name = "Tree" });
                context.PlantType.Add(new PlantType() { Name = "Shrub" });
                context.PlantType.Add(new PlantType() { Name = "Bulb" });
                context.SaveChanges();
            }

            if (!context.Plant.Any())
            {
                AddPlant(context, "Tulip", "Bulb");
                AddPlant(context, "Carrot", "Vegetable", "Root Vegetable");
                context.SaveChanges();
            }
            if (!context.Plan.Any())
            {
                context.Plan.Add(new Plan()
                {
                    Name = "Super Carrots",
                    Description = "Grow the best carrots ever.",
                    HardinessZone = context.HardinessZone.Where(m => m.Name == "0a").Single(),
                    Plant = context.Plant.Where(m => m.Name == "Carrot").Single()
                });
                context.SaveChanges();
            }
        }

        private static void AddPlant(GardeningContext context, string name, params string[] types)
        {
            Plant p = new Plant() { Name = name, HardinessZone = context.HardinessZone.Where(m => m.Name == "0a").Single() };
            context.Plant.Add(p);
            foreach (string s in types)
                context.PlantPlantType.Add(new PlantPlantType() { Plant = p, PlantType = context.PlantType.Where(m => m.Name.Equals(s)).Single() });
        }
    }
}
