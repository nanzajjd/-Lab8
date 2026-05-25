using TestingSystem.Domain.Interfaces;

namespace TestingSystem.Domain.Models;
public class Question : BaseEntity, ISearchable<Answer>
{
    private readonly List<Answer> _answers = new();

    public IReadOnlyList<Answer> Answers => _answers.AsReadOnly();

    public Question(string text) : base(text) { }

    public void AddAnswer(string text, bool isCorrect)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Answer text cannot be empty.", nameof(text));

        _answers.Add(new Answer(text, isCorrect));
    }
    public void RemoveAnswer(int index)
    {
        if (index < 0 || index >= _answers.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Answer index is out of range.");

        _answers.RemoveAt(index);
    }

    public void UpdateAnswer(int index, string newText, bool isCorrect)
    {
        if (index < 0 || index >= _answers.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Answer index is out of range.");

        _answers[index].Text = newText ?? throw new ArgumentNullException(nameof(newText));
        _answers[index].IsCorrect = isCorrect;
    }

    public bool HasCorrectAnswer() => _answers.Any(a => a.IsCorrect);

    public IEnumerable<Answer> Search(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return _answers;

        return _answers.Where(a =>
            a.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public override string ToString() => $"[Питання] {Name} ({_answers.Count} відповідей)";
}
