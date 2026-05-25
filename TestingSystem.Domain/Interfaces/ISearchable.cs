namespace TestingSystem.Domain.Interfaces;

public interface ISearchable<T>
{
    IEnumerable<T> Search(string keyword);
}
