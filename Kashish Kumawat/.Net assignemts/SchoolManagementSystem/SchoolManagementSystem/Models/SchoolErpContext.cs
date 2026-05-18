using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Models;

public partial class SchoolErpContext : DbContext
{
    public SchoolErpContext()
    {
    }

    public SchoolErpContext(DbContextOptions<SchoolErpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamResult> ExamResults { get; set; }

    public virtual DbSet<ExamType> ExamTypes { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-OFI0SIG;Database=school_erp;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.StudentId }).HasName("PK__attendan__6B7D11958F64D76B");

            entity.ToTable("attendance");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Remark)
                .HasColumnType("text")
                .HasColumnName("remark");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__attendanc__stude__160F4887");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.ClassroomId).HasName("PK__classroo__448E90B86B6189E7");

            entity.ToTable("classroom");

            entity.Property(e => e.ClassroomId).HasColumnName("classroom_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("remarks");
            entity.Property(e => e.Section)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("section");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasMany(d => d.Students).WithMany(p => p.Classrooms)
                .UsingEntity<Dictionary<string, object>>(
                    "ClassroomStudent",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__classroom__stude__19DFD96B"),
                    l => l.HasOne<Classroom>().WithMany()
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__classroom__class__18EBB532"),
                    j =>
                    {
                        j.HasKey("ClassroomId", "StudentId").HasName("PK__classroo__F62DA0D17BB95C98");
                        j.ToTable("classroom_student");
                        j.IndexerProperty<int>("ClassroomId").HasColumnName("classroom_id");
                        j.IndexerProperty<int>("StudentId").HasColumnName("student_id");
                    });
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__course__8F1EF7AE6E78DD1D");

            entity.ToTable("course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__exam__9C8C7BE993F1FAAC");

            entity.ToTable("exam");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.ExamTypeId).HasColumnName("exam_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.ExamType).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ExamTypeId)
                .HasConstraintName("FK__exam__exam_type___6A30C649");
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__exam_res__3214EC0728383663");

            entity.ToTable("exam_result");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.Marks)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("marks");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
        });

        modelBuilder.Entity<ExamType>(entity =>
        {
            entity.HasKey(e => e.ExamTypeId).HasName("PK__exam_typ__FAB1396DE961C7A2");

            entity.ToTable("exam_type");

            entity.Property(e => e.ExamTypeId).HasColumnName("exam_type_id");
            entity.Property(e => e.Desc)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("desc");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentId).HasName("PK__parent__F2A60819B34729B1");

            entity.ToTable("parent");

            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("fname");
            entity.Property(e => e.LastLoginDate)
                .HasColumnType("datetime")
                .HasColumnName("last_login_date");
            entity.Property(e => e.Lname)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("lname");
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__student__2A33069A0662101F");

            entity.ToTable("student");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.DateOfJoin).HasColumnName("date_of_join");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("lname");
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Parent).WithMany(p => p.Students)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__student__parent___60A75C0F");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__teacher__03AE777ED3FF2DF1");

            entity.ToTable("teacher");

            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("lname");
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
