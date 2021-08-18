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
    public class PriceController : Controller
    {
        private readonly DataContext _context;

        public PriceController(DataContext context)
        {
            _context = context;
        }

        // GET: Price
        public async Task<IActionResult> Index()
        {
            return View(await _context.Price.ToListAsync());
        }

        // GET: Price/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceModel = await _context.Price
                .FirstOrDefaultAsync(m => m.Date == id);
            if (priceModel == null)
            {
                return NotFound();
            }

            return View(priceModel);
        }

        // GET: Price/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Price/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,StapleFoodPrice,PetrolPrice,GasOilPrice")] PriceModel priceModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priceModel);
        }

        // GET: Price/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceModel = await _context.Price.FindAsync(id);
            if (priceModel == null)
            {
                return NotFound();
            }
            return View(priceModel);
        }

        // POST: Price/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,StapleFoodPrice,PetrolPrice,GasOilPrice")] PriceModel priceModel)
        {
            if (id != priceModel.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceModelExists(priceModel.Date))
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
            return View(priceModel);
        }

        // GET: Price/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceModel = await _context.Price
                .FirstOrDefaultAsync(m => m.Date == id);
            if (priceModel == null)
            {
                return NotFound();
            }

            return View(priceModel);
        }

        // POST: Price/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var priceModel = await _context.Price.FindAsync(id);
            _context.Price.Remove(priceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceModelExists(DateTime id)
        {
            return _context.Price.Any(e => e.Date == id);
        }
    }
}
