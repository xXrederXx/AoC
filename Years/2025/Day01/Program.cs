using AoC.Common;

namespace AoC.Y2025.Day01;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 01");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        int[] values = input
            .Select(x => (x[0], int.Parse(x.Substring(1))))
            .Select(x => x.Item1 == 'L' ? -x.Item2 : x.Item2)
            .ToArray();
        int timesAtZero = 0;
        int current = 50;

        foreach (int tick in values)
        {
            current += tick;
            current %= 100;

            if (current == 0)
            {
                timesAtZero++;
            }
        }

        return timesAtZero.ToString();
    }

    static string Part2(string[] input)
    {
        int[] values = input
            .Select(x => (x[0], int.Parse(x.Substring(1))))
            .Select(x => x.Item1 == 'L' ? -x.Item2 : x.Item2)
            .ToArray();
        int timesAtZero = 0;
        int current = 50;

        foreach (int tick in values)
        {
            System.Console.Write($"Current: {current}\tTick: {tick}");
            // Count full cycles (each 100 units wraps around)
            timesAtZero += Math.Abs(tick / 100);
            int start = current;
            current = (current + (tick % 100) + 100) % 100;

            // Check if crossed zero during the move
            if (tick > 0 && start > current && start != 0)
            {
                timesAtZero++;
            }
            else if (tick < 0 && start < current && start != 0)
            {
                timesAtZero++;
            }
            else if (current == 0)
            {
                timesAtZero++;
            }
            System.Console.WriteLine($"\tTimes @ 0: {timesAtZero}");
        }

        return timesAtZero.ToString();
    }
}
