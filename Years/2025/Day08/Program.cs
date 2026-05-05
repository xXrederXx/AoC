using AoC.Common;
using ZLinq;

namespace AoC.Y2025.Day08;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 08");

        string[] input = FileHelper.GetLines("data/example.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        return string.Join('\n', InputToVectors(input));
    }

    static string Part2(string[] input)
    {
        return string.Join('\n', input);
    }

    static Vector3Int[] InputToVectors(string[] lines) =>
        lines.AsValueEnumerable()
            .Select(line => line.Split(',')
                .Select(part => int.Parse(part))
                .ToArray())
            .Select(values => new Vector3Int(values[0], values[1], values[2]))
            .ToArray()
;
}

