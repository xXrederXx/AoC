using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AoC.Common;

public readonly struct Vector2Int : IEquatable<Vector2Int>
{
    public readonly int X;
    public readonly int Y;

    public static readonly Vector2Int One = new Vector2Int(1, 1);
    public static readonly Vector2Int Up = new Vector2Int(0, 1);
    public static readonly Vector2Int Right = new Vector2Int(1, 0);
    public static readonly Vector2Int Down = new Vector2Int(0, -1);
    public static readonly Vector2Int Left = new Vector2Int(-1, 0);
    public static readonly Vector2Int Zero = new Vector2Int(0, 0);
    
    public Vector2Int(int x, int y)
    {
        (X, Y) = (x, y);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector2Int vec && Equals(vec);
    }

    public bool Equals(Vector2Int other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public static bool operator ==(Vector2Int left, Vector2Int right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Vector2Int left, Vector2Int right)
    {
        return !(left == right);
    }

    public static Vector2Int operator +(Vector2Int left, Vector2Int right) => new Vector2Int(left.X + right.X, left.Y + right.Y);
    public static Vector2Int operator -(Vector2Int left, Vector2Int right) => new Vector2Int(left.X - right.X, left.Y - right.Y);
    public static Vector2Int operator *(Vector2Int left, Vector2Int right) => new Vector2Int(left.X * right.X, left.Y * right.Y);
}
