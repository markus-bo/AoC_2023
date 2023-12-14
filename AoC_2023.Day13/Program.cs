using System.Runtime.CompilerServices;
using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;
using System.Text;

internal class Solution
{
    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static string[] Transpose(string[] input)
    {
        var result = new List<string>();

        for (int j = 0; j < input[0].Length; j++)
        {
            var line = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                line.Append(input[i][j]);
            }

            result.Add(line.ToString());
        }

        return result.ToArray();
    }

    static int GetMirror(string[] input, int compareAgainst)
    {
        for(int i = 1; i < input[0].Length; i++)
        {
            var dict = Enumerable.Range(0, input.Length)
                                 .ToDictionary(k => k, v => true);

            for (int j = 0; j < input.Length; j++)
            {
                var left = new string(input[j][..i]);
                var right = new string(input[j][i..]);

                if (left.Length < right.Length)
                {
                    left = new string(left.Reverse().ToArray());

                    if (right.StartsWith(left) == false)
                    {
                        dict[j]  = false;
                    }
                }
                else
                {
                    right = new string(right.Reverse().ToArray());

                    if (left.EndsWith(right) == false)
                    {
                        dict[j]  = false;
                    }
                }
            }

            var count = dict.Count(x => x.Value);
            
            if (count == compareAgainst)
            {
                return i;
            }
        }

        return 0;
    }

    static IEnumerable<string[]> GetInputBlocks(string[] input)
    {
        var part = new List<string>();

        foreach(var line in input)
        {
            if (line == "")
            {
                yield return part.ToArray();

                part = new List<string>();
                continue;
            }

            part.Add(line);
        }

        yield return part.ToArray();
    }

    static object? solutionPart1(string[] input)
    {
        var result = 0;

        foreach(var block in GetInputBlocks(input))
        {
            result += GetMirror(block, block.Length);
            result += 100 * GetMirror(Transpose(block), block[0].Length);
        }

        return result;
    }

    static object? solutionPart2(string[] input)
    {
        var result = 0;

        foreach(var block in GetInputBlocks(input))
        {
            result += GetMirror(block, block.Length - 1);
            result += 100 * GetMirror(Transpose(block), block[0].Length - 1);
        }

        return result;
    }
}