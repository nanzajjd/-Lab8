using TestingSystem.Domain.Interfaces;
using TestingSystem.Domain.Models;

namespace TestingSystem.Domain.Services;

public class TestManager : ISearchable<Test>
{
    private readonly List<Test> _tests = new();

    public IReadOnlyList<Test> Tests => _tests.AsReadOnly();

    public Test CreateTest(string name, int questionCount, int secondsPerQuestion)
    {
        var test = new Test(name, questionCount, secondsPerQuestion);
        _tests.Add(test);
        return test;
    }

    public void UpdateTest(Test test, int newQuestionCount, int newSecondsPerQuestion)
    {
        if (!_tests.Contains(test))
            throw new InvalidOperationException("Test not found in the manager.");

        test.QuestionCount = newQuestionCount;
        test.SecondsPerQuestion = newSecondsPerQuestion;
    }

    public void RemoveTest(Test test)
    {
        if (!_tests.Remove(test))
            throw new InvalidOperationException("Test not found in the manager.");
    }

    public IEnumerable<Test> Search(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return _tests;

        return _tests.Where(t =>
            t.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public void PrintAllStatistics()
    {
        if (_tests.Count == 0)
        {
            Console.WriteLine("Тестів ще немає.");
            return;
        }

        foreach (var test in _tests)
            Console.WriteLine(test.GetStatistics());
    }
}
