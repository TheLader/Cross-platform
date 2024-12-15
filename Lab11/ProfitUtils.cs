using System;

namespace Lab11
{
    public static class ProfitUtils
    {
        public static int GetMaxProfit(int[] prices)
        {
            if (!IsPriceArrayCorrect(prices))
            {
                throw new ArithmeticException();
            }

            int sum = 0;
            int totalHair = 0;
            int maxPrice = -1;

            for (int i = prices.Length - 1; i >= 0; i--)
            {
                if (prices[i] >= maxPrice)
                {
                    sum += totalHair * maxPrice;
                    maxPrice = prices[i];
                    totalHair = 1;
                }
                else
                {
                    totalHair++;
                }
            }

            sum += totalHair * maxPrice;

            return sum;
        }

        private static bool IsPriceArrayCorrect(int[] prices)
        {
            if (prices == null)
            {
                return false;
            }

            foreach (int price in prices)
            {
                if (price <= 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
