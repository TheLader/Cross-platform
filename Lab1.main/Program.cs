namespace Chess
{
    public class Vector2Int
    {
        public int x, y;
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2Int(Vector2Int vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }
        public static Vector2Int operator -(Vector2Int vector1, Vector2Int vector2)
        {
            return new Vector2Int(vector1.x - vector2.x, vector1.y - vector2.y);
        }
    }
    public static class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                string input = "INPUT.txt";
                string output = "OUTPUT.txt";
                string cell1 = "";
                string cell2 = "";

                string content = File.ReadAllText(input);
                string[] parts = content.Split(", ");

                cell1 = parts[0];
                cell2 = parts[1];


                Vector2Int vector1 = StringToVector(cell1);
                Vector2Int vector2 = StringToVector(cell2);
                if (CanReachInOneMove(vector1, vector2))
                {
                    Console.WriteLine(1);
                    File.WriteAllText(output, "1");
                }
                else if (CanReachInTwoMove(vector1, vector2))
                {
                    Console.WriteLine(2);
                    File.WriteAllText(output, "2");
                }
                else
                {
                    Console.WriteLine("NO");
                    File.WriteAllText(output, "NO");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static bool CanReachInOneMove(Vector2Int from, Vector2Int to)
        {
            Vector2Int vector = from - to;
            int lenght = Math.Abs(vector.x * vector.y);
            return lenght == 2;
        }
        public static bool CanReachInTwoMove(Vector2Int from, Vector2Int to)
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    Vector2Int interVector = new Vector2Int(i, j);
                    if (CanReachInOneMove(from, interVector))
                    {
                        if (CanReachInOneMove(interVector, to))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static Vector2Int StringToVector(string s)
        {
            int x = -100, y;
            switch (s[0])
            {
                case 'a':
                    {
                        x = 1;
                        break;
                    }
                case 'b':
                    {
                        x = 2;
                        break;
                    }
                case 'c':
                    {
                        x = 3;
                        break;
                    }
                case 'd':
                    {
                        x = 4;
                        break;
                    }
                case 'e':
                    {
                        x = 5;
                        break;
                    }
                case 'f':
                    {
                        x = 6;
                        break;
                    }
                case 'g':
                    {
                        x = 7;
                        break;
                    }
                case 'h':
                    {
                        x = 8;
                        break;
                    }
            }
            int.TryParse(s[1].ToString(), out y);
            y = y <= 8 ? y : -100;
            y = x != -100 ? y : -100;
            return new Vector2Int(x, y);
        }
    }
}