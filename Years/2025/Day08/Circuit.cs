using AoC.Common;

namespace AoC.Y2025.Day08;

public class Circuit
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
        Array.Copy(other.JunctionBoxes, 0,  newCircuit, JunctionBoxes.Length, other.JunctionBoxes.Length);
        JunctionBoxes = newCircuit;
        return this;
    }
}