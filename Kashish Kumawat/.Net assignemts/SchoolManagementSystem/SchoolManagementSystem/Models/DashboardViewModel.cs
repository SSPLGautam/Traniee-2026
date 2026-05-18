using System.Collections.Generic;

namespace SchoolManagementSystem.Models
{
    public class DashboardViewModel
    {
        
        public int TotalStudents { get; set; }
        public int TotalClassrooms { get; set; }
        public int ActiveStudents { get; set; }
        public int InactiveStudents { get; set; }      
        public int PresentToday { get; set; }
        public int AbsentToday { get; set; }
        public string? AttendanceRate { get; set; }        
        public int UpcomingExams { get; set; }
        public int RecentlyConductedExamsCount { get; set; }
        public int ClassWisePerformanceCount { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalParentsRegistered { get; set; }
        public int NumberOfCoursesOffered { get; set; }
        public int TotalGradesSections { get; set; }
    }
}