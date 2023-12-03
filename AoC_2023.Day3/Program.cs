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

    static object? solutionPart1(string[] input)
    {
        var width = input.First().Length;
        var height = input.Length;

        var map = input.Select(x => $".{x}.")
                       .ToList();

        map.Insert(0, new string('.', width + 2));
        map.Add(new string('.', width + 2));

        var result = 0;

        for (int y = 1; y <= height; y++)
        {
            for (int x = 1; x <= width; x++)
            {
                if (!char.IsDigit(map[y][x]))
                {
                    continue;
                }
                
                var digits = map[y][x..].TakeWhile(char.IsDigit)
                                        .ToArray();

                if (IsValidNumber(map, y, x, digits))
                {
                    var number = int.Parse(new string(digits));

                    result += number;
                }

                x += digits.Length;
            }
        }

        return result;
    }

    private static bool IsValidNumber(List<string> map, int y, int x, char[] digits)
    {
        for (int yy = y - 1; yy <= y + 1; yy++)
        {
            for (int xx = x - 1; xx <= x + digits.Length; xx++)
            {
                if (!char.IsDigit(map[yy][xx]) && map[yy][xx] != '.')
                {
                    return true;
                }
            }
        }

        return false;
    }

    static object? solutionPart2(string[] input)
    {
        var width = input.First().Length;
        var height = input.Length;

        var map = input.Select(x => $".{x}.")
                       .ToList();

        map.Insert(0, new string('.', width + 2));
        map.Add(new string('.', width + 2));

        var result = 0;

        for (int y = 1; y <= height; y++)
        {
            for (int x = 1; x <= width; x++)
            {
                if (map[y][x] != '*')
                {
                    continue;
                }

                var gears = FindGears(map, result, y, x);

                if (gears.Count == 2)
                {
                    var gearRatioAsInt = gears.Select(int.Parse);

                    result += gearRatioAsInt.First() * gearRatioAsInt.Last();
                }
            }
        }

        return result;
    }

    private static List<string> FindGears(List<string> map, long result, int y, int x)
    {
        var gears = new List<string>();

        for (int yy = y - 1; yy <= y + 1; yy++)
        {
            for (int xx = x - 1; xx <= x + 1; xx++)
            {
                if (!char.IsDigit(map[yy][xx]))
                {
                    continue;
                }

                int i = FindStartOfNumber(map[yy], xx);

                var digits = map[yy][i..].TakeWhile(char.IsDigit)
                                         .ToArray();

                gears.Add(new string(digits));

                xx = i + digits.Length;
            }
        }

        return gears;
    }

    private static int FindStartOfNumber(string text, int xx)
    {
        var i = xx;

        while (char.IsDigit(text[i]))
        {
            i--;
        }

        return i + 1;
    }
}