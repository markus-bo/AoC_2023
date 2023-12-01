namespace AoC_Toolbox.Pathfinding;

public class PathfindingBfs<T> : IPathfinding<T> where T : Node
{
    private Dictionary<T, long> _exploredStates = new Dictionary<T, long>();

    private IGraph<T> _graph;

    public PathfindingBfs(IGraph<T> graph)
    {
        _graph = graph;
    }

    public long searchMinimumPathLength(T startNode, T endNode, long initialLength = 0)
    {
        _exploredStates.Clear();

        return searchMinimumPathLengthBfsSingle(startNode, endNode, initialLength);
    }

    public long searchMinimumPathLength(T startNode, IEnumerable<T> endNodes, long initialLength = 0)
    {
        _exploredStates.Clear();

        return searchMinimumPathLengthBfsMulti(startNode, endNodes, initialLength);
    }

    private long searchMinimumPathLengthBfsSingle(T startNode, T endNode, long initialLength)
    {
        // Initialize queue with start element
        var queue = new Queue<(T node, long step)>();
        queue.Enqueue((startNode, initialLength));

        var minLength = long.MaxValue;

        // Dequeue as long as there is something
        while (queue.TryDequeue(out (T node, long step) current))
        {
            // Evaluate path length and skip further processing if target found
            if (current.node == endNode)
            {
                minLength = Math.Min(minLength, current.step);
                continue;
            }

            // Skip further exploration on that node in case path length is more than the minimum found already
            if (current.step >= minLength)
                continue;

            // Memorize the explored node
            if (_exploredStates.ContainsKey(current.node))
                if (_exploredStates[current.node] <= current.step)
                    continue;
                else
                    _exploredStates[current.node] = current.step;
            else
                _exploredStates.Add(current.node, current.step);

            // Explore edges
            foreach (var next in _graph.GetAdjacentNodes(current.node))
                queue.Enqueue((next.node, current.step + 1));
        }

        return minLength;
    }

    private long searchMinimumPathLengthBfsMulti(T startNode, IEnumerable<T> endNodes, long initialLength)
    {
        // Initialize queue with start element
        var queue = new Queue<(T node, long step)>();
        queue.Enqueue((startNode, initialLength));

        var minLength = long.MaxValue;

        // Dequeue as long as there is something
        while (queue.TryDequeue(out (T node, long step) current))
        {
            // Evaluate path length and skip further processing if target found
            if (endNodes.Contains(current.node))
            {
                minLength = Math.Min(minLength, current.step);
                continue;
            }

            // Skip further exploration on that node in case path length is more than the minimum found already
            if (current.step >= minLength)
                continue;

            // Memorize the explored node
            if (_exploredStates.ContainsKey(current.node))
                if (_exploredStates[current.node] <= current.step)
                    continue;
                else
                    _exploredStates[current.node] = current.step;
            else
                _exploredStates.Add(current.node, current.step);

            // Explore edges
            foreach (var next in _graph.GetAdjacentNodes(current.node))
                queue.Enqueue((next.node, current.step + 1));
        }

        return minLength;
    }
}
