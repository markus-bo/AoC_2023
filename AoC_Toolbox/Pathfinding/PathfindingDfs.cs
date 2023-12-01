namespace AoC_Toolbox.Pathfinding;

public class PathfindingDfs<T> : IPathfinding<T> where T : Node
{
    private Dictionary<T, long> exploredStates = new Dictionary<T, long>();

    private IGraph<T> _graph;

    public PathfindingDfs(IGraph<T> graph)
    {
        _graph = graph;
    }

    public long searchMinimumPathLength(T startNode, T endNode, long initialLength = 0)
    {
        exploredStates.Clear();

        return searchMinimumPathLengthDfsSingle(startNode, endNode, initialLength);
    }

    public long searchMinimumPathLength(T startNode, IEnumerable<T> endNodes, long initialLength = 0)
    {
        exploredStates.Clear();

        return searchMinimumPathLengthDfsMulti(startNode, endNodes, initialLength);
    }

    private long searchMinimumPathLengthDfsSingle(T currentNode, T endNode, long pathLength)
    {
        // Skip further exploration on that node if current length is longer than already found length
        if (exploredStates.ContainsKey(currentNode))
            if (exploredStates[currentNode] <= pathLength)
                return long.MaxValue;
            else
                exploredStates[currentNode] = pathLength;
        else
            exploredStates.Add(currentNode, pathLength);

        // Skip further exploration on that node if target has been reached
        if (currentNode == endNode)
            return pathLength;

        // expand towards next states
        var minLength = long.MaxValue;
        foreach (var next in _graph.GetAdjacentNodes(currentNode))
            minLength = Math.Min(minLength, searchMinimumPathLengthDfsSingle(next.node, endNode, pathLength + 1));

        return minLength;
    }

    private long searchMinimumPathLengthDfsMulti(T currentNode, IEnumerable<T> endNodes, long pathLength)
    {
        // Skip further exploration on that node if current length is longer than already found length
        if (exploredStates.ContainsKey(currentNode))
            if (exploredStates[currentNode] <= pathLength)
                return long.MaxValue;
            else
                exploredStates[currentNode] = pathLength;
        else
            exploredStates.Add(currentNode, pathLength);

        // Skip further exploration on that node if target has been reached
        if (endNodes.Contains(currentNode))
            return pathLength;

        // expand towards next states
        var minLength = long.MaxValue;
        foreach (var next in _graph.GetAdjacentNodes(currentNode))
            minLength = Math.Min(minLength, searchMinimumPathLengthDfsMulti(next.node, endNodes, pathLength + 1));

        return minLength;
    }
}
