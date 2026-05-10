using System.Collections;
using AoC.Common;

namespace AoC.Y2025.Day08;

public class Circuit : IEquatable<Circuit>
{
    private readonly List<Connection> connections = [];
    public ulong NumJBoxes =>
        (ulong)connections.SelectMany(con => new Vector3Int[] { con.Left, con.Right }).Distinct().LongCount();

    public Circuit() { }

    public Circuit(List<Connection> connections)
    {
        this.connections = connections;
    }

    public bool Contains(Vector3Int jBox)
    {
        return connections.Any(x => x.Left == jBox || x.Right == jBox);
    }

    public enum MergeType
    {
        CREATE,
        ADD,
        MERGE_SAME,
        MERGE_DIFFERENT,
    }

    public static (Circuit, MergeType) MergeCircuits(
        Circuit? left,
        Circuit? right,
        Connection connection
    )
    {
        if (left is null && right is null)
        {
            return (new Circuit([connection]), MergeType.CREATE);
        }
        if (left is null && right is not null)
        {
            right.connections.Add(connection);
            return (right, MergeType.ADD);
        }
        if (left is not null && right is null)
        {
            left.connections.Add(connection);
            return (left, MergeType.ADD);
        }

        if (left == right)
        {
            left!.connections.Add(connection);
            return (left, MergeType.MERGE_SAME);
        }

        left!.connections.Add(connection);
        left.connections.AddRange(right!.connections);
        return (left, MergeType.MERGE_DIFFERENT);
    }

    public override bool Equals(object? obj)
    {
        return obj is Circuit circuit && Equals(circuit);
    }

    public bool Equals(Circuit? other)
    {
        return other is not null && connections.SequenceEqual(other.connections);
    }

    public override int GetHashCode()
    {
        return ((IStructuralEquatable)connections).GetHashCode(
            EqualityComparer<Vector3Int>.Default
        );
    }

    public override string ToString()
    {
        return $"Len({NumJBoxes}) " + string.Join(", ", connections);
    }
}
