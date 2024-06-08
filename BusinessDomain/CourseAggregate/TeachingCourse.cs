using EFCore.Inherritace.BusinessDomain.UserAggregate;

namespace EFCore.Inherritace.BusinessDomain.CourseAggregate;

public class TeachingCourse : Course
{
  public Guid TeacherId { get; set; }

  public Teacher Teacher { get; set; } = null!;

  public int MaximumNumberOfStudents
  {
    get => _maximumNumberOfStudents;
    set
    {
      if (value < 0)
        throw new Exception("Maximum number of this course cannot be negative");

      _maximumNumberOfStudents = value;
    }
  }

  private int _maximumNumberOfStudents;

  public class Builder
  {
    private TeachingCourse teachingCourse = null!;

    private Builder()
    {
      this.teachingCourse = new();
    }

    public static Builder Create() => new Builder();

    public Builder SetMaximumNumberOfStudents(int maximumNumberOfStudents)
    {
      this.teachingCourse.MaximumNumberOfStudents = maximumNumberOfStudents;
      return this;
    }

    public Builder SetName(string name)
    {
      this.teachingCourse.Name = name;
      return this;
    }

    public Builder SetPrice(int price)
    {
      this.teachingCourse.Price = price;
      return this;
    }

    public TeachingCourse Build()
    {
      return this.teachingCourse;
    }
  }
}
