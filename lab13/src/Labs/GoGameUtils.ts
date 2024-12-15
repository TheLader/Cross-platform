export class GoGameUtils {
    static parseAndValidateBoardLine(input: string, expectedLength: number): string[] {
        if (!input || input.trim() === "") {
            throw new Error("The board line is empty or contains only spaces.");
        }

        if (input.length !== expectedLength) {
            throw new Error(`The board line must have exactly ${expectedLength} characters.`);
        }

        for (const c of input) {
            if (c !== "B" && c !== "W" && c !== ".") {
                throw new Error("The board line contains invalid characters. Only 'B', 'W', or '.' are allowed.");
            }
        }

        return input.split("");
    }

    static countGroupsInAtari(board: string[][], N: number): number {
        const visited: boolean[][] = Array.from({ length: N }, () => Array(N).fill(false));
        let groupsInAtari = 0;

        for (let i = 0; i < N; i++) {
            for (let j = 0; j < N; j++) {
                if (board[i][j] === "B" && !visited[i][j]) {
                    const { dame } = this.dfs(board, visited, N, i, j);
                    if (dame.size === 1) {
                        groupsInAtari++;
                    }
                }
            }
        }

        return groupsInAtari;
    }

    private static dfs(
        board: string[][],
        visited: boolean[][],
        N: number,
        x: number,
        y: number,
    ): { group: Array<[number, number]>; dame: Set<[number, number]> } {
        const dx = [-1, 1, 0, 0];
        const dy = [0, 0, -1, 1];

        const stack: Array<[number, number]> = [];
        stack.push([x, y]);
        visited[x][y] = true;

        const group: Array<[number, number]> = [[x, y]];
        const dame: Set<[number, number]> = new Set();

        while (stack.length > 0) {
            const [cx, cy] = stack.pop()!;

            for (let i = 0; i < 4; i++) {
                const nx = cx + dx[i];
                const ny = cy + dy[i];

                if (nx >= 0 && nx < N && ny >= 0 && ny < N) {
                    if (board[nx][ny] === "B" && !visited[nx][ny]) {
                        visited[nx][ny] = true;
                        stack.push([nx, ny]);
                        group.push([nx, ny]);
                    } else if (board[nx][ny] === ".") {
                        dame.add([nx, ny]);
                    }
                }
            }
        }

        return { group, dame };
    }
}
