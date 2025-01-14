using Microsoft.EntityFrameworkCore;
using WebAppForMVC.Models.DataModels;

namespace WebAppForMVC.Context;

public class SystemDatabaseContext : DbContext
{
    public SystemDatabaseContext(DbContextOptions options) : base (options)
    {}

    public DbSet<Student> Students {get; set;}
    public DbSet<Skill> Skills {get; set;}
    public DbSet<Course> Courses {get; set;}
    public DbSet<SchoolProgram> Programs {get; set;}
    public DbSet<Department> Departments {get; set;}
    public DbSet<StudentCourse> StudentCourses {get; set;}
    public DbSet<StudentSkill> StudentSkills {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>()
            .HasMany(s => s.Students)
            .WithMany(s => s.Skills)
            .UsingEntity<StudentSkill>(
                s => s
                    .HasOne(s => s.Student)
                    .WithMany(s => s.StudentSkills)
                    .HasForeignKey(s => s.StudentId),
                s => s
                    .HasOne(s => s.Skill)
                    .WithMany(s => s.StudentSkills)
                    .HasForeignKey(s => s.SkillId),
                s => s.HasKey(ss => new {ss.StudentId, ss.SkillId})
            );

        modelBuilder.Entity<Course>()
            .HasMany(s => s.Students)
            .WithMany(s => s.Courses)
            .UsingEntity<StudentCourse>(
                s => s
                    .HasOne(s => s.Student)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(s => s.StudentId),
                s => s
                    .HasOne(s => s.Course)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(s => s.CourseId),
                s => s.HasKey(s => new{s.StudentId, s.CourseId})
            );
        
        modelBuilder.Entity<SchoolProgram>(s => 
        {
            s.HasMany(s => s.Students)
            .WithOne(s => s.Program)
            .HasForeignKey("ProgramId");

            s.HasIndex(s => s.Code)
                .IsUnique();

        });

        modelBuilder.Entity<Department>(d => 
        {
            d.HasMany(s => s.Students)
            .WithOne(s => s.Department)
            .HasForeignKey("DepartmentId");

            d.HasIndex(d => d.Code)
                .IsUnique();

        });
    }

}
