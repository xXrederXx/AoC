using AoC.Common;

namespace AoC.Y2025.Day06;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 06");

        string[] input = FileHelper.GetLines(
            "data/input.txt",
            StringSplitOptions.RemoveEmptyEntries
        );

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        IEnumerable<IEnumerable<string>> seperated = input.Select(x =>
            x.Split(' ').Where(num => !num.IsWhiteSpace())
        );
        char[] operations = seperated.Last().Select(x => x[0]).ToArray();
        var numbers = seperated
            .SkipLast(1)
            .Select(row => row.Select(int.Parse).ToArray())
            .ToArray();

        long sum = 0;
        for (int i = 0; i < operations.Length; i++)
        {
            char op = operations[i];
            long result = op == '*' ? 1 : 0;
            for (int j = 0; j < numbers.Count(); j++)
            {
                result = op == '*' ? result * numbers[j][i] : result + numbers[j][i];
            }
            sum += result;
        }

        return sum.ToString();
    }

    static string Part2(string[] input)
    {
        var numberLines = input.SkipLast(1);

        var operationLines = input.Last();
        var opearations = operationLines.Split(" ").Where(c => !c.IsWhiteSpace()).Select(c => c[0]);

        List<List<long>> numberGroups = [];
        int groupIdx = 0;
        for (int columnIdx = 0; columnIdx < numberLines.First().Length; columnIdx++)
        {
            long number = 0;
            int numberPlace = 0;
            for (int rowIdx = numberLines.Count() - 1; rowIdx >= 0; rowIdx--)
            {
                if (
                    numberLines.Count() <= rowIdx
                    || numberLines.ElementAt(rowIdx).Length <= columnIdx
                )
                {
                    continue;
                }
                int asInt = CharHelper.TryParseDigit(numberLines.ElementAt(rowIdx)[columnIdx]);
                if (asInt == -1)
                {
                    continue;
                }
                number += asInt * (long)Math.Pow(10, numberPlace);
                numberPlace++;
            }
            if (number == 0)
            {
                groupIdx++;
                continue;
            }
            while (numberGroups.Count <= groupIdx)
            {
                numberGroups.Add(new List<long>());
            }

            numberGroups.ElementAt(groupIdx).Add(number);
        }

        long sum = 0;
        for (int i = 0; i < opearations.Count(); i++)
        {
            char op = opearations.ElementAt(i);
            var numbers = numberGroups[i];
            long result = op == '*' ? 1 : 0;
            for (int j = 0; j < numbers.Count(); j++)
            {
                result = op == '*' ? result * numbers[j] : result + numbers[j];
            }
            sum += result;
        }

        return sum.ToString();
    }
}
