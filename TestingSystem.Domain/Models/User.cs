namespace TestingSystem.Domain.Models;

public class User : BaseEntity
{
    public string Email { get; set; }

    public User(string name, string email) : base(name)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public override string ToString()
    {
        return $"{Name} ({Email})";
    }
}