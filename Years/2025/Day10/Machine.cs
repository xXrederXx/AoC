namespace AoC.Y2025.Day10;

record Machine(bool[] ToAchive, List<int[]> ButtonsInIndexes, int[] Joltages)
{
    public Machine(string input)
        : this(
            input
                .Split(' ')
                .First()
                .TrimStart('[')
                .TrimEnd(']')
                .Select(chr => chr == '#')
                .ToArray(),
            input
                .Split(' ')
                .Where(section => section.StartsWith('('))
                .Select(section =>
                    section
                        .TrimStart('(')
                        .TrimEnd(')')
                        .Split(',')
                        .Select(num => int.Parse(num))
                        .ToArray()
                )
                .ToList(),
            input
                .Split(' ')
                .Last()
                .TrimStart('{')
                .TrimEnd('}')
                .Split(',')
                .Select(num => int.Parse(num))
                .ToArray()
        ) { }
}
