using AoC.Common;

namespace AoC.Y2025.Day11;

internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Advent of Code 2025 - Day 11");

        string[] input = FileHelper.GetLines("data/input.txt");

        SolutionVerifier.VerifyAndLog("Part 1:", Part1(input), "477");
        SolutionVerifier.VerifyAndLog("Part 2:", Part2(input), "383307150903216");
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
        Dictionary<string, Node> nodes = input
            .Select(line =>
                line.Split(':')
                    .SelectMany(x =>
                        x.Split(' ').Select(x => x.Trim().Trim(':')).Where(x => !x.IsWhiteSpace())
                    )
            )
            .Select(ids => new Node(ids.First(), ids.Skip(1).ToArray()))
            .ToDictionary(node => node.Id);
        nodes.Add("out", new Node("out", []));
        System.Console.WriteLine($"Loaded {nodes.Count} nodes");

        Dictionary<string, ulong> cache = new();
        ulong dacToFft = nodes["dac"].CountTo(nodes, cache, "fft");
        System.Console.WriteLine($"Counted {dacToFft} paths from dac to fft (dac: {nodes["dac"].Childs?.Length})");
        cache.Clear();
        ulong fftToDac = nodes["fft"].CountTo(nodes, cache, "dac");
        System.Console.WriteLine($"Counted {fftToDac} paths from fft to dac (fft: {nodes["fft"].Childs?.Length})");
        cache.Clear();

        ulong between;
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
            throw new InvalidDataException("No path between fft and dac found!!!!");
        }
        System.Console.WriteLine("Loaded between");

        ulong fromSrv = nodes["svr"].CountTo(nodes, cache, srvToNode);
        cache.Clear();
        ulong toOut = nodes[outFromNode].CountTo(nodes, cache, "out");
        cache.Clear();

        return (fromSrv * between * toOut)
            .ToString();
    }
}
