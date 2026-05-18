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
    public class ExamTypesController : Controller
    {
        private readonly SchoolErpContext _context;

        public ExamTypesController(SchoolErpContext context)
        {
            _context = context;
        }

        // GET: ExamTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExamTypes.ToListAsync());
        }

        // GET: ExamTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examType = await _context.ExamTypes
                .FirstOrDefaultAsync(m => m.ExamTypeId == id);
            if (examType == null)
            {
                return NotFound();
            }

            return View(examType);
        }

        // GET: ExamTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExamTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExamTypeId,Name,Desc")] ExamType examType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(examType);
        }

        // GET: ExamTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examType = await _context.ExamTypes.FindAsync(id);
            if (examType == null)
            {
                return NotFound();
            }
            return View(examType);
        }

        // POST: ExamTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExamTypeId,Name,Desc")] ExamType examType)
        {
            if (id != examType.ExamTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamTypeExists(examType.ExamTypeId))
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
            return View(examType);
        }

        // GET: ExamTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examType = await _context.ExamTypes
                .FirstOrDefaultAsync(m => m.ExamTypeId == id);
            if (examType == null)
            {
                return NotFound();
            }

            return View(examType);
        }

        // POST: ExamTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examType = await _context.ExamTypes.FindAsync(id);
            if (examType != null)
            {
                _context.ExamTypes.Remove(examType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamTypeExists(int id)
        {
            return _context.ExamTypes.Any(e => e.ExamTypeId == id);
        }
    }
}
