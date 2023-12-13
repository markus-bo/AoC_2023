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
        var result = 0;

        foreach(var line in input)
        {
            var pattern = line.Split()[0];
            var groups = line.Split()[1].Split(',')
                                        .Select(int.Parse)
                                        .ToArray();

            memoization.Clear();
            
            var combinations = GetAllCombinations(0, pattern, "", groups);

            result += combinations;
        }

        return result;
    }

    static Dictionary<(int, string), int> memoization = new Dictionary<(int, string), int>();

    static int GetAllCombinations(int index, string target, string current, int[] groups)
    {
        if (memoization.ContainsKey((index, current)))
        {
            return memoization[(index, current)];
        }

        if (index == target.Length)
        {
            if (current.Split('.')
                      .Where(x => x != "")
                      .Select(x => x.Length)
                      .SequenceEqual(groups))
                      {
                        return 1;
                      }
                      else
                      {
                        return 0;
                      }
        }

        var combinations = 0;

        if (target[index] == '?')
        {
            combinations += GetAllCombinations(index + 1, target, current + '#', groups);
            combinations += GetAllCombinations(index + 1, target, current + '.', groups);
        }
        else
        {
            combinations += GetAllCombinations(index + 1, target, current + target[index], groups);
        }

        memoization.Add((index, current), combinations);

        return combinations;

    }
    static object? solutionPart2(string[] input)
    {
        var result = 0;

        foreach(var line in input)
        {
            var pattern = line.Split()[0];
            var groups = line.Split()[1].Split(',')
                                        .Select(int.Parse)
                                        .ToArray();

            var unfoldedPattern = "";
            var unfoldedGroups = new List<int>();

            for (int i=0; i<5; i++)
            {
                unfoldedPattern += pattern + "?";

                unfoldedGroups.AddRange(groups);
            }

            //Console.Error.WriteLine($"{unfoldedPattern} {string.Join(',', unfoldedGroups)}");

            var combinations = GetAllCombinations(0, unfoldedPattern, "", unfoldedGroups.ToArray());

            Console.Error.WriteLine($"{pattern} {combinations}");

            result += combinations;
        }

        Console.ReadLine();
        
        return result;
    }
}