namespace AoC_Toolbox.Pathfinding;

public interface IPathfindingAStar<T> where T : Node
{
    public long searchMinimumPathLength(T startNode, T endNode, Func<T, T, long> heuristic, long initialLength = 0);
}
