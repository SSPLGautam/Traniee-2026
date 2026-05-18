using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly SchoolErpContext _context;

        public AttendancesController(SchoolErpContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var schoolErpContext = _context.Attendances.Include(a => a.Student);
            return View(await schoolErpContext.ToListAsync());
        }

        // GET: Attendances/Details
        public async Task<IActionResult> Details(DateOnly? date, int? studentId)
        {
            if (date == null || studentId == null) return NotFound();

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Date == date && m.StudentId == studentId);

            if (attendance == null) return NotFound();

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Fname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,StudentId,Status,Remark")] Attendance attendance)
        { 
            ModelState.Remove("Student");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(attendance);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Is student ka data already inserted h. Dusre student ka data insert kro. Thank you !");
                }
            }

            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Fname", attendance.StudentId);
            return View(attendance);
        }

        // GET: Attendances/Edit
        public async Task<IActionResult> Edit(DateOnly? date, int? studentId)
        {
            if (date == null || studentId == null) return NotFound();

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.Date == date && m.StudentId == studentId);

            if (attendance == null) return NotFound();

            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Fname", attendance.StudentId);
            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateOnly date, int studentId, [Bind("Date,StudentId,Status,Remark")] Attendance attendance)
        {
            if (date != attendance.Date || studentId != attendance.StudentId) return NotFound();

            ModelState.Remove("Student");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Date, attendance.StudentId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Fname", attendance.StudentId);
            return View(attendance);
        }

        // GET: Attendances/Delete
        public async Task<IActionResult> Delete(DateOnly? date, int? studentId)
        {
            if (date == null || studentId == null) return NotFound();

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Date == date && m.StudentId == studentId);

            if (attendance == null) return NotFound();

            return View(attendance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateOnly date, int studentId)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.Date == date && m.StudentId == studentId);

            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(DateOnly date, int studentId)
        {
            return _context.Attendances.Any(e => e.Date == date && e.StudentId == studentId);
        }
    }
}