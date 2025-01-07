using System;
using Microsoft.EntityFrameworkCore;
using WebAppForMVC.Models;

namespace WebAppForMVC.Context;

public class SystemDatabaseContext : DbContext
{
    public SystemDatabaseContext(DbContextOptions options) : base (options)
    {}

    public DbSet<Student> Students {get; set;}
    public DbSet<Skill> Skills {get; set;}
    public DbSet<Course> Courses {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(e =>
        {
            e.HasMany(e => e.Students)
            .WithMany(e => e.Courses)
            .UsingEntity("StudentCourses");

            e.HasIndex(s => s.CourseCode)
            .IsUnique();

        });
        
        modelBuilder.Entity<Skill>()
            .HasMany(e => e.Students)
            .WithMany(e => e.Skills)
            .UsingEntity("StudentSkills");
    }

}
