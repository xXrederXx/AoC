using AoC.Common;

namespace AoC.Y2025.Day11;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 11");

        string[] input = FileHelper.GetLines("data/example.txt");

        System.Console.WriteLine("Part 1:" + Part1(input));
        System.Console.WriteLine("Part 2:" + Part2(input));
    }

    static string Part1(string[] input)
    {
        HashSet<Node> nodes = input
            .Select(line => line.Split(':').SelectMany(x => x.Split(' ').Select(x => x.Trim().Trim(':')).Where(x => !x.IsWhiteSpace())))
            .Select(ids => new Node(ids.First(), ids.Skip(1).ToArray())).ToHashSet();
        nodes.Add(new Node("out", []));


        int paths = GfG.CountPaths(nodes);
        return paths.ToString();
    }

    static string Part2(string[] input)
    {
        return string.Join('\n', input);
    }
}

