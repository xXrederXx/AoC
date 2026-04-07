using AoC.Common;

namespace AoC.Y2025.Day05;

internal class Program
{
    record Range(long Start, long End)
    {
        public bool Includes(long x) => x >= Start && x <= End;

        public bool Includes(Range x) => x.Start >= Start && x.End <= End;
    }

    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 05");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        List<long> ingredientIds = [];
        List<Range> freshRanges = [];

        // Parse input: lines with one number are ingredient IDs, lines with two numbers (start-end) are fresh ranges
        foreach (string line in input)
        {
            string[] splited = line.Split('-');

            if (splited.Length == 2)
            {
                freshRanges.Add(new Range(long.Parse(splited[0]), long.Parse(splited[1])));
            }
            else
            {
                ingredientIds.Add(long.Parse(splited[0]));
            }
        }

        long numFresh = ingredientIds.Count(id => freshRanges.Any(range => range.Includes(id)));

        return numFresh.ToString();
    }

    static string Part2(string[] input)
    {
        List<Range> freshRanges = [];

        foreach (string line in input)
        {
            string[] splited = line.Split('-');

            if (splited.Length == 2)
            {
                freshRanges.Add(new Range(long.Parse(splited[0]), long.Parse(splited[1])));
            }
        }
        freshRanges = freshRanges.OrderBy(x => x.Start).ToList();

        // Merge overlapping ranges iteratively until no more merges are possible
        List<Range> currentRanges = freshRanges;
        Range[] oldRanges = [];
        while (currentRanges.Count != oldRanges.Length)
        {
            oldRanges = new Range[currentRanges.Count];
            currentRanges.CopyTo(oldRanges);
            currentRanges = [];
            for (int i = 0; i < oldRanges.Length; i++)
            {
                int nextIdx = i + 1 >= oldRanges.Length ? i : i + 1;

                if (oldRanges[i].End >= oldRanges[nextIdx].Start)
                {
                    // Ranges overlap, merge them
                    Range newRange = new(oldRanges[i].Start, Math.Max(oldRanges[nextIdx].End, oldRanges[i].End));
                    if (!currentRanges.Any(x => x.Includes(newRange)))
                    {
                        currentRanges.Add(newRange);
                    }
                }
                else if (!currentRanges.Any(x => x.Includes(oldRanges[i])))
                {
                    currentRanges.Add(oldRanges[i]);
                }
            }
        }
        long availableIds = currentRanges.Sum(x => x.End - x.Start + 1);
        return availableIds.ToString();
    }
}
