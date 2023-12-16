using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;
using System.Numerics;

internal class Solution
{
    enum Direction
    {
        None = 0,
        Up = 1,
        Left = 2,
        Down = 4,
        Right = 8,
    }
    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static object? solutionPart1(string[] input)
    {
        var height = input.Length;
        var width = input[0].Length;

        mirrorMap = new char[height][];
        heatMap = new Direction[height][];

        for(var i =0; i< height; i++)
        {
            mirrorMap[i] = input[i].ToCharArray();
            heatMap[i] = new Direction[width];
        }

        GetHeatMap(0, 0, width, height, Direction.Right);

        var result = 0;

        for(var i = 0; i < height; i++)
        {
            result += heatMap[i].Count(x => x != Direction.None);
        }

        return result;
    }

    static char[][] mirrorMap;
    static Direction[][] heatMap;

    static void GetHeatMap(int y, int x, int width, int height, Direction travelDirection)
    {
        if (y < 0 || y >= height || x < 0 || x >= width)
        {
            return;
        }

        if (heatMap[y][x] == travelDirection)
        {
            return;
        }

        heatMap[y][x] = travelDirection;

        if (mirrorMap[y][x] == '.')
        {
            if (travelDirection == Direction.Right)
            {
                GetHeatMap(y, x + 1, width, height, travelDirection);
            }
            else if (travelDirection == Direction.Left)
            {
                GetHeatMap(y, x - 1, width, height, travelDirection);
            }
            else if (travelDirection == Direction.Up)
            {
                GetHeatMap(y - 1, x, width, height, travelDirection);
            }
            else if (travelDirection == Direction.Down)
            {
                GetHeatMap(y + 1, x, width, height, travelDirection);
            }
        }
        else if (mirrorMap[y][x] == '/')
        {
            if (travelDirection == Direction.Right)
            {
                GetHeatMap(y - 1, x, width, height, Direction.Up);
            }
            else if (travelDirection == Direction.Left)
            {
                GetHeatMap(y + 1, x, width, height, Direction.Down);
            }
            else if (travelDirection == Direction.Up)
            {
                GetHeatMap(y, x + 1, width, height, Direction.Right);
            }
            else if (travelDirection == Direction.Down)
            {
                GetHeatMap(y, x - 1, width, height, Direction.Left);
            }
        }
        else if (mirrorMap[y][x] == '\\')
        {
            if (travelDirection == Direction.Right)
            {
                GetHeatMap(y + 1, x, width, height, Direction.Down);
            }
            else if (travelDirection == Direction.Left)
            {
                GetHeatMap(y - 1, x, width, height, Direction.Up);
            }
            else if (travelDirection == Direction.Up)
            {
                GetHeatMap(y, x - 1, width, height, Direction.Left);
            }
            else if (travelDirection == Direction.Down)
            {
                GetHeatMap(y, x + 1, width, height, Direction.Right);
            }
        }
        else if (mirrorMap[y][x] == '-')
        {
            if (travelDirection == Direction.Right)
            {
                GetHeatMap(y, x + 1, width, height, travelDirection);
            }
            else if (travelDirection == Direction.Left)
            {
                GetHeatMap(y, x - 1, width, height, travelDirection);
            }
            else if (travelDirection == Direction.Up || travelDirection == Direction.Down)
            {
                GetHeatMap(y, x + 1, width, height, Direction.Right);
                GetHeatMap(y, x - 1, width, height, Direction.Left);
            }
        }
        else if (mirrorMap[y][x] == '|')
        {
            if (travelDirection == Direction.Up)
            {
                GetHeatMap(y - 1, x, width, height, travelDirection);
            }
            else if (travelDirection == Direction.Down)
            {
                GetHeatMap(y + 1, x, width, height, travelDirection);
            }
            else if (travelDirection == Direction.Left || travelDirection == Direction.Right)
            {
                GetHeatMap(y - 1, x, width, height, Direction.Up);
                GetHeatMap(y + 1, x, width, height, Direction.Down);
            }
        }
    }

    static object? solutionPart2(string[] input)
    {
        var height = input.Length;
        var width = input[0].Length;

        mirrorMap = new char[height][];

        for (var i = 0; i < height; i++)
        {
            mirrorMap[i] = input[i].ToCharArray();
        }

        var maxResult = 0;

        var resetHeatMap = () =>
        {
            heatMap = new Direction[height][];

            for (var j = 0; j < height; j++)
            {
                heatMap[j] = new Direction[width];
            }
        };

        var getHeatCount = () => heatMap.Select(l => l.Count(x => x != Direction.None)).Sum();
        
        for (var i = 0; i < width; i++)
        {
            resetHeatMap();

            GetHeatMap(0, i, width, height, Direction.Down);

            maxResult = Math.Max(maxResult, getHeatCount());
        }

        for (var i = 0; i < width; i++)
        {
            resetHeatMap();

            GetHeatMap(height - 1, i, width, height, Direction.Up);

            maxResult = Math.Max(maxResult, getHeatCount());
        }

        for (var i = 0; i < height; i++)
        {
            resetHeatMap();

            GetHeatMap(i, 0, width, height, Direction.Right);

            maxResult = Math.Max(maxResult, getHeatCount());
        }

        for (var i = 0; i < height; i++)
        {
            resetHeatMap();

            GetHeatMap(i, width - 1, width, height, Direction.Left);

            maxResult = Math.Max(maxResult, getHeatCount());
        }

        return maxResult;
    }
}