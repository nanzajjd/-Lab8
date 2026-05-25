namespace TestingSystem.Domain.Models;

public class Student : User
{
    public string Group { get; set; }

    public Student(string name, string email, string group)
        : base(name, email)
    {
        Group = group;
    }

    public override string ToString()
    {
        return $"Студент: {Name}, група {Group}";
    }
}