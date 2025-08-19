using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee_MVC.Models;

namespace Employee_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dept = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (dept == null) return NotFound();

            return View(dept);
        }

        // GET: Department/Create
        public IActionResult Create() => View();

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name")] Department dept)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(dept);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            return dept == null ? NotFound() : View(dept);
        }

        // POST: Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Name")] Department dept)
        {
            if (id != dept.DepartmentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Departments.Any(e => e.DepartmentId == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);

            return dept == null ? NotFound() : View(dept);
        }

        // POST: Department/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dept = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (dept != null)
            {
                // حذف جميع الموظفين المرتبطين بالقسم
                if (dept.Employees.Any())
                {
                    _context.Employees.RemoveRange(dept.Employees);
                }

                // حذف القسم نفسه
                _context.Departments.Remove(dept);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
