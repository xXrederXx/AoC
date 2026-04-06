using AoC.Common;

namespace AoC.${PROJECT};

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code ${YEAR} - Day ${DAY_PADDED}");

        string[] input = FileHelper.GetLines("data/input.txt");
        
        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
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
