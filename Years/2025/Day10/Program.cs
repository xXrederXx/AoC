using System.Diagnostics;
using AoC.Common;
using Microsoft.Z3;

namespace AoC.Y2025.Day10;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 10");

        string[] input = FileHelper.GetLines("data/input.txt");

        SolutionVerifier.VerifyAndLog("Part 1:", "452", Part1(input));
        SolutionVerifier.VerifyAndLog("Part 2:", "17424", Part2(input));
    }

    static string Part1(string[] input)
    {
        Machine[] machines = input.Select(line => new Machine(line)).ToArray();

        return machines.Sum(Par1ProcessMachine).ToString();
    }

    static string Part2(string[] input)
    {
        Machine[] machines = input.Select(line => new Machine(line)).ToArray();

        return machines.Select(Par2ProcessMachine).Aggregate((a, b) => a + b).ToString();
    }

    static int Par1ProcessMachine(Machine machine)
    {
        Node<bool> root = new Node<bool>(null, -1, new bool[machine.ToAchive.Length]);

        List<Node<bool>> activeNodes = [root];
        List<Node<bool>> newActiveNodes = [];
        while (activeNodes.Find(node => node.State.SequenceEqual(machine.ToAchive)) is null)
        {
            foreach (Node<bool> active in activeNodes)
            {
                for (int i = 0; i < machine.ButtonsInIndexes.Count; i++)
                {
                    int[] operation = machine.ButtonsInIndexes[i];
                    bool[] state = FlipSwitches(operation, active.State);
                    if (
                        activeNodes.Find(node => node.State.SequenceEqual(state)) is not null
                        || newActiveNodes.Find(node => node.State.SequenceEqual(state)) is not null
                    )
                    {
                        // node already found
                        continue;
                    }
                    newActiveNodes.Add(new Node<bool>(active, i, state));
                }
            }
            activeNodes.Clear();
            activeNodes.AddRange(newActiveNodes);
            newActiveNodes.Clear();
        }
        Node<bool> final = activeNodes.Find(node => node.State.SequenceEqual(machine.ToAchive))!;
        return final.Depth;
    }

    static ulong Par2ProcessMachine(Machine machine)
    {
        // This solution uses Z3

        var ctx = new Context();
        var opt = ctx.MkOptimize();

        // Create variables w0 to wN where N is the last button
        // The variables tell how many times the button is pressed
        var presses = Enumerable
            .Range(0, machine.ButtonsInIndexes.Count)
            .Select(i => ctx.MkIntConst($"w{i}"))
            .ToArray();

        // Telling Z3 that every variable w0 to wN needs to be >= 0
        foreach (var press in presses)
        {
            opt.Add(ctx.MkGe(press, ctx.MkInt(0)));
        }

        for (int i = 0; i < machine.Joltages.Length; i++)
        {
            // Sort for all buttons which affect joltage[i]
            var terms = presses.Where((press, idx) => machine.ButtonsInIndexes[idx].Contains(i));

            // Calculating total of presses as fromula
            ArithExpr total = terms.Any() ? ctx.MkAdd(terms.ToArray()) : ctx.MkInt(0);

            // Telling Z3 that the total presses needs to equal joltages[i]
            opt.Add(ctx.MkEq(total, ctx.MkInt(machine.Joltages[i])));
        }

        // Telling Z3 i need the result where i have the least amount of presses
        opt.MkMinimize(ctx.MkAdd(presses));

        // Check if it is possible
        if (opt.Check() == Status.SATISFIABLE)
        {
            var model = opt.Model;
            // Calculate the sum as ulongs
            return presses
                .Select(p => ((IntNum)model.Evaluate(p)).UInt64)
                .Aggregate((a, c) => a + c);
        }
        return 0;
    }
    static bool[] FlipSwitches(int[] operation, bool[] state)
    {
        bool[] fliped = new bool[state.Length];
        for (int i = 0; i < state.Length; i++)
        {
            if (operation.Contains(i))
            {
                fliped[i] = !state[i];
            }
            else
            {
                fliped[i] = state[i];
            }
        }
        return fliped;
    }
}
