using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Szakdolgozat.Context;
using Szakdolgozat.Models;

namespace Szakdolgozat.Controllers
{
    public class GDPController : Controller
    {
        List<int> gdp = new List<int>();
        List<GDPModel> gdpDataToChart = new List<GDPModel>();
        private readonly DataContext _context;

        public GDPController(DataContext context)
        {
            _context = context;
        }

        public void ListUpload()
        {
            var gdpModel = _context.GDP.ToList();
            foreach (var item in gdpModel)
            {
                gdp.Add(item.GDP);
            }
        }

        public void ChartListUpload()
        {
            int i = 1;
            var gdpModel = _context.GDP.ToList();
            foreach (var item in gdpModel)
            {
                gdpDataToChart.Add(new GDPModel(i++, item.GDP, item.Date.ToString("yyyy.MM.dd")));
            }
        }

        public double Avg()
        {

            double avg = gdp.Average();
            return avg;
        }

        public int Median()
        {
            int salesCount = gdp.Count();
            int halfIndex = gdp.Count() / 2;
            var sortedNumbers = gdp.OrderBy(n => n);
            int median;
            if ((salesCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) +
                    sortedNumbers.ElementAt((halfIndex - 1))) / 2);

            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }

            return median;

        }

        public int Modus()
        {
            int mode = 0;

            var groupResult = gdp.GroupBy(n => n).ToList();

            if (groupResult.Count != gdp.Count)
            {
                mode = gdp.GroupBy(n => n).
                      OrderByDescending(g => g.Count()).
                      Select(g => g.Key).FirstOrDefault();
            }
            else
            {
            }
            return mode;
        }

        public int Max()
        {
            int max = gdp.Max();
            return max;
        }

        public int Min()
        {
            int min = gdp.Min();
            return min;
        }

        public double Deviation()
        {
            double avg = Avg();
            List<double> differences = new List<double>();
            foreach (var item in gdp)
            {
                double difference = Math.Pow(item - avg, 2);
                differences.Add(difference);
            }
            double differenceSum = differences.Sum();
            double deviation = Math.Sqrt(differenceSum / gdp.Count());

            return deviation;
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

        public ActionResult Statistics()
        {
            ChartListUpload();
            ViewBag.GDPModels = JsonConvert.SerializeObject(gdpDataToChart);
            ListUpload();
            ViewBag.avg = Avg();
            ViewBag.median = Median();
            ViewBag.modus = Modus();
            ViewBag.max = Max();
            ViewBag.min = Min();
            ViewBag.dev = Deviation();
            return View();
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
