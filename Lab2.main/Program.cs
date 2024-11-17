public static class Program
{
    public struct Agent
    {
        public int Age;
        public int Risk;
    }
    public static Agent[] ConvertStringToAgent(string[] str)
    {      
        var agents = str[1]
            .Split(' ')
            .Select((x, i) => new { Value = int.Parse(x), Index = i })
            .GroupBy(x => x.Index / 2)
            .Select(g => new Agent
            {
                Age = g.ElementAt(0).Value,
                Risk = g.ElementAt(1).Value
            })
            .ToArray();
        return agents.OrderBy(agent => agent.Age).ToArray();

    }

    static void Main()
    {
        // Зчитування даних із файлу
        var input = File.ReadAllLines("INPUT.TXT");

        int n = int.Parse(input[0]); // Кількість агентів        

        // Сортуємо агентів за віком
        var sortedAgents = ConvertStringToAgent(input);
        int totalRisk = CalculateRisk(n, sortedAgents);

        // Виведення результату у файл
        File.WriteAllText("OUTPUT.TXT", totalRisk.ToString());
    }

    public static int CalculateRisk(int n, Agent[] sortedAgents)
    {
        int totalRisk = 0;

        // Ітерація по парах і підрахунок ризику
        for (int i = 0; i < n - 1; i++)
        {
            totalRisk += sortedAgents[i + 1].Risk;
        }

        return totalRisk;
    }
}
