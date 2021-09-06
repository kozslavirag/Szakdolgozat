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
    public class EmployeeController : Controller
    {
        List<int> employee = new List<int>();
        List<EmployeeModel> employeeDataToChart = new List<EmployeeModel>();
        List<EmployeeModel> employeeGenderDataToChart = new List<EmployeeModel>();
        List<int> employeeFemaleDataToChart = new List<int>();
        List<int> employeeMaleDataToChart = new List<int>();
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        public void ListUpload()
        {
            var employeeModel = _context.Employee.ToList();
            foreach (var item in employeeModel)
            {
                employee.Add(item.NumberofEmployee);
            }
        }

        public void ChartListUpload()
        {
            int i = 1;
            var employeeModel = _context.Employee.ToList();
            foreach (var item in employeeModel)
            {
                employeeDataToChart.Add(new EmployeeModel(i++, item.NumberofEmployee, item.Date.ToString("yyyy.MM.dd")));
            }
        }

        public void PieChartListUpload()
        {
            var employeeModel = _context.Employee.ToList();
            foreach (var item in employeeModel)
            {
                employeeFemaleDataToChart.Add(item.FemaleEmployee);
                employeeMaleDataToChart.Add(item.MaleEmployee);
            }
            employeeGenderDataToChart.Add(new EmployeeModel("Foglalkoztatottak száma (Nő)", employeeFemaleDataToChart.Sum()));
            employeeGenderDataToChart.Add(new EmployeeModel("Foglalkoztatottak száma (Férfi)", employeeMaleDataToChart.Sum()));
        }

        public double Avg()
        {

            double avg = employee.Average();
            return avg;
        }

        public int Median()
        {
            int salesCount = employee.Count();
            int halfIndex = employee.Count() / 2;
            var sortedNumbers = employee.OrderBy(n => n);
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

            var groupResult = employee.GroupBy(n => n).ToList();

            if (groupResult.Count != employee.Count)
            {
                mode = employee.GroupBy(n => n).
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
            int max = employee.Max();
            return max;
        }

        public int Min()
        {
            int min = employee.Min();
            return min;
        }

        public double Deviation()
        {
            double avg = Avg();
            List<double> differences = new List<double>();
            foreach (var item in employee)
            {
                double difference = Math.Pow(item - avg, 2);
                differences.Add(difference);
            }
            double differenceSum = differences.Sum();
            double deviation = Math.Sqrt(differenceSum / employee.Count());

            return deviation;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.Employee
                .FirstOrDefaultAsync(m => m.Date == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        public ActionResult Statistics()
        {
            ChartListUpload();
            ViewBag.EmployeeModels = JsonConvert.SerializeObject(employeeDataToChart);
            PieChartListUpload();
            ViewBag.EmployeeGenderModels = JsonConvert.SerializeObject(employeeGenderDataToChart);
            ListUpload();
            ViewBag.avg = Avg();
            ViewBag.median = Median();
            ViewBag.modus = Modus();
            ViewBag.max = Max();
            ViewBag.min = Min();
            ViewBag.dev = Deviation();
            return View();
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,NumberofEmployee,MaleEmployee,FemaleEmployee")] EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.Employee.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,NumberofEmployee,MaleEmployee,FemaleEmployee")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(employeeModel.Date))
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
            return View(employeeModel);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.Employee
                .FirstOrDefaultAsync(m => m.Date == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var employeeModel = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employeeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(DateTime id)
        {
            return _context.Employee.Any(e => e.Date == id);
        }
    }
}
