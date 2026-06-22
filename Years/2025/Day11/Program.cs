using AoC.Common;

namespace AoC.Y2025.Day11;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 11");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1:" + Part1(input));
        System.Console.WriteLine("Part 2:" + Part2(input));
    }

    static string Part1(string[] input)
    {
        return string.Join('\n', input);
    }

    static string Part2(string[] input)
    {
        return string.Join('\n', input);
    }
}

