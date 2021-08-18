using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Szakdolgozat.Context;
using Szakdolgozat.Models;

namespace Szakdolgozat.Controllers
{
    public class TourismController : Controller
    {
        private readonly DataContext _context;

        public TourismController(DataContext context)
        {
            _context = context;
        }

        // GET: Tourism
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tourism.ToListAsync());
        }

        // GET: Tourism/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourismModel = await _context.Tourism
                .FirstOrDefaultAsync(m => m.Date == id);
            if (tourismModel == null)
            {
                return NotFound();
            }

            return View(tourismModel);
        }

        // GET: Tourism/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tourism/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,TripForeignGuest,NightForeignGuest,SpendForeignGuest,TripHungarianGuest,NightHungarianGuest,SpendHungarianGuest")] TourismModel tourismModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourismModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourismModel);
        }

        // GET: Tourism/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourismModel = await _context.Tourism.FindAsync(id);
            if (tourismModel == null)
            {
                return NotFound();
            }
            return View(tourismModel);
        }

        // POST: Tourism/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,TripForeignGuest,NightForeignGuest,SpendForeignGuest,TripHungarianGuest,NightHungarianGuest,SpendHungarianGuest")] TourismModel tourismModel)
        {
            if (id != tourismModel.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourismModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourismModelExists(tourismModel.Date))
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
            return View(tourismModel);
        }

        // GET: Tourism/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourismModel = await _context.Tourism
                .FirstOrDefaultAsync(m => m.Date == id);
            if (tourismModel == null)
            {
                return NotFound();
            }

            return View(tourismModel);
        }

        // POST: Tourism/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var tourismModel = await _context.Tourism.FindAsync(id);
            _context.Tourism.Remove(tourismModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourismModelExists(DateTime id)
        {
            return _context.Tourism.Any(e => e.Date == id);
        }
    }
}
