

public class Program
{
    public static void Main(string[] args)
    {
        string inputFilePath = args.Length > 0 ? args[0] : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\INPUT.txt");
        string outputFilePath = args.Length > 1 ? args[1] : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\OUTPUT.txt");

        var (n, particles, destructionMatrix) = Utils.ReadInput(inputFilePath);

        var finalStates = ParticleProcessor.ProcessParticles(n, particles, destructionMatrix);

        Utils.WriteOutput(outputFilePath, finalStates);
    }
}

public static class Utils
{
    public static (int n, List<int> particles, bool[,] destructionMatrix) ReadInput(string inputFilePath)
    {
        string[] inputLines = File.ReadAllLines(inputFilePath);

        int n = int.Parse(inputLines[0]);
        List<int> particles = new List<int>(Array.ConvertAll(inputLines[1].Split(), int.Parse));

        bool[,] destructionMatrix = new bool[n, n];
        for (int i = 0; i < n; i++)
        {
            var line = Array.ConvertAll(inputLines[i + 2].Split(), int.Parse);
            for (int j = 0; j < n; j++)
            {
                destructionMatrix[i, j] = (line[j] != 0);
            }
        }

        return (n, particles, destructionMatrix);
    }

    public static void WriteOutput(string outputFilePath, HashSet<List<int>> finalStates)
    {
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            writer.WriteLine(finalStates.Count);
            foreach (var state in finalStates)
            {
                writer.WriteLine(string.Join(" ", state));
            }
        }
    }
}

public class ListComparer : IEqualityComparer<List<int>>
{
    public bool Equals(List<int> x, List<int> y)
    {
        if (x.Count != y.Count) return false;
        for (int i = 0; i < x.Count; i++)
        {
            if (x[i] != y[i]) return false;
        }
        return true;
    }

    public int GetHashCode(List<int> obj)
    {
        int hash = 17;
        foreach (var item in obj)
        {
            hash = hash * 23 + item.GetHashCode();
        }
        return hash;
    }
}
public static class ParticleProcessor
{
    public static HashSet<List<int>> ProcessParticles(int n, List<int> particles, bool[,] destructionMatrix)
    {
        HashSet<List<int>> allStates = new HashSet<List<int>>(new ListComparer());
        Queue<List<int>> queue = new Queue<List<int>>();
        allStates.Add(new List<int>(particles));
        queue.Enqueue(new List<int>(particles));

        HashSet<List<int>> finalStates = new HashSet<List<int>>(new ListComparer());

        while (queue.Count > 0)
        {
            List<int> current = queue.Dequeue();
            bool isFinal = true;

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    if (i == j && current[i] >= 2 && destructionMatrix[i, i])
                    {
                        var next = new List<int>(current);
                        next[i]--;
                        isFinal = false;
                        EnqueueState(next, allStates, queue);
                    }
                    else if (i != j)
                    {
                        ProcessPair(i, j, current, destructionMatrix, allStates, queue, ref isFinal);
                    }
                }
            }

            if (isFinal)
            {
                finalStates.Add(current);
            }
        }

        return finalStates;
    }

    private static void ProcessPair(int i, int j, List<int> current, bool[,] destructionMatrix, HashSet<List<int>> allStates, Queue<List<int>> queue, ref bool isFinal)
    {
        if (current[i] > 0 && current[j] > 0)
        {
            if (destructionMatrix[i, j])
            {
                var next = new List<int>(current);
                next[j]--;
                isFinal = false;
                EnqueueState(next, allStates, queue);
            }
            if (destructionMatrix[j, i])
            {
                var next = new List<int>(current);
                next[i]--;
                isFinal = false;
                EnqueueState(next, allStates, queue);
            }
        }
    }

    private static void EnqueueState(List<int> state, HashSet<List<int>> allStates, Queue<List<int>> queue)
    {
        if (!allStates.Contains(state))
        {
            allStates.Add(state);
            queue.Enqueue(state);
        }
    }
}