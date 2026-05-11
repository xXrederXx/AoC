using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

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

    public static double Distance(Vector3Int left, Vector3Int right)
    {
        return Math.Sqrt(DistancePow(left, right));
    }

    public static double DistancePow(Vector3Int left, Vector3Int right)
    {
        double dX = left.X - right.X;
        double dY = left.Y - right.Y;
        double dZ = left.Z - right.Z;

        return dX * dX + dY * dY + dZ * dZ;
    }


    public static Vector3Int operator +(Vector3Int left, Vector3Int right) => new Vector3Int(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    public static Vector3Int operator -(Vector3Int left, Vector3Int right) => new Vector3Int(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    public static Vector3Int operator *(Vector3Int left, Vector3Int right) => new Vector3Int(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
}
