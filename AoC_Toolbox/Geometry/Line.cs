namespace AoC_Toolbox.Geometry;

public record Line
{
    public Point Start { get; init; }

    public Point End { get; init; }

    public long ManhattenDistance => Start.GetManhattenDistance(End);

    public double EuclidicDistance => Start.GetEuclidicDistance(End);

    public bool IsHoricontal => Start.Y == End.Y;

    public bool IsVertical => Start.X == End.X;

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }

    public IEnumerable<Point> GetBresenhamLinePoints()
    {
        return Start.GetBresenhamLinePoints(End);
    }
}
