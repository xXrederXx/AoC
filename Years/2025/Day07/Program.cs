using System.Numerics;
using AoC.Common;
using ZLinq;

namespace AoC.Y2025.Day07;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 07");

        string[] input = FileHelper.GetLines("data/example.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));

        input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        List<Vector2> spliters = [];
        Vector2 startPos = Vector2.One;

        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == 'S')
                {
                    startPos = new Vector2(j, i);
                }
                if (line[j] == '^')
                {
                    spliters.Add(new Vector2(j, i));
                }
            }
        }

        List<Vector2> activatedSplitters = [];
        float lowerBound = spliters.OrderBy(x => x.Y).Last().Y + 1;
        List<Vector2> beamTipPositions = [];
        beamTipPositions.Add(new Vector2(startPos.X, startPos.Y + 1));
        Vector2[] oldBeamTipPositions = [];
        while (beamTipPositions.Count != 0)
        {
            oldBeamTipPositions = new Vector2[beamTipPositions.Count];
            beamTipPositions.CopyTo(oldBeamTipPositions);
            beamTipPositions = [];

            foreach (Vector2 beamTip in oldBeamTipPositions)
            {
                Vector2 nextPos = new Vector2(beamTip.X, beamTip.Y + 1);
                if (nextPos.Y > lowerBound)
                {
                    // out of bounds
                    continue;
                }

                if (spliters.Any(x => x.Equals(nextPos)))
                {
                    Vector2 newLeft = new(nextPos.X - 1, nextPos.Y);
                    Vector2 newRight = new(nextPos.X + 1, nextPos.Y);

                    if (!beamTipPositions.Any(x => x.Equals(newLeft)))
                    {
                        beamTipPositions.Add(newLeft);
                    }
                    if (!beamTipPositions.Any(x => x.Equals(newRight)))
                    {
                        beamTipPositions.Add(newRight);
                    }

                    activatedSplitters.Add(nextPos);
                    continue;
                }
                // just continue
                beamTipPositions.Add(nextPos);
            }
        }
        return activatedSplitters.Distinct().Count().ToString();
    }

    // Could not figure out yet
    static string Part2(string[] input)
    {
        HashSet<Vector2Int> spliters = [];
        Vector2Int startPos = new Vector2Int(1, 1);

        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == 'S')
                {
                    startPos = new Vector2Int(j, i);
                }
                if (line[j] == '^')
                {
                    spliters.Add(new Vector2Int(j, i));
                }
            }
        }

        ulong finishedBeams = 0;
        int lowerBound = input.Length;

        // Vector 3 here are used the following
        // X, Y = Position, Z = Weight

        Dictionary<Vector2Int, ulong> beamWeights = new();
        Dictionary<Vector2Int, ulong> nextBeamWeights = new();

        beamWeights[startPos] = 1;

        while (beamWeights.Count != 0)
        {
            nextBeamWeights.Clear();

            foreach ((Vector2Int beamTip, ulong beamWeight) in beamWeights)
            {
                Vector2Int nextPos = new Vector2Int(beamTip.X, beamTip.Y + 1);
                if (nextPos.Y > lowerBound)
                {
                    // out of bounds
                    finishedBeams += beamWeight;
                    continue;
                }

                if (spliters.Contains(nextPos))
                {
                    AddWeight(
                        nextBeamWeights,
                        new Vector2Int(nextPos.X + 1, nextPos.Y),
                        beamWeight
                    );
                    AddWeight(
                        nextBeamWeights,
                        new Vector2Int(nextPos.X - 1, nextPos.Y),
                        beamWeight
                    );
                    continue;
                }
                // just continue
                AddWeight(nextBeamWeights, nextPos, beamWeight);
            }
            var temp = beamWeights;
            beamWeights = nextBeamWeights;
            nextBeamWeights = temp;
        }
        return finishedBeams.ToString();
    }

    static void AddWeight(Dictionary<Vector2Int, ulong> dict, Vector2Int pos, ulong weight)
    {
        if (dict.TryGetValue(pos, out ulong existing))
            dict[pos] = existing + weight;
        else
            dict[pos] = weight;
    }
}
