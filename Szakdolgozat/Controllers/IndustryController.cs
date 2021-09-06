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
    public class IndustryController : Controller
    {
        List<int> industry = new List<int>();
        List<IndustryModel> industryDataToChart = new List<IndustryModel>();
        private readonly DataContext _context;

        public IndustryController(DataContext context)
        {
            _context = context;
        }

        public void ListUpload()
        {
            var industryModel = _context.Industry.ToList();
            foreach (var item in industryModel)
            {
                industry.Add(item.SalesAmount);
            }
        }

        public void ChartListUpload()
        {
            int i = 1;
            var industryModel = _context.Industry.ToList();
            foreach (var item in industryModel)
            {
                industryDataToChart.Add(new IndustryModel(i++, item.SalesAmount, item.Date.ToString("yyyy.MM.dd")));
            }
        }

        public double Avg()
        {

            double avg = industry.Average();
            return avg;
        }

        public int Median()
        {
            int salesCount = industry.Count();
            int halfIndex = industry.Count() / 2;
            var sortedNumbers = industry.OrderBy(n => n);
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

            var groupResult = industry.GroupBy(n => n).ToList();

            if (groupResult.Count != industry.Count)
            {
                mode = industry.GroupBy(n => n).
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
            int max = industry.Max();
            return max;
        }

        public int Min()
        {
            int min = industry.Min();
            return min;
        }

        public double Deviation()
        {
            double avg = Avg();
            List<double> differences = new List<double>();
            foreach (var item in industry)
            {
                double difference = Math.Pow(item - avg, 2);
                differences.Add(difference);
            }
            double differenceSum = differences.Sum();
            double deviation = Math.Sqrt(differenceSum / industry.Count());

            return deviation;
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

        public ActionResult Statistics()
        {
            ChartListUpload();
            ViewBag.IndustryModels = JsonConvert.SerializeObject(industryDataToChart);
            ListUpload();
            ViewBag.avg = Avg();
            ViewBag.median = Median();
            ViewBag.modus = Modus();
            ViewBag.max = Max();
            ViewBag.min = Min();
            ViewBag.dev = Deviation();
            return View();
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
