using System;
using System.Collections.Generic;

namespace SchoolManagementSystem.Models;

public partial class ExamResult
{
    public int Id { get; set; }

    public int ExamId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public string? Marks { get; set; }
}
