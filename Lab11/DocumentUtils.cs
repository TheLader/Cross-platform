using System;
using System.Linq;

namespace Lab11
{
    public static class DocumentUtils
    {
        public static int ParseAndValidateNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int number) || number <= 0)
            {
                throw new ArgumentException("Invalid format of the first line. An integer greater than zero is expected.");
            }

            return number;
        }

        public static int[] ParseAndValidateLine(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("The line is empty or contains only spaces.");
            }

            return input.Split(' ')
                        .Select(part =>
                        {
                            if (!int.TryParse(part, out int number))
                            {
                                throw new ArgumentException("The line contains non-numeric characters.");
                            }
                            return number;
                        })
                        .ToArray();
        }
    }
}
