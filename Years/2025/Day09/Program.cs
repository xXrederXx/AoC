using AoC.Common;
using ZLinq;

namespace AoC.Y2025.Day09;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 09");

        string[] input = FileHelper.GetLines("data/input.txt");

        System.Console.WriteLine("Part 1: " + Part1(input));
        System.Console.WriteLine("Part 2: " + Part2(input));
    }

    static string Part1(string[] input)
    {
        Vector2Int[] points = input
            .Select(x => x.Split(','))
            .Select(x => new Vector2Int(int.Parse(x[0]), int.Parse(x[1])))
            .ToArray();
        ulong biggestArea = 0;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2Int point1 = points[i];
            for (int j = i + 1; j < points.Length; j++)
            {
                Vector2Int point2 = points[j];
                ulong dx = (ulong)Math.Abs(point1.X - point2.X) + 1;
                ulong dy = (ulong)Math.Abs(point1.Y - point2.Y) + 1;

                ulong area = dx * dy;
                if (area > biggestArea)
                {
                    biggestArea = area;
                }
            }
        }
        return biggestArea.ToString();
    }

    static string Part2(string[] input)
    {
        Vector2Int[] redTiles = input
            .Select(x => x.Split(','))
            .Select(x => new Vector2Int(int.Parse(x[0]), int.Parse(x[1])))
            .ToArray();

        HashSet<Vector2Int> greenTiles = new HashSet<Vector2Int>();
        for (int i = 0; i < redTiles.Length; i++)
        {
            Vector2Int red1 = redTiles[i];
            Vector2Int red2 = redTiles[(i + 1) % redTiles.Length];
            Vector2Int[] newGreens = generateLinePoints(red1, red2);
            foreach (Vector2Int greenTile in newGreens)
            {
                greenTiles.Add(greenTile);
            }
        }
        Console.WriteLine("Loaded all green tiles");

        Dictionary<Vector2Int, bool> checkCache = new();
        ulong biggestArea = 0;
        for (int i = 0; i < redTiles.Length; i++)
        {
            Vector2Int red1 = redTiles[i];
            Console.WriteLine($"Next Tile: {red1} {i}/{redTiles.Length}");
            for (int j = i + 1; j < redTiles.Length; j++)
            {
                Vector2Int red2 = redTiles[j];

                List<Vector2Int> borderTiles = [];
                borderTiles.AddRange(generateLinePoints(red1, new Vector2Int(red2.X, red1.Y)));
                borderTiles.AddRange(generateLinePoints(red1, new Vector2Int(red1.X, red2.Y)));
                borderTiles.AddRange(generateLinePoints(red2, new Vector2Int(red2.X, red1.Y)));
                borderTiles.AddRange(generateLinePoints(red2, new Vector2Int(red1.X, red2.Y)));

                if (!allBorderInside(greenTiles, checkCache, borderTiles))
                {
                    continue;
                }

                ulong dx = (ulong)Math.Abs(red1.X - red2.X) + 1;
                ulong dy = (ulong)Math.Abs(red1.Y - red2.Y) + 1;

                ulong area = dx * dy;
                if (area > biggestArea)
                {
                    biggestArea = area;
                }
            }
        }
        return biggestArea.ToString();
    }

    static bool allBorderInside(
        HashSet<Vector2Int> greenTiles,
        Dictionary<Vector2Int, bool> checkCache,
        List<Vector2Int> borderTiles
    )
    {
        foreach (Vector2Int borderTile in borderTiles)
        {
            if (checkCache.TryGetValue(borderTile, out bool tileIsInside))
            {
                if (tileIsInside)
                {
                    continue;
                }
                return false;
            }
            bool checkedTile = isInside(greenTiles, borderTile);
            checkCache.Add(borderTile, checkedTile);
            if (!checkedTile)
            {
                return false;
            }
        }
        return true;
    }

    static bool isInside(HashSet<Vector2Int> greenTiles, Vector2Int checkTile)
    {
        if (greenTiles.Contains(checkTile))
        {
            return true;
        }
        int numCrossed = greenTiles
            .AsValueEnumerable()
            .Where(tile => tile.Y == checkTile.Y)
            .Where(tile => tile.X > checkTile.X)
            .Count();
        return numCrossed % 2 == 1;
    }

    static Vector2Int[] generateLinePoints(Vector2Int tile1, Vector2Int tile2)
    {
        int start = 0;
        int end = 0;
        bool isX = false;
        if (tile1.X != tile2.X)
        {
            start = tile1.X < tile2.X ? tile1.X : tile2.X;
            end = tile1.X > tile2.X ? tile1.X : tile2.X;
            isX = true;
        }
        if (tile1.Y != tile2.Y)
        {
            start = tile1.Y < tile2.Y ? tile1.Y : tile2.Y;
            end = tile1.Y > tile2.Y ? tile1.Y : tile2.Y;
        }
        List<Vector2Int> greens = [];
        for (int i = start; i <= end; i++)
        {
            greens.Add(new Vector2Int(isX ? i : tile1.X, isX ? tile1.X : i));
        }
        return greens.ToArray();
    }
}
