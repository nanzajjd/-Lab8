namespace TestingSystem.Domain.Models;

public class Teacher : User
{
    public string Subject { get; set; }

    public Teacher(string name, string email, string subject)
        : base(name, email)
    {
        Subject = subject;
    }

    public override string ToString()
    {
        return $"Викладач: {Name}, предмет {Subject}";
    }
}