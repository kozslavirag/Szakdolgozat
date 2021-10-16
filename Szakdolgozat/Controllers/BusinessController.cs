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
    public class BusinessController : Controller
    {
        List<int> business = new List<int>();
        List<BusinessModel> businessDataToChart = new List<BusinessModel>();

        private readonly DataContext _context;

        public BusinessController(DataContext context)
        {
            _context = context;
        }

        public void ListUploadOnline()
        {
            var businessModel = _context.Business.ToList();
            foreach (var item in businessModel)
            {
                business.Add(item.OnlineBusiness);
            }
        }

        public void ChartListUploadOnline()
        {
            int i = 1;
            var businessModel = _context.Business.ToList();
            foreach (var item in businessModel)
            {
                businessDataToChart.Add(new BusinessModel(i++, item.OnlineBusiness, item.Date.ToString("yyyy.MM.dd")));
            }
        }

        public double AvgOnline()
        {

            double avg = business.Average();
            return avg;
        }

        public int MedianOnline()
        {
            int salesCount = business.Count();
            int halfIndex = business.Count() / 2;
            var sortedNumbers = business.OrderBy(n => n);
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

        public int ModusOnline()
        {
            int mode = 0;

            var groupResult = business.GroupBy(n => n).ToList();

            if (groupResult.Count != business.Count)
            {
                mode = business.GroupBy(n => n).
                      OrderByDescending(g => g.Count()).
                      Select(g => g.Key).FirstOrDefault();
            }
            else
            {
            }
            return mode;
        }

        public int MaxOnline()
        {
            int max = business.Max();
            return max;
        }

        public int MinOnline()
        {
            int min = business.Min();
            return min;
        }

        public double DeviationOnline()
        {
            double avg = AvgOnline();
            List<double> differences = new List<double>();
            foreach (var item in business)
            {
                double difference = Math.Pow(item - avg, 2);
                differences.Add(difference);
            }
            double differenceSum = differences.Sum();
            double deviation = Math.Sqrt(differenceSum / business.Count());

            return deviation;
        }

        // GET: Business
        public async Task<IActionResult> Index()
        {
            return View(await _context.Business.ToListAsync());
        }

        // GET: Business/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessModel = await _context.Business
                .FirstOrDefaultAsync(m => m.Date == id);
            if (businessModel == null)
            {
                return NotFound();
            }

            return View(businessModel);
        }

        public ActionResult Statistics()
        {
            ChartListUploadOnline();
            ViewBag.CateringModels = JsonConvert.SerializeObject(businessDataToChart);
            ListUploadOnline();
            ViewBag.avg = AvgOnline();
            ViewBag.median = MedianOnline();
            ViewBag.modus = ModusOnline();
            ViewBag.max = MaxOnline();
            ViewBag.min = MinOnline();
            ViewBag.dev = DeviationOnline();
            return View();
        }

        // GET: Business/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Business/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,OnlineBusiness,Retail")] BusinessModel businessModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(businessModel);
        }

        // GET: Business/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessModel = await _context.Business.FindAsync(id);
            if (businessModel == null)
            {
                return NotFound();
            }
            return View(businessModel);
        }

        // POST: Business/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,OnlineBusiness,Retail")] BusinessModel businessModel)
        {
            if (id != businessModel.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessModelExists(businessModel.Date))
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
            return View(businessModel);
        }

        // GET: Business/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessModel = await _context.Business
                .FirstOrDefaultAsync(m => m.Date == id);
            if (businessModel == null)
            {
                return NotFound();
            }

            return View(businessModel);
        }

        // POST: Business/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var businessModel = await _context.Business.FindAsync(id);
            _context.Business.Remove(businessModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessModelExists(DateTime id)
        {
            return _context.Business.Any(e => e.Date == id);
        }
    }
}
