namespace AoC_Toolbox.Pathfinding;

public interface IPathfindingAStar<T> where T : Node
{
    public long searchMinimumPathLength(T startNode, T endNode, Func<T, T, long> heuristic, Func<T, T, bool> equalityComparer, long initialLength = 0);
}
