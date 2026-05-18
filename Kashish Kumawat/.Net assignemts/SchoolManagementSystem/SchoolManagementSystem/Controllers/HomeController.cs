using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolErpContext _context;

        public HomeController(SchoolErpContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var totalStudents = await _context.Students.CountAsync();
            var activeStudents = await _context.Students.CountAsync(s => s.Status == true);
            var totalClassrooms = await _context.Classrooms.CountAsync();
            var present = await _context.Attendances.CountAsync(a => a.Status == true);
            var absent = await _context.Attendances.CountAsync(a => a.Status == false);
            var totalAttendance = present + absent;

            string rate = "0%";
            if (totalAttendance > 0)
            {
                double pct = ((double)present / totalAttendance) * 100;
                rate = $"{Math.Round(pct, 1)}%";
            }

            var upcomingExams = await _context.Exams.CountAsync();
            var conductedExamsCount = await _context.Exams.CountAsync();
            var classPerformanceCount = await _context.Classrooms.CountAsync();
            var totalTeachers = await _context.Teachers.CountAsync();
            var totalParents = await _context.Parents.CountAsync();
            var totalCourses = await _context.Courses.CountAsync();
            var totalGrades = totalClassrooms; 

            var viewModel = new DashboardViewModel
            {
                TotalStudents = totalStudents,
                TotalClassrooms = totalClassrooms,
                ActiveStudents = activeStudents,
                InactiveStudents = totalStudents - activeStudents,
                PresentToday = present,
                AbsentToday = absent,
                AttendanceRate = rate,
                UpcomingExams = upcomingExams,
                RecentlyConductedExamsCount = conductedExamsCount,
                ClassWisePerformanceCount = classPerformanceCount,
                TotalTeachers = totalTeachers,
                TotalParentsRegistered = totalParents,
                NumberOfCoursesOffered = totalCourses,
                TotalGradesSections = totalGrades
            };

            return View(viewModel);
        }

        public IActionResult SelectTable() => View();
        public IActionResult CreateParent() => RedirectToAction("Create", "Parents");
        public IActionResult CreateStudent() => RedirectToAction("Create", "Students");
        public IActionResult CreateClassroom() => RedirectToAction("Create", "Classrooms");
        public IActionResult CreateExam() => RedirectToAction("Create", "Exams");
        public IActionResult CreateTeacher() => RedirectToAction("Create", "Teachers");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}