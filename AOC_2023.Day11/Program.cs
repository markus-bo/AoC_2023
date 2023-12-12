using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;

internal class Solution
{
    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static IEnumerable<int> GetHoricontalInflation(string[] input)
    {
        for (int i = 0; i<input[0].Length; i++)
        {
            var allEmpty = true;

            for (int j = 0; j<input.Length; j++)
            {
                if (input[j][i] == '#')
                {
                    allEmpty = false;
                    break;
                }
            }

            if (allEmpty)
            {
                yield return i;
            }
        }
    }

    static IEnumerable<int> GetVerticalInflation(string[] input)
    {
        for (int i = 0; i<input.Length; i++)
        {
            if (input[i].All(x => x=='.'))
            {
                yield return i;
            }
        }
    }

    static object? solutionPart1(string[] input)
    {
        var horicontalExpansionIndices = GetHoricontalInflation(input);
        var verticalExpansionIndices = GetVerticalInflation(input);
        var points = new List<Point>();

        for (int i = 0; i<input.Length; i++)
        {
            for (int j = 0; j<input[0].Length; j++)
            {
                if (input[i][j] == '#')
                {
                    points.Add(new Point(j, i));
                }
            }
        }



        var result = 0L;

        for(int i = 0; i< points.Count - 1; i++)
        {
            for(int j = i+1; j < points.Count; j++)
            {
                var distance = (long)points[i].GetManhattenDistance(points[j]);

                for(long x = (long)Math.Min(points[i].X, points[j].X); x <= (long)Math.Max(points[i].X, points[j].X); x++)
                {
                    if (horicontalExpansionIndices.Contains((int)x))
                    {
                        
                        distance++;
                    }
                }

                for(long y = (long)Math.Min(points[i].Y, points[j].Y); y <= (long)Math.Max(points[i].Y, points[j].Y); y++)
                {
                    if (verticalExpansionIndices.Contains((int)y))
                    {
                        distance++;
                    }
                }

                result += distance;
            }
        }

        return result;
    }

    static object? solutionPart2(string[] input)
    {
        var horicontalExpansionIndices = GetHoricontalInflation(input);
        var verticalExpansionIndices = GetVerticalInflation(input);
        var points = new List<Point>();

        for (int i = 0; i<input.Length; i++)
        {
            for (int j = 0; j<input[0].Length; j++)
            {
                if (input[i][j] == '#')
                {
                    points.Add(new Point(j, i));
                }
            }
        }

        

        var result = 0L;

        for(int i = 0; i< points.Count - 1; i++)
        {
            for(int j = i+1; j < points.Count; j++)
            {
                var distance = (long)points[i].GetManhattenDistance(points[j]);

                for(long x = (long)Math.Min(points[i].X, points[j].X); x <= (long)Math.Max(points[i].X, points[j].X); x++)
                {
                    if (horicontalExpansionIndices.Contains((int)x))
                    {
                        distance+=999999;
                    }
                }

                for(long y = (long)Math.Min(points[i].Y, points[j].Y); y <= (long)Math.Max(points[i].Y, points[j].Y); y++)
                {
                    if (verticalExpansionIndices.Contains((int)y))
                    {
                        distance+=999999;
                    }
                }

                result += distance;
            }
        }

        return result;
    }
}