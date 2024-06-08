namespace EFCore.Inherritace.BusinessDomain.UserAggregate;

public enum UserRole
{
  STUDENT, TEACHER
}

public abstract class User
{
  public User(UserRole role)
  {
    this.Role = role;
  }

  public User(string username, UserRole role)
    : this(role)
  {
    this.Username = username;
  }

  public Guid Id { get; set; }

  public string Username { get; set; } = null!;

  public UserRole Role { get; private set; }
}
