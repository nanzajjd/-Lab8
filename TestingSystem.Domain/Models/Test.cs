using TestingSystem.Domain.Interfaces;

namespace TestingSystem.Domain.Models;

public class Test : BaseEntity, IStatistics
{
    private readonly List<Question> _questions = new();
    private readonly List<TestResult> _results = new();

    public int QuestionCount { get; set; }
    public int SecondsPerQuestion { get; set; }

    public IReadOnlyList<Question> Questions => _questions.AsReadOnly();
    public IReadOnlyList<TestResult> Results => _results.AsReadOnly();

    public Test(string name, int questionCount, int secondsPerQuestion) : base(name)
    {
        if (questionCount <= 0)
            throw new ArgumentException("Question count must be a positive number.", nameof(questionCount));

        if (secondsPerQuestion <= 0)
            throw new ArgumentException("Seconds per question must be a positive number.", nameof(secondsPerQuestion));

        QuestionCount = questionCount;
        SecondsPerQuestion = secondsPerQuestion;
    }

    public void AddQuestion(Question question)
    {
        if (question == null)
            throw new ArgumentNullException(nameof(question));

        if (!question.HasCorrectAnswer())
            throw new InvalidOperationException($"Question '{question.Name}' must have at least one correct answer.");

        _questions.Add(question);
    }

    public void RemoveQuestion(Question question)
    {
        if (!_questions.Remove(question))
            throw new InvalidOperationException("The specified question was not found in this test.");
    }

    public void AddResult(TestResult result)
    {
        if (result == null)
            throw new ArgumentNullException(nameof(result));

        _results.Add(result);
    }

    public string GetStatistics()
    {
        if (_results.Count == 0)
            return $"Тест «{Name}»: результатів ще немає.";

        double avg = _results.Average(r => r.ScorePercent);
        double best = _results.Max(r => r.ScorePercent);

       return $"Середній результат: {avg:F1}%, найкращий результат: {best:F1}%";
    }

    public override string ToString()
    {
        return $"[Тест] «{Name}» | питань={_questions.Count}, {SecondsPerQuestion}с/питання";
    }
}