namespace AoC_Toolbox.Pathfinding;

public interface IGraph<T>
{
    public void AddEdge(T from, T to, long cost);

    public IEnumerable<(T node, long cost)> GetAdjacentNodes(T currentNode);
}