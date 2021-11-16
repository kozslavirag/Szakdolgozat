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
    public class TourismController : Controller
    {
        List<int> tourism = new List<int>();
        List<int> tourismHu = new List<int>();
        List<TourismModel> tourismForeignDataToChart = new List<TourismModel>();
        List<TourismModel> tourismHungarianDataToChart = new List<TourismModel>();
        List<TourismModel> tourismDataToChart = new List<TourismModel>();
        List<TourismModel> tourismHuDataToChart = new List<TourismModel>();
        List<EmployeeModel> tourismSpendDataToChart = new List<EmployeeModel>();
        List<int> tourismForeign = new List<int>();
        List<int> tourismHungarian = new List<int>();
        private readonly DataContext _context;

        public TourismController(DataContext context)
        {
            _context = context;
        }

        public void ListUpload()
        {
            var tourismModel = _context.Tourism.ToList();
            foreach (var item in tourismModel)
            {
                tourism.Add(item.SpendForeignGuest);
                tourismHu.Add(item.SpendHungarianGuest);
            }
        }

        public void ChartListUpload()
        {
            int i = 1;
            var tourismModel = _context.Tourism.ToList();
            foreach (var item in tourismModel)
            {
                tourismForeignDataToChart.Add(new TourismModel(i++, item.SpendForeignGuest, item.Date.ToString("yyyy.MM.dd")));
            }
            int j = 1;
            foreach (var item in tourismModel)
            {
                tourismHungarianDataToChart.Add(new TourismModel(j++, item.SpendHungarianGuest, item.Date.ToString("yyyy.MM.dd")));
            }
        }

        public void PieChartListUpload()
        {
            int i = 1;
            int j = 1;
            var tourismModel = _context.Tourism.ToList();
            foreach (var item in tourismModel)
            {
                tourismDataToChart.Add(new TourismModel (i++, item.NightForeignGuest, item.Date.ToString("yyyy.MM.dd")));
                tourismHuDataToChart.Add(new TourismModel(j++, item.NightHungarianGuest, item.Date.ToString("yyyy.MM.dd")));
            }
            
            //foreach (var item in tourismModel)
            //{
            //    tourismHungarianDataToChart.Add(new TourismModel(j++, item.NightHungarianGuest, item.Date.ToString("yyyy.MM.dd")));
            //}
            //tourismSpendDataToChart.Add(new EmployeeModel("Külföldi vendégéjszakák száma", tourismForeign.Sum()));
            //tourismSpendDataToChart.Add(new EmployeeModel("Belföldi vendégéjszakák száma", tourismHu.Sum()));
        }

        public double Avg()
        {

            double avg = tourism.Average();
            return avg;
        }

        public double AvgHu()
        {

            double avg = tourismHu.Average();
            return avg;
        }

        public int Median()
        {
            int salesCount = tourism.Count();
            int halfIndex = tourism.Count() / 2;
            var sortedNumbers = tourism.OrderBy(n => n);
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

        public int MedianHu()
        {
            int salesCount = tourismHu.Count();
            int halfIndex = tourismHu.Count() / 2;
            var sortedNumbers = tourismHu.OrderBy(n => n);
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

            var groupResult = tourism.GroupBy(n => n).ToList();

            if (groupResult.Count != tourism.Count)
            {
                mode = tourism.GroupBy(n => n).
                      OrderByDescending(g => g.Count()).
                      Select(g => g.Key).FirstOrDefault();
            }
            else
            {
            }
            return mode;
        }

        public int ModusHu()
        {
            int mode = 0;

            var groupResult = tourismHu.GroupBy(n => n).ToList();

            if (groupResult.Count != tourismHu.Count)
            {
                mode = tourismHu.GroupBy(n => n).
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
            int max = tourism.Max();
            return max;
        }

        public int MaxHu()
        {
            int max = tourismHu.Max();
            return max;
        }

        public int Min()
        {
            int min = tourism.Min();
            return min;
        }

        public int MinHu()
        {
            int min = tourismHu.Min();
            return min;
        }

        public double Deviation()
        {
            double avg = Avg();
            List<double> differences = new List<double>();
            foreach (var item in tourism)
            {
                double difference = Math.Pow(item - avg, 2);
                differences.Add(difference);
            }
            double differenceSum = differences.Sum();
            double deviation = Math.Sqrt(differenceSum / (tourism.Count()-1));

            return deviation;
        }

        public double DeviationHu()
        {
            double avg = AvgHu();
            List<double> differencesHu = new List<double>();
            foreach (var item in tourismHu)
            {
                double differenceHu = Math.Pow(item - avg, 2);
                differencesHu.Add(differenceHu);
            }
            double differenceSum = differencesHu.Sum();
            double deviation = Math.Sqrt(differenceSum / (tourismHu.Count()-1));

            return deviation;
        }

        // GET: Tourism
        public IActionResult Index(DateTime searchByDate)
        {
            if (searchByDate != default)
            {
                return View(_context.Tourism.Where(x => x.Date == searchByDate).ToList());
            }
            else
            {
                return View(_context.Tourism.ToList());
            }

            //return View(tourismModels);
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

        public ActionResult Statistics()
        {
            ChartListUpload();
            ViewBag.TourismForeignSpendModels = JsonConvert.SerializeObject(tourismForeignDataToChart);
            ViewBag.TourismHungarianSpendModels = JsonConvert.SerializeObject(tourismHungarianDataToChart);
            PieChartListUpload();
            //ViewBag.TourismSpendModels = JsonConvert.SerializeObject(tourismSpendDataToChart);
            ViewBag.TourismForeignModels = JsonConvert.SerializeObject(tourismDataToChart);
            ViewBag.TourismHungarianModels = JsonConvert.SerializeObject(tourismHuDataToChart);
            ListUpload();
            ViewBag.avg = Avg();
            ViewBag.avgHu = AvgHu();
            ViewBag.median = Median();
            ViewBag.medianHu = MedianHu();
            ViewBag.modus = Modus();
            ViewBag.modusHu = ModusHu();
            ViewBag.max = Max();
            ViewBag.maxHu = MaxHu();
            ViewBag.min = Min();
            ViewBag.minHu = MinHu();
            ViewBag.dev = Deviation();
            ViewBag.devHu = DeviationHu();
            return View();
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
