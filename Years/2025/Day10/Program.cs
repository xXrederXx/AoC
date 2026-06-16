using System.Diagnostics;
using AoC.Common;

namespace AoC.Y2025.Day10;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 10");

        string[] input = FileHelper.GetLines("data/input.txt");

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
        System.Console.WriteLine($"Process machine: {machine}");
        Node<int> root = new Node<int>(null, -1, new int[machine.ToAchive.Length]);

        List<Node<int>> activeNodes = [root];
        List<Node<int>> newActiveNodes = [];
        while (activeNodes.Find(node => node.State.SequenceEqual(machine.Joltages)) is null)
        {
            int discard = 0;
            foreach (Node<int> active in activeNodes)
            {
                for (int i = 0; i < machine.ButtonsInIndexes.Count; i++)
                {
                    int[] operation = machine.ButtonsInIndexes[i];
                    int[] state = UpdateState(operation, active.State);
                    if (
                        activeNodes.Find(node => node.State.SequenceEqual(state)) is not null
                        || newActiveNodes.Find(node => node.State.SequenceEqual(state)) is not null
                    )
                    {
                        // node already found

                        continue;
                    }
                    if (state.Where((joltage, idx) => joltage > machine.Joltages[idx]).Any())
                    {
                        // at least one joltage is too high so this path can be discarded
                        continue;
                    }
                    newActiveNodes.Add(new Node<int>(active, i, state));
                }
            }
            activeNodes.Clear();
            activeNodes.AddRange(newActiveNodes);
            newActiveNodes.Clear();
            System.Console.WriteLine($"Nodes: {activeNodes.Count} {discard}");
        }
        Node<int> final = activeNodes.Find(node => node.State.SequenceEqual(machine.Joltages))!;
        return final.Depth;
    }

    private static int[] UpdateState(int[] operation, int[] state)
    {
        int[] updated = new int[state.Length];
        for (int i = 0; i < state.Length; i++)
        {
            if (operation.Contains(i))
            {
                updated[i] = state[i] + 1;
            }
            else
            {
                updated[i] = state[i];
            }
        }
        return updated;
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
