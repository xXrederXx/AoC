using AoC.Common;

namespace AoC.Y2025.Day04;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 04");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        bool[][] grid = input.Select(x => x.Select(y => y == '@').ToArray()).ToArray();
        int sum = 0;

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                if (!grid[y][x])
                {
                    continue;
                }

                bool hasLessThan4Neighbours = getNumNeighbours(grid, x, y) < 4;
                sum += hasLessThan4Neighbours ? 1 : 0;
            }
        }

        return sum.ToString();
    }

    static string Part2(string[] input)
    {
        bool[][] grid = input.Select(x => x.Select(y => y == '@').ToArray()).ToArray();
        int sum = 0;
        int oldSum = -1;
        while (sum != oldSum)
        {
            oldSum = sum;
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (!grid[y][x])
                    {
                        continue;
                    }

                    bool hasLessThan4Neighbours = getNumNeighbours(grid, x, y) < 4;
                    grid[y][x] = !hasLessThan4Neighbours;
                    sum += hasLessThan4Neighbours ? 1 : 0;
                }
            }
        }

        return sum.ToString();
    }

    private static int getNumNeighbours(bool[][] grid, int x, int y)
    {
        int sum = 0;
        for (int j = -1; j <= 1; j++)
        {
            int checkY = y + j;
            if (checkY < 0 || checkY >= grid.GetLength(0))
            {
                continue;
            }
            for (int i = -1; i <= 1; i++)
            {
                int checkX = x + i;
                if (checkX < 0 || checkX >= grid[checkY].Length)
                {
                    continue;
                }

                if (grid[checkY][checkX] && !(j == 0 & i == 0))
                {
                    sum++;
                }
            }
        }
        return sum;
    }
}
