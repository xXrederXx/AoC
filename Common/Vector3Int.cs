using System.Diagnostics.CodeAnalysis;

namespace AoC.Common;

public readonly struct Vector3Int : IEquatable<Vector3Int>
{
    public readonly int X;
    public readonly int Y;
    public readonly int Z;

    public Vector3Int(int x, int y, int z)
    {
        (X, Y, Z) = (x, y, z);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector3Int vec && Equals(vec);
    }

    public bool Equals(Vector3Int other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public static bool operator ==(Vector3Int left, Vector3Int right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Vector3Int left, Vector3Int right)
    {
        return !(left == right);
    }
}
