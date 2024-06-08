using EFCore.Inherritace.BusinessDomain.UserAggregate;

namespace EFCore.Inherritace.BusinessDomain.CourseAggregate;

public abstract class Course
{
  public Course() { }

  public Course(string name, int price)
  {
    this.Name = name;
    this.Price = price;
  }

  public Guid Id { get; set; }

  private string _name = null!;
  public string Name
  {
    get => _name;
    set
    {
      string fixedValue = (value ?? "").Trim();

      if (string.IsNullOrEmpty(fixedValue))
        throw new Exception("Course name cannot be empty");

      _name = fixedValue;
    }
  }

  private int _price;
  public int Price
  {
    get => _price;

    set
    {
      if (value < 0)
        throw new Exception("Price of this course cannot be negative");
      
      _price = value;
    }
  }

  public ICollection<Student> EnrolledStudents = new List<Student>();
}
