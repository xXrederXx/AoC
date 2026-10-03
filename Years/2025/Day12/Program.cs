using AoC.Common;
using Y2025.Day12;

namespace AoC.Y2025.Day12;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 12");

        string[] input = FileHelper.GetLines("data/example.txt");

        System.Console.WriteLine("Part 1:" + Part1(input));
        System.Console.WriteLine("Part 2:" + Part2(input));
    }

    static Shape[] ReadAllShapes(string[] lines)
    {
        string[] shapeLines = lines.TakeWhile(line => !line.Contains('x')).ToArray();
        var shape = Shape.ReadShape(shapeLines);
        Console.WriteLine(shape);
        return [shape];
    }

    static string Part1(string[] input)
    {
        ReadAllShapes(input);
        return string.Join('\n', input);
    }

    static string Part2(string[] input)
    {
        return string.Join('\n', input);
    }
}
