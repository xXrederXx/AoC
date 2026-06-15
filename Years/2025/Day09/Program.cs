using AoC.Common;
using ZLinq;

namespace AoC.Y2025.Day09;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 09");

        string[] input = FileHelper.GetLines("data/input.txt");

        SolutionVerifier.VerifyAndLog("Part 1:", "4786902990", Part1(input));
        SolutionVerifier.VerifyAndLog("Part 2:", "1571016172", Part2(input));
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

        Dictionary<Vector2Int, bool> checkCache = new();
        ulong biggestArea = 0;
        for (int i = 0; i < redTiles.Length; i++)
        {
            Vector2Int red1 = redTiles[i];
            Console.WriteLine($"Next Tile: {red1} {i}/{redTiles.Length}");
            for (int j = i + 1; j < redTiles.Length; j++)
            {
                Vector2Int red2 = redTiles[j];

                Vector2Int[] borderTiles = GenerateAllLinePoints(red1, red2);

                if (!AllBorderInside(redTiles, checkCache, borderTiles))
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

    static bool AllBorderInside(
        Vector2Int[] redTiles,
        Dictionary<Vector2Int, bool> checkCache,
        Vector2Int[] borderTiles
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
            bool checkedTile = PointInsidePolygon.Contains(redTiles, borderTile) != PointInsidePolygon.PointContainment.Outside;
            checkCache.Add(borderTile, checkedTile);
            if (!checkedTile)
            {
                return false;
            }
        }
        return true;
    }

    static Vector2Int[] GenerateAllLinePoints(Vector2Int red1, Vector2Int red2)
    {

        // Horizontal Top
        Vector2Int[] ht = GenerateLinePoints(red1, new Vector2Int(red2.X, red1.Y));

        // Vertical Left
        Vector2Int[] vl = GenerateLinePoints(red1, new Vector2Int(red1.X, red2.Y));

        // Horizontal Bottom
        Vector2Int[] hb = GenerateLinePoints(red2, new Vector2Int(red2.X, red1.Y));

        // Vertical Right
        Vector2Int[] vr = GenerateLinePoints(red2, new Vector2Int(red1.X, red2.Y));

        return ArrayConcatenator.ConcatArrays(ht, vl, hb, vr);
    }

    static Vector2Int[] GenerateLinePoints(Vector2Int tile1, Vector2Int tile2)
    {
        (int start, int end, bool isHorizontal) = GenerateLinePointsMeta(tile1, tile2);

        Vector2Int[] greens = new Vector2Int[end - start + 1];

        for (int i = start; i <= end; i++)
        {
            greens[i - start] = (new Vector2Int(isHorizontal ? i : tile1.X, isHorizontal ? tile1.Y : i));
        }
        return greens;
    }

    static (int start, int end, bool isHorizontal) GenerateLinePointsMeta(Vector2Int tile1, Vector2Int tile2)
    {
        bool isHorizontal = tile1.X != tile2.X;
        int start;
        int end;
        if (isHorizontal)
        {
            start = Math.Min(tile1.X, tile2.X);
            end = Math.Max(tile1.X, tile2.X);
        }
        else
        {
            start = Math.Min(tile1.Y, tile2.Y);
            end = Math.Max(tile1.Y, tile2.Y);
        }
        return (start, end, isHorizontal);
    }
}
