using System.Windows.Markup;
using EFCore.Inherritace.BusinessDomain.CourseAggregate;

namespace EFCore.Inherritace.BusinessDomain.UserAggregate;

public class Student : User
{
  public Student() : base(UserRole.STUDENT)
  {
  }

  public Student(string username)
    : base(username, UserRole.STUDENT)
  {
  }

  public string FacebookUrl { get; set; } = null!;

  public ICollection<Course> MyCourses = new HashSet<Course>();

  public void EnrollCourse(Course course)
  {
    this.MyCourses.Add(course);
    course.EnrolledStudents.Add(this);
  }

  public class Builder
  {
    private Student student = null!;

    private Builder()
    {
      this.student = new();
    }

    public static Builder Create() => new Builder();

    public Builder SetFacebookUrl(string facebookUrl)
    {
      this.student.FacebookUrl = facebookUrl;
      return this;
    }

    public Builder SetUsername(string username)
    {
      this.student.Username = username;
      return this;
    }

    public Student Build()
    {
      return this.student;
    }
  }
}
