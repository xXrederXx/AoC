using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Y2025.Day12;

public record Shape(int Id, string[] ShapeAsLines, bool[][] ShapeAsGrid)
{
    public static Shape ReadShape(string[] lines)
    {
        Regex idExtractor = new Regex(@"^(\d+):$");
        var idMatch = idExtractor.Match(lines[0]);
        if (!int.TryParse(idMatch.Groups[1].Value, out int id))
        {
            throw new ArgumentException("Could not match id");
        }

        string[] shapeLines = lines
            .Skip(1)
            .TakeWhile(l => l.StartsWith('.') || l.StartsWith('#'))
            .ToArray();
        bool[][] shapeGrid = shapeLines
            .Select(line => line.Select(c => c == '#').ToArray())
            .ToArray();
        return new Shape(id, shapeLines, shapeGrid);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Id: {Id}");
        foreach (string line in ShapeAsLines)
        {
            sb.AppendLine(line);
        }
        foreach (bool[] row in ShapeAsGrid)
        {
            sb.AppendLine(string.Join("", row.Select(x => x ? "#" : ".")));
        }
        return sb.ToString();
    }
}
