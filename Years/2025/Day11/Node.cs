namespace AoC.Y2025.Day11;

public record Node(string Id, string[]? Childs)
{
    public int IdAsInt { get; } =
        Id.Length >= 3
            ? (byte)Id[0] | ((byte)Id[1] << 8) | ((byte)Id[2] << 16)
            : throw new ArgumentException("Id must contain at least 3 characters.");

    public int CountToOut(HashSet<Node> nodes)
    {
        if (Id == "out")
        {
            return 1;
        }

        int paths = 0;
        foreach (string child in Childs ?? [])
        {
            paths += nodes.First(n => n.Id == child).CountToOut(nodes);
        }
        return paths;
    }
}

