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
            var bigSplit = line.Split(": ");

            var gameNumber = int.Parse(bigSplit[0].Split()[1]);

            var graps = bigSplit[1].Split("; ").Select(g => g.Split(", "));

            var valid = true;

            var blue = 0;
            var red = 0;
            var green = 0;

            foreach(var grap in graps)
            {
                var blueString = grap.FirstOrDefault(g => g.EndsWith("blue"), "0 0")
                    .Split()[0];

                var redString = grap.FirstOrDefault(g => g.EndsWith("red"), "0 0")
                    .Split()[0];

                var greenString = grap.FirstOrDefault(g => g.EndsWith("green"), "0 0")
                    .Split()[0];

                var blueNumber = int.Parse(blueString);
                var redNumber = int.Parse(redString);
                var greenNumber = int.Parse(greenString);

                if (blueNumber > 14 || greenNumber > 13 || redNumber > 12)
                {
                    valid = false;
                }
            }

            if (valid)
            {
                result += gameNumber;
            }
        }

        return result;
    }

    static object? solutionPart2(string[] input)
    {
        var result = 0;

        foreach (var line in input)
        {
            var bigSplit = line.Split(": ");

            var gameNumber = int.Parse(bigSplit[0].Split()[1]);

            var graps = bigSplit[1].Split("; ").Select(g => g.Split(", "));

            var valid = true;

            var maxBlue = 0;
            var maxRed = 0;
            var maxGreen = 0;

            foreach (var grap in graps)
            {
                var blueString = grap.FirstOrDefault(g => g.EndsWith("blue"), "0 0")
                    .Split()[0];

                var redString = grap.FirstOrDefault(g => g.EndsWith("red"), "0 0")
                    .Split()[0];

                var greenString = grap.FirstOrDefault(g => g.EndsWith("green"), "0 0")
                    .Split()[0];

                var blueNumber = int.Parse(blueString);
                var redNumber = int.Parse(redString);
                var greenNumber = int.Parse(greenString);

                maxBlue = Math.Max(maxBlue, blueNumber);
                maxRed = Math.Max(maxRed, redNumber);
                maxGreen = Math.Max(maxGreen, greenNumber);
            }

            var power = maxBlue * maxRed * maxGreen;

            result += power;
        }

        return result;
    }
}