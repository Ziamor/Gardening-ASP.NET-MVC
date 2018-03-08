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
    public class PlanController : Controller
    {
        private readonly GardeningContext _context;

        public PlanController(GardeningContext context)
        {
            _context = context;
        }

        // GET: Plan
        public async Task<IActionResult> Index()
        {
            var gardeningContext = _context.Plan.Include(p => p.HardinessZone).Include(p => p.Plant);
            return View(await gardeningContext.ToListAsync());
        }

        // GET: Plan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .Include(p => p.HardinessZone)
                .Include(p => p.Plant)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // GET: Plan/Create
        public IActionResult Create()
        {
            ViewData["HardinessZoneID"] = new SelectList(_context.HardinessZone, "ID", "Name");
            ViewData["PlantID"] = new SelectList(_context.Plant, "ID", "Name");
            return View();
        }

        // POST: Plan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,PlantID,HardinessZoneID")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HardinessZoneID"] = new SelectList(_context.HardinessZone, "ID", "Name", plan.HardinessZoneID);
            ViewData["PlantID"] = new SelectList(_context.Plant, "ID", "Name", plan.PlantID);
            return View(plan);
        }

        // GET: Plan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan.SingleOrDefaultAsync(m => m.ID == id);
            if (plan == null)
            {
                return NotFound();
            }
            ViewData["HardinessZoneID"] = new SelectList(_context.HardinessZone, "ID", "Name", plan.HardinessZoneID);
            ViewData["PlantID"] = new SelectList(_context.Plant, "ID", "Name", plan.PlantID);
            return View(plan);
        }

        // POST: Plan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,PlantID,HardinessZoneID")] Plan plan)
        {
            if (id != plan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.ID))
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
            ViewData["HardinessZoneID"] = new SelectList(_context.HardinessZone, "ID", "Name", plan.HardinessZoneID);
            ViewData["PlantID"] = new SelectList(_context.Plant, "ID", "Name", plan.PlantID);
            return View(plan);
        }

        // GET: Plan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .Include(p => p.HardinessZone)
                .Include(p => p.Plant)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Plan.SingleOrDefaultAsync(m => m.ID == id);
            _context.Plan.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanExists(int id)
        {
            return _context.Plan.Any(e => e.ID == id);
        }
    }
}
