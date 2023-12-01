namespace AoC_Toolbox.Pathfinding;

public interface IPathfinding<T>
{
    public long searchMinimumPathLength(T startNode, T endNode, long initialLength = 0);

    public long searchMinimumPathLength(T startNode, IEnumerable<T> endNodes, long initialLength = 0);
}
