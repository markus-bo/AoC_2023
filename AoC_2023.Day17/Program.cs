using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

enum Direction
{
    None = 0,
    Up = 1,
    Left = 2,
    Down = 4,
    Right = 8,
}

internal record HeatNode : Node
{
    public Direction Direction { get; set; }

    public int StraightLength { get; set; }

    public Point Point { get; set; }

    public HeatNode(Point point, Direction direction, int straightLength)
    {
        this.Point = point;
        this.Direction = direction;
        this.StraightLength = straightLength;
    }
}

internal class GraphP1 : IGraph<HeatNode>
{
    public int[][] Map { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public void AddEdge(HeatNode from, HeatNode to, long cost)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(HeatNode node, long cost)> GetAdjacentNodes(HeatNode currentNode)
    {
        var y = currentNode.Point.Y;
        var x = currentNode.Point.X;

        if (currentNode.Direction == Direction.Left && currentNode.Point.X > 0 && currentNode.StraightLength < 3)
        {
            yield return (new HeatNode(new Point(x - 1, y), Direction.Left, currentNode.StraightLength + 1), Map[y][x - 1]);
        }

        if (currentNode.Direction == Direction.Right && currentNode.Point.X < Width - 1 && currentNode.StraightLength < 3)
        {
            yield return (new HeatNode(new Point(x + 1, y), Direction.Right, currentNode.StraightLength + 1), Map[y][x + 1]);
        }

        if (currentNode.Direction == Direction.Left || currentNode.Direction == Direction.Right)
        { 
            if (currentNode.Point.Y > 0)
            {
                yield return (new HeatNode(new Point(x, y - 1), Direction.Up, 1), Map[y - 1][x]);
            }
            if (currentNode.Point.Y < Height - 1)
            {
                yield return (new HeatNode(new Point(x, y + 1), Direction.Down, 1), Map[y + 1][x]);
            }
        }

        if (currentNode.Direction == Direction.Up && currentNode.Point.Y > 0 && currentNode.StraightLength < 3)
        {
            yield return (new HeatNode(new Point(x, y - 1), Direction.Up, currentNode.StraightLength + 1), Map[y - 1][x]);
        }

        if (currentNode.Direction == Direction.Down && currentNode.Point.Y < Height - 1 && currentNode.StraightLength < 3)
        {
            yield return (new HeatNode(new Point(x, y + 1), Direction.Down, currentNode.StraightLength + 1), Map[y + 1][x]);
        }

        if (currentNode.Direction == Direction.Up || currentNode.Direction == Direction.Down)
        {
            if (currentNode.Point.X > 0)
            {
                yield return (new HeatNode(new Point(x - 1, y), Direction.Left, 1), Map[y][x - 1]);
            }
            if (currentNode.Point.X < Width - 1)
            {
                yield return (new HeatNode(new Point(x + 1, y), Direction.Right, 1), Map[y][x + 1]);
            }
        }

    }
}

internal class GraphP2 : IGraph<HeatNode>
{
    public int[][] Map { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public void AddEdge(HeatNode from, HeatNode to, long cost)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(HeatNode node, long cost)> GetAdjacentNodes(HeatNode currentNode)
    {
        var y = currentNode.Point.Y;
        var x = currentNode.Point.X;

        if (currentNode.Direction == Direction.Left && currentNode.Point.X > 0 && currentNode.StraightLength < 10)
        {
            yield return (new HeatNode(new Point(x - 1, y), Direction.Left, currentNode.StraightLength + 1), Map[y][x - 1]);
        }

        if (currentNode.Direction == Direction.Right && currentNode.Point.X < Width - 1 && currentNode.StraightLength < 10)
        {
            yield return (new HeatNode(new Point(x + 1, y), Direction.Right, currentNode.StraightLength + 1), Map[y][x + 1]);
        }

        if ((currentNode.Direction == Direction.Left || currentNode.Direction == Direction.Right) && currentNode.StraightLength >= 4)
        {
            if (currentNode.Point.Y > 0)
            {
                yield return (new HeatNode(new Point(x, y - 1), Direction.Up, 1), Map[y - 1][x]);
            }
            if (currentNode.Point.Y < Height - 1)
            {
                yield return (new HeatNode(new Point(x, y + 1), Direction.Down, 1), Map[y + 1][x]);
            }
        }

        if (currentNode.Direction == Direction.Up && currentNode.Point.Y > 0 && currentNode.StraightLength < 10)
        {
            yield return (new HeatNode(new Point(x, y - 1), Direction.Up, currentNode.StraightLength + 1), Map[y - 1][x]);
        }

        if (currentNode.Direction == Direction.Down && currentNode.Point.Y < Height - 1 && currentNode.StraightLength < 10)
        {
            yield return (new HeatNode(new Point(x, y + 1), Direction.Down, currentNode.StraightLength + 1), Map[y + 1][x]);
        }

        if ((currentNode.Direction == Direction.Up || currentNode.Direction == Direction.Down) && currentNode.StraightLength >= 4)
        {
            if (currentNode.Point.X > 0)
            {
                yield return (new HeatNode(new Point(x - 1, y), Direction.Left, 1), Map[y][x - 1]);
            }
            if (currentNode.Point.X < Width - 1)
            {
                yield return (new HeatNode(new Point(x + 1, y), Direction.Right, 1), Map[y][x + 1]);
            }
        }

    }
}

internal class Solution
{
    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static object? solutionPart1(string[] input)
    {
        var graph = new GraphP1();

        graph.Height = input.Length;
        graph.Width = input[0].Length;

        graph.Map = input.Select(x => x.Select(c => c - '0').ToArray())
                   .ToArray();

        var path = new PathfindingAStar<HeatNode>(graph).searchMinimumPath(
            new HeatNode(new Point(0, 0), Direction.Right, 0),
            new HeatNode(new Point(graph.Width - 1, graph.Height - 1), Direction.Right, 0),
            (a, b) => a.Point.GetManhattenDistance(b.Point),
            (a, b) => a.Point == b.Point,
            0);


        var result = path.Select(p => graph.Map[(int)p.Point.Y][(int)p.Point.X]).Sum();

        result -= graph.Map[0][0];
        result -= graph.Map[graph.Height - 1][graph.Width - 1];

        return result;
    }
    
    static object? solutionPart2(string[] input)
    {
        var graph = new GraphP2();

        graph.Height = input.Length;
        graph.Width = input[0].Length;

        graph.Map = input.Select(x => x.Select(c => c - '0').ToArray())
                   .ToArray();

        var path = new PathfindingAStar<HeatNode>(graph).searchMinimumPath(
            new HeatNode(new Point(0, 0), Direction.Right, 0),
            new HeatNode(new Point(graph.Width - 1, graph.Height - 1), Direction.Right, 0),
            (a, b) => a.Point.GetManhattenDistance(b.Point),
            (a, b) => (a.Point == b.Point) && a.StraightLength >= 4,
            0);


        var result = path.Select(p => graph.Map[(int)p.Point.Y][(int)p.Point.X]).Sum();

        result -= graph.Map[0][0];
        result -= graph.Map[graph.Height - 1][graph.Width - 1];

        return result;
    }
}