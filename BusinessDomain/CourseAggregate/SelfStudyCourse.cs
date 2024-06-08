namespace EFCore.Inherritace.BusinessDomain.CourseAggregate;

public class SelfStudyCourse : Course
{
  public SelfStudyCourse() { }

  public SelfStudyCourse(string name, int price) : base(name, price)
  {

  }

  public string Tips { get; set; } = null!;

  public class Builder
  {
    private SelfStudyCourse selfStudyCourse = null!;

    private Builder()
    {
      this.selfStudyCourse = new();
    }

    public static Builder Create() => new Builder();

    public Builder SetTips(string tips)
    {
      this.selfStudyCourse.Tips = tips;
      return this;
    }

    public Builder SetName(string name)
    {
      this.selfStudyCourse.Name = name;
      return this;
    }

    public Builder SetPrice(int price)
    {
      this.selfStudyCourse.Price = price;
      return this;
    }

    public SelfStudyCourse Build()
    {
      return this.selfStudyCourse;
    }
  }
}
