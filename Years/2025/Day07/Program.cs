using System.Numerics;
using System.Runtime.InteropServices;
using AoC.Common;

namespace AoC.Y2025.Day07;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 07");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        //System.Console.WriteLine("Part 2: " + Part2(input));
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

    static string Part2(string[] input)
    {
        return string.Join('\n', input);
    }
}
