using System;
using System.Collections.Generic;

namespace Lab11
{
    public static class GoGame
    {
        public static char[] ParseAndValidateBoardLine(string input, int expectedLength)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("The board line is empty or contains only spaces.");
            }

            if (input.Length != expectedLength)
            {
                throw new ArgumentException($"The board line must have exactly {expectedLength} characters.");
            }

            foreach (char c in input)
            {
                if (c != 'B' && c != 'W' && c != '.')
                {
                    throw new ArgumentException("The board line contains invalid characters. Only 'B', 'W', or '.' are allowed.");
                }
            }

            return input.ToCharArray();
        }

        public static int CountGroupsInAtari(char[][] board, int N)
        {
            bool[][] visited = new bool[N][];
            for (int i = 0; i < N; i++)
                visited[i] = new bool[N];

            int groupsInAtari = 0;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (board[i][j] == 'B' && !visited[i][j])
                    {
                        var (group, dame) = DFS(board, visited, N, i, j);
                        if (dame.Count == 1)
                        {
                            groupsInAtari++;
                        }
                    }
                }
            }

            return groupsInAtari;
        }

        private static (List<(int, int)> group, HashSet<(int, int)> dame) DFS(char[][] board, bool[][] visited, int N, int x, int y)
        {
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            var stack = new Stack<(int, int)>();
            stack.Push((x, y));
            visited[x][y] = true;

            var group = new List<(int, int)>();
            group.Add((x, y));

            var dame = new HashSet<(int, int)>();

            while (stack.Count > 0)
            {
                var (cx, cy) = stack.Pop();

                for (int i = 0; i < 4; i++)
                {
                    int nx = cx + dx[i];
                    int ny = cy + dy[i];

                    if (nx >= 0 && nx < N && ny >= 0 && ny < N)
                    {
                        if (board[nx][ny] == 'B' && !visited[nx][ny])
                        {
                            visited[nx][ny] = true;
                            stack.Push((nx, ny));
                            group.Add((nx, ny));
                        }
                        else if (board[nx][ny] == '.')
                        {
                            dame.Add((nx, ny));
                        }
                    }
                }
            }

            return (group, dame);
        }
    }
}
