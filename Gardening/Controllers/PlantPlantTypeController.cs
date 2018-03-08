using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gardening.Models;

namespace Gardening.Controllers
{
    public class PlantPlantTypeController : Controller
    {
        private readonly GardeningContext _context;

        public PlantPlantTypeController(GardeningContext context)
        {
            _context = context;
        }

        // GET: PlantPlantType
        public async Task<IActionResult> Index()
        {
            var gardeningContext = _context.PlantPlantType.Include(p => p.Plant).Include(p => p.PlantType);
            return View(await gardeningContext.ToListAsync());
        }

        // GET: PlantPlantType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantPlantType = await _context.PlantPlantType
                .Include(p => p.Plant)
                .Include(p => p.PlantType)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (plantPlantType == null)
            {
                return NotFound();
            }

            return View(plantPlantType);
        }

        // GET: PlantPlantType/Create
        public IActionResult Create()
        {
            ViewData["PlantID"] = new SelectList(_context.Plant, "ID", "Name");
            ViewData["PlantTypeID"] = new SelectList(_context.PlantType, "ID", "Name");
            return View();
        }

        // POST: PlantPlantType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PlantID,PlantTypeID")] PlantPlantType plantPlantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantPlantType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantID"] = new SelectList(_context.Plant, "ID", "Name", plantPlantType.PlantID);
            ViewData["PlantTypeID"] = new SelectList(_context.PlantType, "ID", "Name", plantPlantType.PlantTypeID);
            return View(plantPlantType);
        }

        // GET: PlantPlantType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantPlantType = await _context.PlantPlantType.SingleOrDefaultAsync(m => m.ID == id);
            if (plantPlantType == null)
            {
                return NotFound();
            }
            ViewData["PlantID"] = new SelectList(_context.Plant, "ID", "Name", plantPlantType.PlantID);
            ViewData["PlantTypeID"] = new SelectList(_context.PlantType, "ID", "Name", plantPlantType.PlantTypeID);
            return View(plantPlantType);
        }

        // POST: PlantPlantType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PlantID,PlantTypeID")] PlantPlantType plantPlantType)
        {
            if (id != plantPlantType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantPlantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantPlantTypeExists(plantPlantType.ID))
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
            ViewData["PlantID"] = new SelectList(_context.Plant, "ID", "Name", plantPlantType.PlantID);
            ViewData["PlantTypeID"] = new SelectList(_context.PlantType, "ID", "Name", plantPlantType.PlantTypeID);
            return View(plantPlantType);
        }

        // GET: PlantPlantType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantPlantType = await _context.PlantPlantType
                .Include(p => p.Plant)
                .Include(p => p.PlantType)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (plantPlantType == null)
            {
                return NotFound();
            }

            return View(plantPlantType);
        }

        // POST: PlantPlantType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantPlantType = await _context.PlantPlantType.SingleOrDefaultAsync(m => m.ID == id);
            _context.PlantPlantType.Remove(plantPlantType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantPlantTypeExists(int id)
        {
            return _context.PlantPlantType.Any(e => e.ID == id);
        }
    }
}
