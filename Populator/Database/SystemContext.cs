using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Populator.Database;

public partial class SystemContext : DbContext
{
    private string _connectionString;
    public SystemContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    #pragma warning disable
    public SystemContext(DbContextOptions<SystemContext> options)
        : base(options)
    {
    }
    #pragma warning enable

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<SchoolProgram> Programs { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={_connectionString}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(e => e.Code, "IX_Departments_Code").IsUnique();
        });

        modelBuilder.Entity<SchoolProgram>(entity =>
        {
            entity.HasIndex(e => e.Code, "IX_Programs_Code").IsUnique();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Students_DepartmentId");

            entity.HasIndex(e => e.ProgramId, "IX_Students_ProgramId");

            entity.HasOne(d => d.Department).WithMany(p => p.Students).HasForeignKey(d => d.DepartmentId);

            entity.HasOne(d => d.Program).WithMany(p => p.Students).HasForeignKey(d => d.ProgramId);

            entity.HasMany(d => d.Courses).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentCourse",
                    r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                    l => l.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
                    j =>
                    {
                        j.HasKey("StudentId", "CourseId");
                        j.ToTable("StudentCourses");
                        j.HasIndex(new[] { "CourseId" }, "IX_StudentCourses_CourseId");
                    });

            entity.HasMany(d => d.Skills).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentSkill",
                    r => r.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                    l => l.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
                    j =>
                    {
                        j.HasKey("StudentId", "SkillId");
                        j.ToTable("StudentSkills");
                        j.HasIndex(new[] { "SkillId" }, "IX_StudentSkills_SkillId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
