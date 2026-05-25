using TestingSystem.Domain.Models;

namespace TestingSystem.Domain.Services;

public class AnswerGenerator
{
    private readonly Random _random = new();

    public IReadOnlyList<Answer> Shuffle(Question question)
    {
        if (question == null)
            throw new ArgumentNullException(nameof(question));
        if (!question.HasCorrectAnswer())
            throw new InvalidOperationException(
                $"Question '{question.Name}' has no correct answer and cannot be shuffled.");

        return question.Answers
            .OrderBy(_ => _random.Next())
            .ToList()
            .AsReadOnly();
    }
}
