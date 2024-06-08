namespace EFCore.Inherritace.Infrastructure.EF;

using EFCore.Inherritace.BusinessDomain.CourseAggregate;
using EFCore.Inherritace.BusinessDomain.UserAggregate;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Teacher> Teachers { get; set; } = null!;
  public DbSet<Student> Students { get; set; } = null!;
  public DbSet<Course> Courses { get; set; } = null!;
  public DbSet<SelfStudyCourse> SelfStudyCourses { get; set; } = null!;
  public DbSet<TeachingCourse> TeachingCourses { get; set; } = null!;

  public AppDbContext(bool delete = false)
  {
    if (delete)
      this.Database.EnsureDeleted();
    this.Database.EnsureCreated();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseMySQL("Server=localhost;Database=test_ef_core_inherit;Uid=root;Pwd=1234;");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>()
      .HasDiscriminator(u => u.Role)
      .HasValue<Student>(UserRole.STUDENT)
      .HasValue<Teacher>(UserRole.TEACHER);

    modelBuilder.Entity<Course>().UseTpcMappingStrategy();

    modelBuilder.Entity<Teacher>()
      .HasMany(u => u.MyCourses)
      .WithOne(u => u.Teacher);

    modelBuilder.Entity<Course>()
      .HasMany(u => u.EnrolledStudents)
      .WithMany(u => u.MyCourses);
  }
}