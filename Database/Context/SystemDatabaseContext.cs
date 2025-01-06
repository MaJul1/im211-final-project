using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Context;

public class SystemDatabaseContext : DbContext
{
    public DbSet<Student> Students {get; set;}
    public DbSet<Course> Courses {get; set;}
    public DbSet<Skill> Skills {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=system.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasMany(e => e.Students)
            .WithMany(e => e.Courses)
            .UsingEntity("StudentCourses");

        modelBuilder.Entity<Skill>()
            .HasMany(e => e.Students)
            .WithMany(e => e.Skills)
            .UsingEntity("StudentSkills");
    }
}
