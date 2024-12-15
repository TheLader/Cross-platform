using System;
using System.IO;
using System.Linq;

namespace Lab11
{
    public class LabRunner
    {
        public string RunLab1(string inputFilePath)
        {
            string[] lines = File.ReadLines(inputFilePath).Take(2).ToArray();

            if (lines.Length < 2)
            {
                throw new ArgumentException("The file must contain two lines: the number of days and the prices.");
            }

            int days = DocumentUtils.ParseAndValidateNumber(lines[0]);
            if (days < 1 || days > 100)
            {
                throw new ArgumentException("The number of days is incorrect.");
            }

            int[] prices = DocumentUtils.ParseAndValidateLine(lines[1]);

            if (prices.Length != days)
            {
                throw new ArgumentException("The number of prices does not match the number of days.");
            }

            int result = ProfitUtils.GetMaxProfit(prices);

            return result.ToString();
        }

        public string RunLab2(string inputFilePath)
        {
            string[] lines = File.ReadLines(inputFilePath).Take(2).ToArray();

            if (lines.Length != 1)
            {
                throw new ArgumentException("The file must contain one line: the coordinates of the queen.");
            }

            int[] coordinates = DocumentUtils.ParseAndValidateLine(lines[0]);

            const int coordinatesSize = 2;
            if (coordinates.Length != coordinatesSize)
            {
                throw new ArgumentException("Coordinates must consist of two numbers.");
            }

            int result = BoardGame.GetResult(coordinates[0], coordinates[1]);

            return result.ToString();
        }

        public string RunLab3(string inputFilePath)
        {
            string[] lines = File.ReadLines(inputFilePath).ToArray();

            int N = DocumentUtils.ParseAndValidateNumber(lines[0]);
            if (N < 6 || N > 19)
            {
                throw new ArgumentException("N should be in diapason from 6 to 19.");
            }

            char[][] board = new char[N][];

            for (int i = 0; i < N; i++)
            {
                board[i] = GoGame.ParseAndValidateBoardLine(lines[i + 1], N);
            }

            int result = GoGame.CountGroupsInAtari(board, N);

            return result.ToString();
        }
    }
}
