namespace AoC.Y2025.Day11;

public class Node(string Id, string[]? Childs) : IEquatable<Node>
{

    public int IdAsInt { get; } =
        Id.Length >= 3
            ? (byte)Id[0] | ((byte)Id[1] << 8) | ((byte)Id[2] << 16)
            : throw new ArgumentException("Id must contain at least 3 characters.");
    public string Id { get; } = Id;
    public string[]? Childs { get; } = Childs;

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

    public int CountTo(HashSet<Node> nodes, string id)
    {
        if (Id == id)
        {
            return 1;
        }

        int paths = 0;
        foreach (string child in Childs ?? [])
        {
            if (nodes.TryGetValue(new Node(child, []), out Node? node))
            {
                paths += node.CountTo(nodes, id);
            }
        }
        return paths;
    }

    internal List<HashSet<int>> CountToOutPaths(HashSet<Node> nodes)
    {
        if (Id == "out")
        {
            return new List<HashSet<int>>() { new HashSet<int>() };
        }
        List<HashSet<int>> paths = new(Childs?.Length ?? 1);

        foreach (string child in Childs ?? [])
        {
            paths.AddRange(nodes.First(n => n.Id == child).CountToOutPaths(nodes));
        }
        foreach (HashSet<int> path in paths)
        {
            if (path.Contains(IdAsInt))
            {
                Console.WriteLine("Duplicate");
            }
            path.Add(IdAsInt);
        }
        return paths;
    }

    public bool Equals(Node? other)
    {
        return other is not null && this.Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Node node && Equals(node);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
