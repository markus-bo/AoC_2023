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
            var values = line.Split()
                 .ToInt64Array()
                 .ToList();

            result += Extrapolate(values);
        }

        return result;
    }

    static object? solutionPart2(string[] input)
    {
        var result = 0L;

        foreach (var line in input)
        {
            var values = line.Split()
                 .ToInt64Array()
                 .ToList();

            values.Reverse();

            result += Extrapolate(values);
        }

        return result;
    }


    static IEnumerable<long> GetDifferences(List<long> values)
    {
        for (int i = 1; i < values.Count(); i++)
        {
            yield return values[i] - values[i - 1];
        }
    }

    static long Extrapolate(List<long> values)
    {
        var differencesHistory = new List<List<long>>() { values };

        while (differencesHistory.Last().Any(x => x != 0))
        {
            var differences = GetDifferences(differencesHistory.Last())
                                .ToList();

            differencesHistory.Add(differences);
        }

        differencesHistory.Reverse();

        return differencesHistory.Sum(x => x.Last());
    }
}