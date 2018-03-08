using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gardening.Data;
using Gardening.Models;

namespace Gardening.Controllers
{
    public class HardinessZonesController : Controller
    {
        private readonly GardeningContext _context;

        public HardinessZonesController(GardeningContext context)
        {
            _context = context;
        }

        // GET: HardinessZones
        public async Task<IActionResult> Index()
        {
            return View(await _context.HardinessZone.ToListAsync());
        }

        // GET: HardinessZones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hardinessZone = await _context.HardinessZone
                .SingleOrDefaultAsync(m => m.ID == id);
            if (hardinessZone == null)
            {
                return NotFound();
            }

            return View(hardinessZone);
        }

        // GET: HardinessZones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HardinessZones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] HardinessZone hardinessZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hardinessZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hardinessZone);
        }

        // GET: HardinessZones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hardinessZone = await _context.HardinessZone.SingleOrDefaultAsync(m => m.ID == id);
            if (hardinessZone == null)
            {
                return NotFound();
            }
            return View(hardinessZone);
        }

        // POST: HardinessZones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] HardinessZone hardinessZone)
        {
            if (id != hardinessZone.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hardinessZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HardinessZoneExists(hardinessZone.ID))
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
            return View(hardinessZone);
        }

        // GET: HardinessZones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hardinessZone = await _context.HardinessZone
                .SingleOrDefaultAsync(m => m.ID == id);
            if (hardinessZone == null)
            {
                return NotFound();
            }

            return View(hardinessZone);
        }

        // POST: HardinessZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hardinessZone = await _context.HardinessZone.SingleOrDefaultAsync(m => m.ID == id);
            _context.HardinessZone.Remove(hardinessZone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HardinessZoneExists(int id)
        {
            return _context.HardinessZone.Any(e => e.ID == id);
        }
    }
}
