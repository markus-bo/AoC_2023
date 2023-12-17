namespace AoC_Toolbox.Pathfinding;

public class PathfindingAStar<T> : IPathfindingAStar<T> where T : Node
{
    private IGraph<T> _graph;

    public PathfindingAStar(IGraph<T> graph)
    {
        _graph = graph;
    }

    public IEnumerable<T> searchMinimumPath(T startNode, T endNode, Func<T, T, long> heuristic, Func<T, T, bool> equalityComparer, long initialLength = 0)
    {
        var pathEnd = searchMinimumPathLengthAStar(startNode, endNode, heuristic, equalityComparer, initialLength);

        return reconstructPath(pathEnd);
    }
    
    public long searchMinimumPathLength(T startNode, T endNode, Func<T, T, long> heuristic, Func<T, T, bool> equalityComparer, long initialLength = 0)
    {
        return searchMinimumPath(startNode, endNode, heuristic, equalityComparer, initialLength).Count() + initialLength;
    }

    private NodeAStar<T> searchMinimumPathLengthAStar(T startNode, T endNode, Func<T, T, long> heuristic, Func<T, T, bool> equalityComparer, long initialLength = 0)
    {
        var openQueue = new PriorityQueue<NodeAStar<T>, long>();
        var openList = new Dictionary<T, NodeAStar<T>>();
        var closedList = new Dictionary<T, NodeAStar<T>>();

        var endNodeAStar = new NodeAStar<T>(endNode);

        openQueue.Enqueue(new NodeAStar<T>(startNode), 0);

        while(openQueue.TryDequeue(out NodeAStar<T>? currentNode, out long Priority))
        {
            foreach (var next in _graph.GetAdjacentNodes((T)currentNode.NodePosition)
                                       .Select(node => new NodeAStar<T>(
                                                        position: node.node,
                                                        parentNode: currentNode,
                                                        gcost: currentNode.GCost + node.cost,
                                                        hcost: heuristic(endNode, node.node))))
            {
                if (equalityComparer(next.NodePosition,endNode) == true)
                {
                    endNodeAStar.ParentNode = next;
                    goto PATH_FOUND;
                }

                if (openList.ContainsKey(next.NodePosition))
                    if (openList[next.NodePosition].FCost <= next.FCost)
                        continue;

                if (closedList.ContainsKey(next.NodePosition))
                    if (closedList[next.NodePosition].FCost <= next.FCost)
                        continue;

                openQueue.Enqueue(next, next.FCost);

                if (openList.ContainsKey(next.NodePosition))
                    openList[next.NodePosition] = next;
                else
                    openList.Add(next.NodePosition, next);
            }

            if (closedList.ContainsKey(currentNode.NodePosition))
                closedList[currentNode.NodePosition] = currentNode;
            else
                closedList.Add(currentNode.NodePosition, currentNode);
        }

    PATH_FOUND:

        return endNodeAStar;
    }
    
    private IEnumerable<T> reconstructPath(NodeAStar<T> end)
    {
        var path = new List<T>();
        var current = end;

        while (current != null)
        {
            path.Add(current.NodePosition);

            current = current.ParentNode;
        }

        path.Reverse();

        return path;
    }
}
