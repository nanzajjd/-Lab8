using TestingSystem.Domain.Models;

namespace TestingSystem.Domain.Services;

public class TestSession
{
    private readonly Student _student;
    private readonly Test _test;
    private readonly AnswerGenerator _generator;
    private readonly IReadOnlyList<Question> _sessionQuestions;

    private IReadOnlyList<Answer>? _currentShuffled;

    private int _currentIndex;
    private int _correctCount;
    private bool _exitedEarly;

    public bool IsFinished => _exitedEarly || _currentIndex >= _sessionQuestions.Count;

    public Question? CurrentQuestion =>
        IsFinished ? null : _sessionQuestions[_currentIndex];

    public int CurrentQuestionNumber => _currentIndex + 1;

    public int TotalQuestions => _sessionQuestions.Count;

    public TestSession(Student student, Test test, AnswerGenerator generator)
    {
        _student = student ?? throw new ArgumentNullException(nameof(student));
        _test = test ?? throw new ArgumentNullException(nameof(test));
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));

        if (test.Questions.Count < test.QuestionCount)
        {
            throw new InvalidOperationException(
                $"Тест вимагає {test.QuestionCount} питань, але доступно лише {test.Questions.Count}.");
        }

        _sessionQuestions = test.Questions
            .OrderBy(_ => Random.Shared.Next())
            .Take(test.QuestionCount)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<Answer> GetShuffledAnswers()
    {
        if (IsFinished)
            throw new InvalidOperationException("Сесія вже завершена.");

        _currentShuffled = _generator.Shuffle(CurrentQuestion!);

        return _currentShuffled;
    }

    public bool SubmitAnswer(int answerIndex)
    {
        if (IsFinished)
            throw new InvalidOperationException("Сесія вже завершена.");

        if (_currentShuffled == null)
            throw new InvalidOperationException("Спочатку викличте GetShuffledAnswers().");

        if (answerIndex < 0 || answerIndex >= _currentShuffled.Count)
            throw new ArgumentOutOfRangeException(nameof(answerIndex));

        bool isCorrect = _currentShuffled[answerIndex].IsCorrect;

        if (isCorrect)
            _correctCount++;

        _currentIndex++;
        _currentShuffled = null;

        return isCorrect;
    }

    public void ExitEarly()
    {
        _exitedEarly = true;
    }

    public TestResult Finish()
    {
        _exitedEarly = true;

        var result = new TestResult(
            _student,
            _test,
            _correctCount,
            _currentIndex);

        _test.AddResult(result);

        return result;
    }
}