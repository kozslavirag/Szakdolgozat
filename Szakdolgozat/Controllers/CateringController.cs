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
    public class CateringController : Controller
    {
        private readonly DataContext _context;

        public CateringController(DataContext context)
        {
            _context = context;
        }

        // GET: Catering
        public async Task<IActionResult> Index()
        {
            return View(await _context.Catering.ToListAsync());
        }

        // GET: Catering/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateringModel = await _context.Catering
                .FirstOrDefaultAsync(m => m.Date == id);
            if (cateringModel == null)
            {
                return NotFound();
            }

            return View(cateringModel);
        }

        // GET: Catering/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catering/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,SalesVolume")] CateringModel cateringModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cateringModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cateringModel);
        }

        // GET: Catering/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateringModel = await _context.Catering.FindAsync(id);
            if (cateringModel == null)
            {
                return NotFound();
            }
            return View(cateringModel);
        }

        // POST: Catering/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,SalesVolume")] CateringModel cateringModel)
        {
            if (id != cateringModel.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cateringModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CateringModelExists(cateringModel.Date))
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
            return View(cateringModel);
        }

        // GET: Catering/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateringModel = await _context.Catering
                .FirstOrDefaultAsync(m => m.Date == id);
            if (cateringModel == null)
            {
                return NotFound();
            }

            return View(cateringModel);
        }

        // POST: Catering/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var cateringModel = await _context.Catering.FindAsync(id);
            _context.Catering.Remove(cateringModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CateringModelExists(DateTime id)
        {
            return _context.Catering.Any(e => e.Date == id);
        }
    }
}
