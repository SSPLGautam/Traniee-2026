using System;
using System.Collections.Generic;

namespace SchoolManagementSystem.Models;

public partial class Classroom
{
    public int ClassroomId { get; set; }

    public int? Year { get; set; }

    public string? Section { get; set; }

    public bool? Status { get; set; }

    public string? Remarks { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
