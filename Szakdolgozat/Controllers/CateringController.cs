using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Szakdolgozat.Context;
using Szakdolgozat.Models;

namespace Szakdolgozat.Controllers
{
    public class CateringController : Controller
    {
        List<int> catering = new List<int>();
        List<CateringModel> cateringDataToChart = new List<CateringModel>();
        private readonly DataContext _context;

        public CateringController(DataContext context)
        {
            _context = context;
        }

        public void ListUpload()
        {
            var cateringModel = _context.Catering.ToList();
            foreach (var item in cateringModel)
            {
                catering.Add(item.SalesVolume);
            }
        }

        public void ChartListUpload()
        {
            var cateringModel = _context.Catering.ToList();
            foreach (var item in cateringModel)
            {
                cateringDataToChart.Add(new CateringModel(item.Date, item.SalesVolume));
            }
        }

        public double Avg()
        {

            double avg = catering.Average();
            return avg;
        }

        public int Median()
        {
            int salesCount = catering.Count();
            int halfIndex = catering.Count() / 2;
            var sortedNumbers = catering.OrderBy(n => n);
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

            var groupResult = catering.GroupBy(n => n).ToList();

            if (groupResult.Count != catering.Count)
            {
                mode = catering.GroupBy(n => n).
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
            int max = catering.Max();
            return max;
        }

        public int Min()
        {
            int min = catering.Min();
            return min;
        }

        public double Deviation()
        {
            double avg = Avg();
            List<double> differences = new List<double>();
            foreach (var item in catering)
            {
                double difference = Math.Pow(item - avg,2);
                differences.Add(difference);
            }
            double differenceSum = differences.Sum();
            double deviation = Math.Sqrt(differenceSum/catering.Count());

            return deviation;           
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

        public ActionResult Statistics()
        {
            ChartListUpload();
            ViewBag.CateringModels = JsonConvert.SerializeObject(cateringDataToChart);
            ListUpload();
            ViewBag.avg = Avg();
            ViewBag.median = Median();
            ViewBag.modus = Modus();
            ViewBag.max = Max();
            ViewBag.min = Min();
            ViewBag.dev = Deviation();
            return View();
        }
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
