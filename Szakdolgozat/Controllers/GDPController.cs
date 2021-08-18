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
    public class GDPController : Controller
    {
        private readonly DataContext _context;

        public GDPController(DataContext context)
        {
            _context = context;
        }

        // GET: GDP
        public async Task<IActionResult> Index()
        {
            return View(await _context.GDP.ToListAsync());
        }

        // GET: GDP/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gDPModel = await _context.GDP
                .FirstOrDefaultAsync(m => m.Date == id);
            if (gDPModel == null)
            {
                return NotFound();
            }

            return View(gDPModel);
        }

        // GET: GDP/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GDP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,GDP")] GDPModel gDPModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gDPModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gDPModel);
        }

        // GET: GDP/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gDPModel = await _context.GDP.FindAsync(id);
            if (gDPModel == null)
            {
                return NotFound();
            }
            return View(gDPModel);
        }

        // POST: GDP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,GDP")] GDPModel gDPModel)
        {
            if (id != gDPModel.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gDPModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GDPModelExists(gDPModel.Date))
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
            return View(gDPModel);
        }

        // GET: GDP/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gDPModel = await _context.GDP
                .FirstOrDefaultAsync(m => m.Date == id);
            if (gDPModel == null)
            {
                return NotFound();
            }

            return View(gDPModel);
        }

        // POST: GDP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var gDPModel = await _context.GDP.FindAsync(id);
            _context.GDP.Remove(gDPModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GDPModelExists(DateTime id)
        {
            return _context.GDP.Any(e => e.Date == id);
        }
    }
}
