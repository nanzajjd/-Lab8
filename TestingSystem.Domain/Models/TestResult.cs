namespace TestingSystem.Domain.Models;

public class TestResult
{
    public Student Student { get; }

    public Test Test { get; }

    public int CorrectAnswers { get; }

    public int TotalAnswered { get; }

    public DateTime CompletedAt { get; } = DateTime.Now;

    public double ScorePercent =>
        TotalAnswered == 0
            ? 0.0
            : (double)CorrectAnswers / TotalAnswered * 100;

    public TestResult(
        Student student,
        Test test,
        int correctAnswers,
        int totalAnswered)
    {
        Student = student ?? throw new ArgumentNullException(nameof(student));

        Test = test ?? throw new ArgumentNullException(nameof(test));

        if (correctAnswers < 0)
            throw new ArgumentException(
                "Correct answers cannot be negative.",
                nameof(correctAnswers));

        if (totalAnswered < 0)
            throw new ArgumentException(
                "Total answered cannot be negative.",
                nameof(totalAnswered));

        if (correctAnswers > totalAnswered)
            throw new ArgumentException(
                "Correct answers cannot exceed total answered.");

        CorrectAnswers = correctAnswers;
        TotalAnswered = totalAnswered;
    }

    public override string ToString()
    {
        return
            $"Студент: {Student.Name} | " +
            $"Результат: {CorrectAnswers}/{TotalAnswered} " +
            $"({ScorePercent:F1}%)";
    }
}