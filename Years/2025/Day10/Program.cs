using System.Diagnostics;
using AoC.Common;

namespace AoC.Y2025.Day10;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 10");

        string[] input = FileHelper.GetLines("data/example.txt");

        SolutionVerifier.VerifyAndLog("Part 1:", "452", Part1(input));
        System.Console.WriteLine("Part 2:" + Part2(input));
    }

    static string Part1(string[] input)
    {
        Machine[] machines = input.Select(line => new Machine(line)).ToArray();

        return machines.Sum(Par1ProcessMachine).ToString();
    }

    static string Part2(string[] input)
    {
        Machine[] machines = input.Select(line => new Machine(line)).ToArray();

        return machines.Sum(Par2ProcessMachine).ToString();
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

    static int Par2ProcessMachine(Machine machine)
    {
        // Generates my green rows
        List<int[]> buttonVectors = new List<int[]>(machine.ButtonsInIndexes.Count);
        foreach (int[] buttonIdxs in machine.ButtonsInIndexes)
        {
            int[] buttonVector = new int[machine.Joltages.Length];
            foreach (int idx in buttonIdxs)
            {
                buttonVector[idx] = 1;
            }
        }

        int[] weights = new int[buttonVectors.Count];
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = buttonVectors[i]
                .Select((value, idx) => value * machine.Joltages[idx]) // Multiply Joltages and ButtonVector
                .Where(x => x > 0) // Filter out 0s
                .Min(); // Get the smallest value
        }

        while(true)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                int startWeight = weights[i];

                for (int currentWeight = 0; currentWeight >= 0; currentWeight--)
                {
                    weights[i] = currentWeight;
                    if(IsValidWeights(weights, buttonVectors, machine.Joltages))
                    {
                        return weights.Sum();
                    }
                }
            }
        }

        return 0;
    }

    static bool IsValidWeights(int[] weights, List<int[]> buttonVectors, int[] joltages)
    {
        List<int[]> buttonVectorsMultiplied = buttonVectors
            .Select((vec, idx) => vec.Select(value => value * weights[idx]).ToArray())
            .ToList();

        for (int i = 0; i < joltages.Length; i++)
        {
            int currentJoltage = buttonVectors.Sum(vec => vec[i]);
            if(currentJoltage != joltages[i])
            {
                return false;
            }
        }
        return true;
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
