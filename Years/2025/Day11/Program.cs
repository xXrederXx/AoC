using AoC.Common;

namespace AoC.Y2025.Day11;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 11");

        string[] input = FileHelper.GetLines("data/input.txt");

        // SolutionVerifier.VerifyAndLog("Part 1:", Part1(input), "477");
        System.Console.WriteLine("Part 2:" + Part2(input));
    }

    static string Part1(string[] input)
    {
        HashSet<Node> nodes = input
            .Select(line =>
                line.Split(':')
                    .SelectMany(x =>
                        x.Split(' ').Select(x => x.Trim().Trim(':')).Where(x => !x.IsWhiteSpace())
                    )
            )
            .Select(ids => new Node(ids.First(), ids.Skip(1).ToArray()))
            .ToHashSet();
        nodes.Add(new Node("out", []));

        return nodes.First(n => n.Id == "you").CountToOut(nodes).ToString();
    }

    static string Part2(string[] input)
    {
        HashSet<Node> nodes = input
            .Select(line =>
                line.Split(':')
                    .SelectMany(x =>
                        x.Split(' ').Select(x => x.Trim().Trim(':')).Where(x => !x.IsWhiteSpace())
                    )
            )
            .Select(ids => new Node(ids.First(), ids.Skip(1).ToArray()))
            .ToHashSet();
        nodes.Add(new Node("out", []));
        System.Console.WriteLine($"Loaded {nodes.Count} nodes");

        int dacToFft = nodes.First(n => n.Id == "dac").CountTo(nodes, "fft");
        System.Console.WriteLine($"Counted {dacToFft} paths from dac to fft");
        int fftToDac = nodes.First(n => n.Id == "fft").CountTo(nodes, "dac");
        System.Console.WriteLine($"Counted {fftToDac} paths from fft to dac");
        int between;

        string srvToNode;
        string outFromNode;
        if (dacToFft > 0)
        {
            srvToNode = "dac";
            outFromNode = "fft";
            between = dacToFft;
        }
        else if (fftToDac > 0)
        {
            srvToNode = "fft";
            outFromNode = "dac";
            between = fftToDac;
        }
        else
        {
            throw new InvalidDataException("NOPE");
        }
        System.Console.WriteLine("Loaded between");

        int fromSrv = nodes.First(n => n.Id == "svr").CountTo(nodes, srvToNode);
        int toOut = nodes.First(n => n.Id == outFromNode).CountTo(nodes, "out");

        return (fromSrv * between * toOut)
            .ToString();
    }
}
