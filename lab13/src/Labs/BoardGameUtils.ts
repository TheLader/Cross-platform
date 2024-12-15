export class BoardGameUtils {
    static getResult(m: number, n: number): number {
        if (!this.isCoordinatesValid(m, n)) {
            throw new Error("Incorrect coordinates");
        }

        const maxSize = Math.max(m, n);
        const dp: boolean[][] = Array.from({ length: maxSize }, () => Array(maxSize).fill(true));

        dp[0][0] = false;

        const indexOfSecondPosition: number[] = [];
        const diagonals: number[] = [];

        const arrayInclude = (arr: number[], val: number): boolean => arr.includes(val);

        for (let i = 1; i < maxSize; i++) {
            for (let j = i + 1; j < maxSize; j++) {
                if (
                    arrayInclude(indexOfSecondPosition, i) ||
                    arrayInclude(indexOfSecondPosition, j) ||
                    arrayInclude(diagonals, Math.abs(i - j))
                ) {
                    dp[i][j] = true;
                } else if (j > 0 && dp[i][j - 1] && i > 0 && dp[i - 1][j] && i > 0 && j > 0 && dp[i - 1][j - 1]) {
                    dp[i][j] = false;
                    dp[j][i] = false;

                    indexOfSecondPosition.push(i);
                    indexOfSecondPosition.push(j);
                    diagonals.push(Math.abs(i - j));
                }
            }
        }

        return dp[m - 1][n - 1] ? 1 : 2;
    }

    static isCoordinatesValid(m: number, n: number): boolean {
        return m > 0 && n > 0 && m < 251 && n < 251;
    }
}
