using AoC.Common;

namespace AoC.Y2025.Day03;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 03");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input)); // 17092
        System.Console.WriteLine("Part 2: " + Part2(input)); // 170147128753455
    }

    static string Part1(string[] input)
    {
        int sum = 0;
        foreach (string battery in input)
        {
            int[] banks = battery.Select(CharHelper.ParseDigit).ToArray();
            // Find the largest digit
            int firstIdx = findBiggestInRange(banks, 0, banks.Length - 1);
            // Find the next largest after the first
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
            int[] banks = battery.Select(CharHelper.ParseDigit).ToArray();
            int[] indexes = new int[takes];
            // Selectively find the largest digits, ensuring enough remain for later picks
            for (int i = 0; i < takes; i++)
            {
                int startIdx = i == 0 ? 0 : indexes[i - 1] + 1;
                indexes[i] = findBiggestInRange(
                    banks,
                    startIdx,
                    banks.Length - startIdx - takes + i + 1
                );
            }
            // Form a number from the selected digits in order
            long active = indexes
                .Select((idx, num) => banks[idx] * (long)Math.Pow(10, takes - 1 - num))
                .Sum();
            sum += active;
        }
        return sum.ToString();
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
