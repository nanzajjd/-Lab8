using TestingSystem.Domain.Interfaces;
using TestingSystem.Domain.Models;

namespace TestingSystem.Domain.Services;

public class QuestionBank : ISearchable<Question>
{
    private readonly List<Question> _questions = new();
    public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

    public void Add(Question question)
    {
        if (question == null)
            throw new ArgumentNullException(nameof(question));

        _questions.Add(question);
    }

    public void Remove(Question question)
    {
        if (!_questions.Remove(question))
            throw new InvalidOperationException("Question not found in the bank.");
    }

    public void Update(Question question, string newText)
    {
        if (question == null) throw new ArgumentNullException(nameof(question));
        if (newText == null) throw new ArgumentNullException(nameof(newText));
        if (!_questions.Contains(question))
            throw new InvalidOperationException("Question not found in the bank.");

        question.Name = newText;
    }

    public IEnumerable<Question> Search(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return _questions;

        return _questions.Where(q =>
            q.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}
