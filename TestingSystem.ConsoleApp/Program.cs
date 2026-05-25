using TestingSystem.Domain.Models;

var teacher = new Teacher("Василь Петрович", "teacher@test.com", "ООП");
var student = new Student("Антон", "student@test.com", "ІПЗ-11");

var test = new Test("Основи ООП", questionCount: 3, secondsPerQuestion: 30);

var q1 = new Question("Що таке інкапсуляція в ООП?");
q1.AddAnswer("Приховування внутрішнього стану об'єкта", true);
q1.AddAnswer("Можливість мати кілька батьківських класів", false);
q1.AddAnswer("Поділ програми на окремі файли", false);

var q2 = new Question("Що таке поліморфізм?");
q2.AddAnswer("Здатність об'єктів різних типів працювати через спільний інтерфейс", true);
q2.AddAnswer("Автоматичне видалення об'єктів", false);
q2.AddAnswer("Заборона зміни полів", false);

var q3 = new Question("Яке відношення описує зв'язок «є»?");
q3.AddAnswer("Наслідування", true);
q3.AddAnswer("Агрегація", false);
q3.AddAnswer("Композиція", false);

test.AddQuestion(q1);
test.AddQuestion(q2);
test.AddQuestion(q3);

Console.WriteLine("=== СИСТЕМА ТЕСТУВАННЯ ===");
Console.WriteLine(teacher);
Console.WriteLine(student);
Console.WriteLine(test);

Console.WriteLine("\n ПИТАННЯ ТЕСТУ:");

foreach (var question in test.Questions)
{
    Console.WriteLine($"\n{question.Name}");

    for (int i = 0; i < question.Answers.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {question.Answers[i]}");
    }
}

Console.WriteLine("\n РЕДАГУВАННЯ ПИТАННЯ:");
q2.Name = "Що таке поліморфізм у програмуванні?";
Console.WriteLine(q2.Name);

Console.WriteLine("\n РЕДАГУВАННЯ ВІДПОВІДІ:");

Console.WriteLine($"Стара відповідь: {q3.Answers[1].Text}");

q3.UpdateAnswer(1, "Асоціація", false);

Console.WriteLine($"Нова відповідь: {q3.Answers[1].Text}");

Console.WriteLine("\n ПРОХОДЖЕННЯ ТЕСТУ:");

int correctAnswers = 0;

foreach (var question in test.Questions)
{
    Console.WriteLine($"\n{question.Name}");

    for (int i = 0; i < question.Answers.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {question.Answers[i].Text}");
    }

    int selectedAnswer = question == q2 ? 1 : 0;

    bool isCorrect = question.Answers[selectedAnswer].IsCorrect;
    Console.WriteLine($"Обрана відповідь: {selectedAnswer + 1}. {question.Answers[selectedAnswer].Text}");

    if (isCorrect)
    {
        Console.WriteLine("✓ Правильно!");
        correctAnswers++;
    }
    else
    {
        Console.WriteLine("✗ Неправильно.");

        var rightAnswer = question.Answers.First(a => a.IsCorrect);

        Console.WriteLine($"Правильна відповідь: {rightAnswer.Text}");
    }
}

var result = new TestResult(
    student,
    test,
    correctAnswers,
    test.Questions.Count);

test.AddResult(result);

Console.WriteLine($"\n{result}");

