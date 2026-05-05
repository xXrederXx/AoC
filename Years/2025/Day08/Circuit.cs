using System.Collections;
using AoC.Common;

namespace AoC.Y2025.Day08;

public class Circuit : IEquatable<Circuit>
{
    public Vector3Int[] JunctionBoxes { get; private set; }
    public int NumberBoxes => JunctionBoxes.Length;

    public Circuit(Vector3Int junctionBox)
    {
        JunctionBoxes = [junctionBox];
    }

    public Circuit Merge(Circuit other)
    {
        Vector3Int[] newCircuit = new Vector3Int[JunctionBoxes.Length + other.NumberBoxes];
        Array.Copy(JunctionBoxes, 0, newCircuit, 0, JunctionBoxes.Length);
        Array.Copy(
            other.JunctionBoxes,
            0,
            newCircuit,
            JunctionBoxes.Length,
            other.JunctionBoxes.Length
        );
        JunctionBoxes = newCircuit;
        return this;
    }

    public double CalcNearestDistance(Circuit other)
    {
        double minDist = double.PositiveInfinity;
        foreach (Vector3Int junctionBoxOther in other.JunctionBoxes)
        {
            foreach (Vector3Int junctionBoxThis in JunctionBoxes)
            {
                double dist = Vector3Int.DistancePow(junctionBoxThis, junctionBoxOther);
                minDist = dist < minDist ? dist : minDist;
            }
        }
        return minDist;
    }

    public bool Equals(Circuit? other)
    {
        return other is not null && JunctionBoxes.SequenceEqual(other.JunctionBoxes);
    }

    public override bool Equals(object? obj)
    {
        return obj is Circuit circuit && Equals(circuit);
    }

    public override int GetHashCode()
    {
        return ((IStructuralEquatable)JunctionBoxes).GetHashCode(
            EqualityComparer<Vector3Int>.Default
        );
    }

    public override string ToString()
    {
        return string.Join(",", JunctionBoxes);
    }
}
