export class ProfitUtils {
    static getMaxProfit(prices: number[]): number {
        if (!this.isPriceArrayCorrect(prices)) {
            throw new Error("Некоректний масив цін");
        }

        let sum = 0;
        let totalHair = 0;
        let maxPrice = -1;

        for (let i = prices.length - 1; i >= 0; i--) {
            if (prices[i] >= maxPrice) {
                sum += totalHair * maxPrice;
                maxPrice = prices[i];
                totalHair = 1;
            } else {
                totalHair++;
            }
        }

        sum += totalHair * maxPrice;

        return sum;
    }

    private static isPriceArrayCorrect(prices: number[]): boolean {
        if (!prices || prices.length === 0) {
            return false;
        }

        for (const price of prices) {
            if (price <= 0) {
                return false;
            }
        }

        return true;
    }
}
