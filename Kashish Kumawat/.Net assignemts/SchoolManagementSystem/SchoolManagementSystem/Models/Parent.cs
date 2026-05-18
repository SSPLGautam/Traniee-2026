using System;
using System.Collections.Generic;

namespace SchoolManagementSystem.Models;

public partial class Parent
{
    public int ParentId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public bool? Status { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
