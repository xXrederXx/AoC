using AoC.Common;
using ZLinq;

namespace AoC.Y2025.Day08;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 08");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        ReadOnlySpan<Vector3Int> jBoxes = InputToJBoxes(input);
        ReadOnlySpan<Connection> possibleConnections = CalculateAllPossibleConnectionsOrdered(
            jBoxes
        );
        List<Circuit> circuits = [];

        for (int i = 0; i < 1000; i++)
        {
            Connection connection = possibleConnections[i];

            Circuit? left = circuits.FirstOrDefault(circ => circ.Contains(connection.Left));
            Circuit? right = circuits.FirstOrDefault(circ => circ.Contains(connection.Right));

            (Circuit circuit, Circuit.MergeType mergeType) = Circuit.MergeCircuits(
                left,
                right,
                connection
            );

            if (mergeType == Circuit.MergeType.CREATE)
            {
                circuits.Add(circuit);
            }
            if (mergeType == Circuit.MergeType.ADD)
            {
                // no action requiered
            }
            if (mergeType == Circuit.MergeType.MERGE_SAME)
            {
                // no action requiered
            }
            if (mergeType == Circuit.MergeType.MERGE_DIFFERENT)
            {
                circuits.Remove(right!);
            }
        }

        return circuits
            .Select(circ => circ.NumJBoxes)
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate((prod, next) => prod * next)
            .ToString();
    }

    static string Part2(string[] input)
    {
        ReadOnlySpan<Vector3Int> jBoxes = InputToJBoxes(input);
        ReadOnlySpan<Connection> possibleConnections = CalculateAllPossibleConnectionsOrdered(
            jBoxes
        );
        List<Circuit> circuits = [];

        for (int i = 0; i < possibleConnections.Length; i++)
        {
            Connection connection = possibleConnections[i];

            Circuit? left = circuits.FirstOrDefault(circ => circ.Contains(connection.Left));
            Circuit? right = circuits.FirstOrDefault(circ => circ.Contains(connection.Right));

            (Circuit circuit, Circuit.MergeType mergeType) = Circuit.MergeCircuits(
                left,
                right,
                connection
            );

            if (mergeType == Circuit.MergeType.CREATE)
            {
                circuits.Add(circuit);
            }
            if (mergeType == Circuit.MergeType.ADD)
            {
                // no action requiered
            }
            if (mergeType == Circuit.MergeType.MERGE_SAME)
            {
                // no action requiered
            }
            if (mergeType == Circuit.MergeType.MERGE_DIFFERENT)
            {
                circuits.Remove(right!);
            }

            if (AllConnectedToOne(circuits, jBoxes))
            {
                return (connection.Left.X * connection.Right.X).ToString();
            }
        }

        return "N/A";
    }

    static ReadOnlySpan<Vector3Int> InputToJBoxes(string[] lines) =>
        lines
            .AsValueEnumerable()
            .Select(line => line.Split(',').Select(part => int.Parse(part)).ToArray())
            .Select(values => new Vector3Int(values[0], values[1], values[2]))
            .ToArray();

    static ReadOnlySpan<Connection> CalculateAllPossibleConnectionsOrdered(
        ReadOnlySpan<Vector3Int> jBoxes
    )
    {
        int n = jBoxes.Length - 1;
        int numConnections = (n * (n + 1)) / 2; // sum up all integers up to n

        Connection[] connections = new Connection[numConnections];
        int currentIdx = 0;
        for (int i = 0; i < jBoxes.Length; i++)
        {
            Vector3Int currentLeft = jBoxes[i];
            for (int j = 1 + i; j < jBoxes.Length; j++)
            {
                Vector3Int currentRight = jBoxes[j];
                connections[currentIdx] = new Connection(
                    currentLeft,
                    currentRight,
                    Vector3Int.DistancePow(currentLeft, currentRight)
                );
                currentIdx++;
            }
        }
        return connections.AsValueEnumerable().OrderBy(x => x.Distance).ToArray();
    }

    static bool AllConnectedToOne(List<Circuit> circuits, ReadOnlySpan<Vector3Int> jBoxes)
    {
        if (circuits.Count != 1)
        {
            return false;
        }
        return circuits[0].NumJBoxes == (ulong)jBoxes.Length;
    }
}
