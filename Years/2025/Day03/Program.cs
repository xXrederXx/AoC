using AoC.Common;

namespace AoC.Y2025.Day03;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 03");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        int sum = 0;
        foreach (string battery in input)
        {
            int[] banks = battery.Select(charToInt).ToArray();
            int firstIdx = findBiggestInRange(banks, 0, banks.Length - 1);
            int secondIdx = findBiggestInRange(banks, firstIdx + 1, banks.Length);
            sum += banks[firstIdx] * 10 + banks[secondIdx];
        }
        return sum.ToString();
    }

    static string Part2(string[] input)
    {
        const int takes = 12;
        long sum = 0;
        foreach (string battery in input)
        {
            int[] banks = battery.Select(charToInt).ToArray();
            int[] indexes = new int[takes];
            for (int i = 0; i < takes; i++)
            {
                int startIdx = i == 0 ? 0 : indexes[i - 1] + 1;
                indexes[i] = findBiggestInRange(
                    banks,
                    startIdx,
                    banks.Length - startIdx - takes + i + 1
                );
            }
            long active = indexes
                .Select((idx, num) => banks[idx] * (long)Math.Pow(10, takes - 1 - num))
                .Sum();
            sum += active;
        }
        return sum.ToString();
    }

    private static int charToInt(char chr)
    {
        return chr switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            _ => throw new ArgumentException("Must be number 0-9", nameof(chr)),
        };
    }

    private static int findBiggestInRange(int[] arr, int start, int range)
    {
        int biggest = int.MinValue;
        int idx = 0;
        for (int i = start; i < Math.Min(start + range, arr.Length); i++)
        {
            if (arr[i] > biggest)
            {
                biggest = arr[i];
                idx = i;
            }
        }
        return idx;
    }
}
