namespace AoC.Y2025.Day11;

public record Node(string Id, string[]? Childs)
{
    public int IdAsInt { get; } =
        Id.Length >= 3
            ? (byte)Id[0]
              | ((byte)Id[1] << 8)
              | ((byte)Id[2] << 16)
            : throw new ArgumentException("Id must contain at least 3 characters.");
}