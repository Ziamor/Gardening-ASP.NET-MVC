using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gardening.Models;
using Gardening.ViewModels;

namespace Gardening.Controllers
{
    public class PlantController : Controller
    {
        private readonly GardeningContext _context;

        public PlantController(GardeningContext context)
        {
            _context = context;
        }

        // GET: Plant
        public async Task<IActionResult> Index()
        {
            var gardeningContext = _context.Plant.Include(p => p.HardinessZone);
            return View(await gardeningContext.ToListAsync());
        }

        // GET: Plant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .Include(p => p.HardinessZone)
                .Include(p => p.PlantPlantType)
                    .ThenInclude(q => q.PlantType)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // GET: Plant/Create
        public IActionResult Create()
        {
            ViewData["HardinessZoneID"] = new SelectList(_context.HardinessZone, "ID", "Name");
            return View();
        }

        // POST: Plant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HardinessZoneID,Name")] Plant plant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HardinessZoneID"] = new SelectList(_context.HardinessZone, "ID", "Name", plant.HardinessZoneID);
            return View(plant);
        }

        // GET: Plant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .Include(p => p.PlantPlantType)
                    .ThenInclude(q => q.PlantType)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (plant == null)
            {
                return NotFound();
            }
            ViewData["HardinessZoneID"] = new SelectList(_context.HardinessZone, "ID", "Name", plant.HardinessZoneID);
            var viewmodel = new PlantEditViewModel();
            viewmodel.Plant = plant;
            //var t = _context.PlantPlantType.Where(p => p.PlantID == id).ToList();
            var selectedTags = _context.PlantPlantType.Where(p => p.PlantID == id).Select(q => q.PlantTypeID);
            var tags = _context.PlantType.Select(p => new TagViewModel() { PlantTypeID = p.ID, PlantType = p, IsSelected = selectedTags.Contains(p.ID) }).ToList();
            viewmodel.Tags = tags;
            return View(viewmodel);
        }

        // POST: Plant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HardinessZoneID,Name")] Plant plant, PlantEditViewModel plantVM)
        {
            if (id != plant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(plant.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HardinessZoneID"] = new SelectList(_context.HardinessZone, "ID", "Name", plant.HardinessZoneID);
            var viewmodel = new PlantEditViewModel();
            viewmodel.Plant = plant;
            return View(viewmodel);
        }

        // GET: Plant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .Include(p => p.HardinessZone)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plant = await _context.Plant.SingleOrDefaultAsync(m => m.ID == id);
            _context.Plant.Remove(plant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(int id)
        {
            return _context.Plant.Any(e => e.ID == id);
        }
    }
}
