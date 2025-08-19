using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee_MVC.Models;

namespace Employee_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = _context.Employees.Include(e => e.Department);
            return View(await employees.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            ViewBag.Departments = _context.Departments.ToList();
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Update(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
