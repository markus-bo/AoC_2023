using System.Runtime.CompilerServices;
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
        var result = 0L;

        foreach(var line in input)
        {
            var pattern = line.Split()[0];
            var groups = line.Split()[1].Split(',')
                                        .Select(int.Parse)
                                        .ToArray();

            memoization.Clear();
            
            result += GetAllCombinations(0, 0, 0, pattern, groups);
        }
        
        return result;
    }
    
    static object? solutionPart2(string[] input)
    {
        var result = 0L;

        foreach(var line in input)
        {
            var pattern = line.Split()[0];
            var groups = line.Split()[1].Split(',')
                                        .Select(int.Parse)
                                        .ToArray();

            var unfoldedPattern = string.Join("?", Enumerable.Repeat(pattern, 5));
            var unfoldedGroups = Enumerable.Repeat(groups, 5).SelectMany(x => x);

            memoization.Clear();

            result += GetAllCombinations(0, 0, 0, unfoldedPattern, unfoldedGroups.ToArray());
        }

        return result;
    }

    static Dictionary<(int indexString, int indexGroup, int currentGroupLength), long> memoization = new Dictionary<(int indexString, int indexGroup, int currentGroupLength), long>();


    static long GetAllCombinations(int indexString, int indexGroup, int currentGroupLength, string target, int[] groups)
    {
        if (memoization.ContainsKey((indexString, indexGroup, currentGroupLength)))
        {
            return memoization[(indexString, indexGroup, currentGroupLength)];
        }

        if (indexString == target.Length)
        {
            if (indexGroup == groups.Length && currentGroupLength == 0)
            {
                return 1;
            }
            else if (indexGroup == groups.Length - 1 && currentGroupLength == groups[indexGroup])
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        var permutations = 0L;

        if (target[indexString] == '.' || target[indexString] == '?')
        {
            if (currentGroupLength == 0)
            {
                permutations += GetAllCombinations(indexString + 1, indexGroup, 0, target, groups);
            }
            else if (currentGroupLength > 0 && indexGroup < groups.Length && groups[indexGroup] == currentGroupLength)
            {
                permutations += GetAllCombinations(indexString + 1, indexGroup + 1, 0, target, groups);
            }
        }
        
        if (target[indexString] == '#' || target[indexString] == '?')
        {
            permutations += GetAllCombinations(indexString + 1, indexGroup, currentGroupLength + 1, target, groups);
        }

        memoization.Add((indexString, indexGroup, currentGroupLength), permutations);

        return permutations;
    }
}