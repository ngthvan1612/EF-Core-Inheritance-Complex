using EFCore.Inherritace.BusinessDomain.CourseAggregate;

namespace EFCore.Inherritace.BusinessDomain.UserAggregate;

public class Teacher : User
{
  public Teacher() : base(UserRole.TEACHER)
  {
  }

  public Teacher(string username)
    : base(username, UserRole.TEACHER)
  {
  }

  public string Education { get; set; } = null!;

  public ICollection<TeachingCourse> MyCourses { get; set; } = new List<TeachingCourse>();

  public void Teach(TeachingCourse course)
  {
    if (course is null)
      throw new Exception("Teaching course cannot be null");

    this.MyCourses.Add(course);
    course.Teacher = this;
  }

  public class Builder
  {
    private Teacher teacher = null!;

    private Builder()
    {
      this.teacher = new();
    }

    public static Builder Create() => new Builder();

    public Builder SetEducation(string education)
    {
      this.teacher.Education = education;
      return this;
    }

    public Builder SetUsername(string username)
    {
      this.teacher.Username = username;
      return this;
    }

    public Builder TeachCourse(TeachingCourse course)
    {
      this.teacher.Teach(course);
      return this;
    }

    public Teacher Build()
    {
      return this.teacher;
    }
  }
}