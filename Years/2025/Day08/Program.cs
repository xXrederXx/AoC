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
        List<Circuit> circuits = InputToCircuits(input);

        for (int i = 0; i < 10; i++)
        {
            MergeOneBox(circuits);
        }

        return string.Join('\n', circuits);
    }

    static void MergeOneBox(List<Circuit> circuits)
    {
        Circuit? minLeft = null,
            minRight = null;
        double minDist = double.PositiveInfinity;

        for (int i = 0; i < circuits.Count; i++)
        {
            Circuit left = circuits[i];
            for (int j = i + 1; j < circuits.Count; j++)
            {
                Circuit right = circuits[j];
                double dist = left.CalcNearestDistance(right);
                if (dist < minDist)
                {
                    (minLeft, minRight) = (left, right);
                    minDist = dist;
                }
            }
        }

        if (minLeft is null || minRight is null)
        {
            throw new Exception("they should not be able to be null");
        }

        minLeft.Merge(minRight);
        circuits.Remove(minRight);
    }

    static string Part2(string[] input)
    {
        return string.Join('\n', input);
    }

    static List<Circuit> InputToCircuits(string[] lines) =>
        lines
            .AsValueEnumerable()
            .Select(line => line.Split(',').Select(part => int.Parse(part)).ToArray())
            .Select(values => new Vector3Int(values[0], values[1], values[2]))
            .Select(vec => new Circuit(vec))
            .ToList();
}
