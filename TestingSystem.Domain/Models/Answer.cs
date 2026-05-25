namespace TestingSystem.Domain.Models;

public class Answer
{
    public string Text { get; set; }

    public bool IsCorrect { get; set; }

    public Answer(string text, bool isCorrect)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        IsCorrect = isCorrect;
    }

    public override string ToString() => IsCorrect ? $"{Text} (+)" : Text;
}
