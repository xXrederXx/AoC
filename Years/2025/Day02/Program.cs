using AoC.Common;

namespace AoC.Y2025.Day02;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 02");

        string[][] input = FileHelper
            .GetLines("data/input.txt")
            .First()
            .Split(',')
            .Select(x => x.Split('-'))
            .ToArray();

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    // This works using the following formula:
    // An invalid ID follows this scheme just looking at "the first part"
    // 0 < x < 10 => x + 10x, 10 < x < 100 => x + 100x, etc
    // so 55 is invalid cause if x = 5 we get 11x = 55
    static string Part1(string[][] input)
    {
        long sum = 0;
        foreach (string[] pair in input)
        {
            long start = long.Parse(pair[0]);
            long end = long.Parse(pair[1]);
            for (long i = start; i <= end; i++)
            {
                int numDigits = (int)Math.Floor(Math.Log10(i) + 1); // Calculates total digits using floor(log10(x) + 1)
                if (numDigits % 2 == 1) // digits cant repeat if uneven
                {
                    continue;
                }
                // x / (10^(digits / 2) + 1)
                double divided = i / (Math.Pow(10, numDigits / 2.0) + 1);

                // check if it is whole number
                if (Math.Abs(Math.Round(divided) - divided) < 1e-6)
                {
                    sum += i;
                }
            }
        }
        return sum.ToString();
    }

    static string Part2(string[][] input)
    {
        List<long> numbers = new List<long>();
        foreach (string[] pair in input)
        {
            long start = long.Parse(pair[0]);
            long end = long.Parse(pair[1]);
            for (long testId = start; testId <= end; testId++)
            {
                // Number of digits the test id has (Calculated: floor(log10(testId) + 1))
                int numDigits = (int)Math.Floor(Math.Log10(testId) + 1); 

                // Iterates over different lengths of possibly repeating digits
                for (int testLength = 1; testLength <= Math.Ceiling(numDigits / 2.0); testLength++)
                {
                    // digits cant repeat if not evenly splittable
                    if (numDigits % testLength != 0) 
                    {
                        continue;
                    }

                    // This is used to check if a number could be repeated
                    double factor = 1;

                    // Testing iteration for each repetition count
                    for (int j = 1; j <= numDigits / testLength; j++)
                    {
                        factor += Math.Pow(10, j * testLength);

                        // Check if dividing by the repetition factor gives a whole number within range
                        double divided = testId / factor;

                        // check if it is whole number and if the divided is in the right range
                        if (
                            Math.Abs(Math.Round(divided) - divided) < 1e-12
                            && divided < Math.Pow(10, testLength)
                        )
                        {
                            numbers.Add(testId);
                            break;
                        }
                    }
                }
            }
        }
        return numbers.Distinct().Sum().ToString();
    }
}
