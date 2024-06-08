using System.Security.Cryptography.X509Certificates;
using EFCore.Inherritace.BusinessDomain.CourseAggregate;
using EFCore.Inherritace.BusinessDomain.UserAggregate;
using EFCore.Inherritace.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

namespace EFCore.Inherritace;

public class Program
{
  private static void InitSampleData()
  {
    using var dbs = new AppDbContext(delete: true);

    var englishCourse = TeachingCourse.Builder.Create()
      .SetPrice(15000)
      .SetName("Lop hoc tieng anh")
      .SetMaximumNumberOfStudents(15)
      .Build();

    var student01 = Student.Builder.Create()
      .SetUsername("student01")
      .SetFacebookUrl("https://www.facebook.com/abcklsd")
      .Build();

    var teacher01 = Teacher.Builder.Create()
      .SetUsername("teacher01")
      .SetEducation("Master")
      .TeachCourse(englishCourse)
      .Build();

    dbs.Users.AddAsync(teacher01);
    dbs.Users.AddAsync(student01);

    dbs.SaveChanges();
  }

  private static void TestTeacherTeachCourse()
  {
    var dbs = new AppDbContext();

    var franceCourse = TeachingCourse.Builder.Create()
      .SetPrice(15222)
      .SetName("Lop hoc tieng phap")
      .SetMaximumNumberOfStudents(22)
      .Build();

    var teacher = dbs.Teachers.First(u => u.Username == "teacher01");

    teacher.Teach(franceCourse);

    dbs.SaveChanges();
  }

  private static void TestStudentEnrollCourse()
  {
    var dbs = new AppDbContext();

    var englishCourse = dbs.TeachingCourses.First(u => u.Name == "Lop hoc tieng anh");
    var franeCourse = dbs.TeachingCourses.First(u => u.Name == "Lop hoc tieng phap");

    var student01 = dbs.Students.First(u => u.Username == "student01");
    var student02 = Student.Builder.Create()
      .SetUsername("student02")
      .Build();

    student01.EnrollCourse(englishCourse);

    student02.EnrollCourse(englishCourse);
    student02.EnrollCourse(franeCourse);

    dbs.SaveChanges();
  }

  public static void Main(string[] args)
  {
    InitSampleData();
    TestTeacherTeachCourse();
    TestStudentEnrollCourse();
  }
}
