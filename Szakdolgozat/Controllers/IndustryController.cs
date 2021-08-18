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
    public class IndustryController : Controller
    {
        private readonly DataContext _context;

        public IndustryController(DataContext context)
        {
            _context = context;
        }

        // GET: Industry
        public async Task<IActionResult> Index()
        {
            return View(await _context.Industry.ToListAsync());
        }

        // GET: Industry/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var industryModel = await _context.Industry
                .FirstOrDefaultAsync(m => m.Date == id);
            if (industryModel == null)
            {
                return NotFound();
            }

            return View(industryModel);
        }

        // GET: Industry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Industry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,SalesAmount")] IndustryModel industryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(industryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(industryModel);
        }

        // GET: Industry/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var industryModel = await _context.Industry.FindAsync(id);
            if (industryModel == null)
            {
                return NotFound();
            }
            return View(industryModel);
        }

        // POST: Industry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,SalesAmount")] IndustryModel industryModel)
        {
            if (id != industryModel.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(industryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndustryModelExists(industryModel.Date))
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
            return View(industryModel);
        }

        // GET: Industry/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var industryModel = await _context.Industry
                .FirstOrDefaultAsync(m => m.Date == id);
            if (industryModel == null)
            {
                return NotFound();
            }

            return View(industryModel);
        }

        // POST: Industry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var industryModel = await _context.Industry.FindAsync(id);
            _context.Industry.Remove(industryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndustryModelExists(DateTime id)
        {
            return _context.Industry.Any(e => e.Date == id);
        }
    }
}
