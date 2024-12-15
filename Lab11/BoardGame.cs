using System;
using System.Collections.Generic;

namespace Lab11
{
    public static class BoardGame
    {
        public static int GetResult(int m, int n)
        {
            if (!IsCoordinatesValid(m, n))
            {
                throw new ArgumentOutOfRangeException("Incorrect coordinates");
            }

            int maxSize = Math.Max(m, n);
            bool[,] dp = new bool[maxSize, maxSize];

            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    dp[i, j] = true;
                }
            }

            dp[0, 0] = false;

            List<int> indexOfSecondPosition = new List<int>();
            List<int> diagonals = new List<int>();

            bool ArrayInclude(List<int> arr, int val)
            {
                return arr.Contains(val);
            }

            for (int i = 1; i < maxSize; i++)
            {
                for (int j = i + 1; j < maxSize; j++)
                {
                    if (ArrayInclude(indexOfSecondPosition, i) || ArrayInclude(indexOfSecondPosition, j) || ArrayInclude(diagonals, Math.Abs(i - j)))
                    {
                        dp[i, j] = true;
                    }
                    else if ((j > 0 && dp[i, j - 1]) && (i > 0 && dp[i - 1, j]) && (i > 0 && j > 0 && dp[i - 1, j - 1]))
                    {
                        dp[i, j] = false;
                        dp[j, i] = false;

                        indexOfSecondPosition.Add(i);
                        indexOfSecondPosition.Add(j);
                        diagonals.Add(Math.Abs(i - j));
                    }
                }
            }

            return dp[m - 1, n - 1] ? 1 : 2;
        }
        public static bool IsCoordinatesValid(int m, int n)
        {
            return m > 0 && n > 0 && m < 251 && n < 251;
        }
    }
}
