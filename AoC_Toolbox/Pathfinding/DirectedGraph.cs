namespace AoC_Toolbox.Pathfinding;

public class DirectedGraph<T> : IGraph<T> where T : Node
{
    private IDictionary<T, IList<(T node, long cost)>> edges = new Dictionary<T, IList<(T node, long cost)>>();

    public void AddEdge(T from, T to, long cost)
    {
        if (edges.ContainsKey(from))
            edges[from].Add((to, cost));
        else
            edges.Add(from, new List<(T node, long cost)>() { (to, cost) });
    }

    public IEnumerable<(T node, long cost)> GetAdjacentNodes(T currentNode)
    {
        if (!edges.ContainsKey(currentNode))
            return new List<(T node, long cost)>();

        return edges[currentNode];
    }
}