namespace TestingSystem.Domain.Models;

public abstract class BaseEntity
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set; }

    public DateTime CreatedAt { get; } = DateTime.Now;

    protected BaseEntity(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null.");
    }

    public abstract override string ToString();
}
