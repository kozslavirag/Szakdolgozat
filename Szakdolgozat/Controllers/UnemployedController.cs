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
    public class UnemployedController : Controller
    {
        private readonly DataContext _context;

        public UnemployedController(DataContext context)
        {
            _context = context;
        }

        // GET: Unemployed
        public async Task<IActionResult> Index()
        {
            return View(await _context.Unemployed.ToListAsync());
        }

        // GET: Unemployed/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unemployedModel = await _context.Unemployed
                .FirstOrDefaultAsync(m => m.Date == id);
            if (unemployedModel == null)
            {
                return NotFound();
            }

            return View(unemployedModel);
        }

        // GET: Unemployed/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unemployed/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,NumberofUnemployed,MaleUnemployed,FemaleUnemployed")] UnemployedModel unemployedModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unemployedModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unemployedModel);
        }

        // GET: Unemployed/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unemployedModel = await _context.Unemployed.FindAsync(id);
            if (unemployedModel == null)
            {
                return NotFound();
            }
            return View(unemployedModel);
        }

        // POST: Unemployed/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,NumberofUnemployed,MaleUnemployed,FemaleUnemployed")] UnemployedModel unemployedModel)
        {
            if (id != unemployedModel.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unemployedModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnemployedModelExists(unemployedModel.Date))
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
            return View(unemployedModel);
        }

        // GET: Unemployed/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unemployedModel = await _context.Unemployed
                .FirstOrDefaultAsync(m => m.Date == id);
            if (unemployedModel == null)
            {
                return NotFound();
            }

            return View(unemployedModel);
        }

        // POST: Unemployed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var unemployedModel = await _context.Unemployed.FindAsync(id);
            _context.Unemployed.Remove(unemployedModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnemployedModelExists(DateTime id)
        {
            return _context.Unemployed.Any(e => e.Date == id);
        }
    }
}
