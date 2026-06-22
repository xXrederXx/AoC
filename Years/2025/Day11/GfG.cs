// C# Code to count paths from source 
// to destinattion using Topological Sort
// Source: https://www.geeksforgeeks.org/dsa/count-possible-paths-two-vertices/#-1
// Modified to meet requirements

namespace AoC.Y2025.Day11;

static class GfG
{

    public static int CountPaths(HashSet<Node> nodes)
    {
        Dictionary<int, Node> IdOfNodes = new Dictionary<int, Node>(
            nodes.Select((node, idx) => new KeyValuePair<int, Node>(idx, node)));

        int n = nodes.Count;
        int sourceId = IdOfNodes.First(kvp => kvp.Value.Id == "you").Key;
        int destinationId = IdOfNodes.First(kvp => kvp.Value.Id == "out").Key;
        List<int[]> edgeList = [];
        foreach (Node node in nodes)
        {
            if (node.Childs is null) continue;
            foreach (string childId in node.Childs)
            {
                edgeList.Add([IdOfNodes.First(kvp => kvp.Value.Id == node.Id).Key, IdOfNodes.First(kvp => kvp.Value.Id == childId).Key]);
            }
        }
        return CountPaths(n, edgeList.ToArray(), sourceId, destinationId);
    }

    public static int CountPaths(int n, int[][] edgeList,
                          int source, int destination)
    {

        // Create adjacency list (1-based indexing)
        List<int>[] graph = new List<int>[n + 1];
        int[] indegree = new int[n + 1];

        for (int i = 0; i <= n; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (var edge in edgeList)
        {
            int u = edge[0];
            int v = edge[1];
            graph[u].Add(v);
            indegree[v]++;
        }

        // Perform topological sort using Kahn's algorithm
        Queue<int> q = new Queue<int>();
        for (int i = 1; i <= n; i++)
        {
            if (indegree[i] == 0)
            {
                q.Enqueue(i);
            }
        }

        List<int> topoOrder = new List<int>();
        while (q.Count > 0)
        {
            int node = q.Dequeue();
            topoOrder.Add(node);

            foreach (int neighbor in graph[node])
            {
                indegree[neighbor]--;
                if (indegree[neighbor] == 0)
                {
                    q.Enqueue(neighbor);
                }
            }
        }

        // Array to store number of ways to reach each node
        int[] ways = new int[n + 1];
        ways[source] = 1;

        // Traverse in topological order
        foreach (int node in topoOrder)
        {
            foreach (int neighbor in graph[node])
            {
                ways[neighbor] += ways[node];
            }
        }

        return ways[destination];
    }
}