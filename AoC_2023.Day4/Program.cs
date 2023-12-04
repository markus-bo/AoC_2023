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
            var bigSplit = line.Split(": ");

            var data = bigSplit[1].Split(" | ")
                                  .Select(x => x.Split()
                                                .Where(y => y != "")
                                                .Select(int.Parse)
                                                .ToList())
                                  .ToList();

            var matches = data[1].Count(x => data[0].Contains(x));

            var points = (int)Math.Pow(2, matches - 1);

            result += points;
        }

        return result;
    }

    static object? solutionPart2(string[] input)
    {
        var result = 0L;

        var punchCardMatches = new Dictionary<int, int>();
        var punchCardCount = new Dictionary<int, int>();

        foreach (var line in input)
        {
            var bigSplit = line.Split(": ");

            var data = bigSplit[1].Split(" | ")
                                  .Select(x => x.Split()
                                                .Where(y => y != "")
                                                .Select(int.Parse)
                                                .ToList())
                                  .ToList();

            var matches = data[1].Count(x => data[0].Contains(x));
            var cardNr = int.Parse(bigSplit[0].Split()[^1]);

            punchCardMatches.Add(cardNr, matches);
            punchCardCount.Add(cardNr, 1);
        }

        foreach(var match in punchCardMatches)
        {
            for (int j = 1; j <= match.Value; j++)
            {
                punchCardCount[match.Key + j] += punchCardCount[match.Key];
            }
        }

        result = punchCardCount.Values
                               .Sum();

        return result;
    }
}