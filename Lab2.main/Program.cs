using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Зчитування даних із файлу
        var input = File.ReadAllLines("INPUT.TXT");

        int n = int.Parse(input[0]); // Кількість агентів
        var agents = input[1]
            .Split(' ')
            .Select((x, i) => new { Value = int.Parse(x), Index = i })
            .GroupBy(x => x.Index / 2)
            .Select(g => (Age: g.ElementAt(0).Value, Risk: g.ElementAt(1).Value))
            .ToArray();

        // Сортуємо агентів за віком
        var sortedAgents = agents.OrderBy(agent => agent.Age).ToArray();

        int totalRisk = 0;

        // Ітерація по парах і підрахунок ризику
        for (int i = 0; i < n - 1; i++)
        {
            totalRisk += sortedAgents[i + 1].Risk; // Додаємо ризик старшого агента
        }

        // Виведення результату у файл
        File.WriteAllText("OUTPUT.TXT", totalRisk.ToString());
    }
}
