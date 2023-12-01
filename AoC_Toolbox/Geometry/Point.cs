namespace AoC_Toolbox.Geometry;

public record Point
{
    public long X { get; init; }
    public long Y { get; init; }
    public long Z { get; init; }

    public Point(long x, long y)
    {
        X = x;
        Y = y;
        Z = 0;
    }

    public Point(long x, long y, long z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point(IEnumerable<long> vector)
    {
        X = vector.FirstOrDefault(0L);
        Y = vector.Skip(1).FirstOrDefault(0L);
        Z = vector.Skip(2).FirstOrDefault(0L);
    }

    public long GetManhattenDistance(Point other) => GetManhattenDistance(other.X, other.Y, other.Z);

    public long GetManhattenDistance(long x, long y, long z) => Math.Abs(X - x) + Math.Abs(Y - y) + Math.Abs(Z - z);

    public double GetEuclidicDistance(Point other) => GetEuclidicDistance(other.X, other.Y, other.Z);

    public double GetEuclidicDistance(long x, long y, long z)
    {
        return Math.Sqrt(Math.Pow(X - x, 2) *
                         Math.Pow(Y - y, 2) *
                         Math.Pow(Z - z, 2));
    }

    public IEnumerable<Point> GetBresenhamLinePoints(Point other)
    {
        if (Z != 0 || other.Z != 0)
            throw new NotImplementedException($"{nameof(GetBresenhamLinePoints)} only for Points in X/Y plane implemented");

        if (Y < other.Y)
            return Bresenham(this, other);
        else
            return Bresenham(other, this);
    }
    private static IEnumerable<Point> Bresenham(Point fromPoint, Point toPoint)
    {
        var retValue = new List<Point>() { fromPoint };

        var x0 = fromPoint.X;
        var y0 = fromPoint.Y;
        var x1 = toPoint.X;
        var y1 = toPoint.Y;

        var dx = Math.Abs(x1 - x0);
        var dy = Math.Abs(y1 - y0);

        var sx = x0 < x1 ? 1 : -1;
        var sy = y0 < y1 ? 1 : -1;

        var err = dx - dy;
        var e2 = 0L;
        var currentX = x0;
        var currentY = y0;

        while (true)
        {
            e2 = 2 * err;
            if (e2 > -1 * dy)
            {
                err -= dy;
                currentX += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                currentY += sy;
            }

            if (currentX == x1 && currentY == y1) break;

            retValue.Add(new Point(currentX, currentY));
        }

        retValue.Add(toPoint);

        return retValue;
    }
}