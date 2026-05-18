using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class ExamResultsController : Controller
    {
        private readonly SchoolErpContext _context;

        public ExamResultsController(SchoolErpContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExamResults.ToListAsync());
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var examResult = await _context.ExamResults
                .FirstOrDefaultAsync(m => m.Id == id);

            if (examResult == null)
                return NotFound();

            return View(examResult);
        }

        // CREATE GET
        public IActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseId");

            ViewBag.ExamId = new SelectList(_context.Exams, "ExamId", "ExamId");

            ViewBag.StudentId = new SelectList(_context.Students, "StudentId", "StudentId");

            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
[Bind("ExamId,StudentId,CourseId,Marks")] ExamResult examResult)
        {
            ModelState.Remove("Course");
            ModelState.Remove("Exam");
            ModelState.Remove("Student");

            if (ModelState.IsValid)
            {
                examResult.Id = 0;

                _context.ExamResults.Add(examResult);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseId");

            ViewBag.ExamId = new SelectList(_context.Exams, "ExamId", "ExamId");

            ViewBag.StudentId = new SelectList(_context.Students, "StudentId", "StudentId");

            return View(examResult);
        }





        // EDIT GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var examResult = await _context.ExamResults.FindAsync(id);

            if (examResult == null)
                return NotFound();

            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseId", examResult.CourseId);

            ViewBag.ExamId = new SelectList(_context.Exams, "ExamId", "ExamId", examResult.ExamId);

            ViewBag.StudentId = new SelectList(_context.Students, "StudentId", "StudentId", examResult.StudentId);

            return View(examResult);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExamResult examResult)
        {
            if (id != examResult.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(examResult);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(examResult);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var examResult = await _context.ExamResults
                .FirstOrDefaultAsync(m => m.Id == id);

            if (examResult == null)
                return NotFound();

            return View(examResult);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examResult = await _context.ExamResults.FindAsync(id);

            if (examResult != null)
            {
                _context.ExamResults.Remove(examResult);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}