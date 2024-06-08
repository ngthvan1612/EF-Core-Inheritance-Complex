using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

namespace EFCore.Inherritace;

public class Program
{
  private static void P1()
  {
    using var dbs = new AppDbContext(delete: true);

    var student01 = new Student()
    {
      Username = "student01",
      FacebookUrl = "https://www.facebook.com/abcklsd"
    };

    var teacher01 = new Teacher()
    {
      Username = "teacher01",
      Education = "Master"
    };

    dbs.Users.AddAsync(teacher01);
    dbs.Users.AddAsync(student01);

    var englishCourse = new TeachingCourse()
    {
      Price = 15000,
      Name = "Lop hoc tieng anh",
      MaximumNumberOfStudents = 15
    };

    teacher01.Teach(englishCourse);

    dbs.SaveChanges();
  }

  private static void P2()
  {
    var dbs = new AppDbContext();
    
    var franceCourse = new TeachingCourse()
    {
      Price = 15222,
      Name = "Lop hoc tieng phap",
      MaximumNumberOfStudents = 22
    };

    var teacher = dbs.Teachers.First(u => u.Username == "teacher01");
    teacher.Teach(franceCourse);

    dbs.SaveChanges();
  }

  public static void Main(string[] args)
  {
    P1();
    P2();
  }
}

public abstract class Course
{
  public Guid Id { get; set;}

  public string Name { get; set; } = null!;

  public int Price { get; set; }
}

public abstract class User
{
  public Guid Id { get; set; }

  public string Username { get; set; } = null!;

  public abstract string Role { get; }
}

public class Student : User
{
  public string FacebookUrl { get; set; } = null!;

  public override string Role => "STUDENT";
}

public class Teacher : User
{
  public string Education { get; set; } = null!;

  public override string Role => "TEACHER";

  public ICollection<TeachingCourse> MyCourses { get; set; } = new List<TeachingCourse>();

  public void Teach(TeachingCourse course)
  {
    this.MyCourses.Add(course);
    course.Teacher = this;
  }
}

public class TeachingCourse : Course
{
  public Guid TeacherId { get; set; }

  public Teacher Teacher { get; set; } = null!;

  public int MaximumNumberOfStudents { get; set; }
}

public class SelfStudyCourse : Course
{
  public string SelfStudyTips { get; set; } = null!;
}

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
      .HasValue<Student>("STUDENT")
      .HasValue<Teacher>("TEACHER");

    // modelBuilder.Entity<User>().UseTpcMappingStrategy();
    modelBuilder.Entity<Course>().UseTpcMappingStrategy();

    modelBuilder.Entity<Teacher>()
      .HasMany(u => u.MyCourses)
      .WithOne(u =>  u.Teacher);
  }
}
