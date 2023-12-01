using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Threading;

internal class Solution
{
    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static object? solutionPart1(string[] input)
    {
        var result = input.Select(l => (a: l.First(c => char.IsDigit(c)), b: l.Last(c => char.IsDigit(c))))
             .Select(l => (l.a - '0') * 10 + (l.b - '0'))
             .Sum();

        return result;
    }

    static object? solutionPart2(string[] input)
    {
        var pattern = @"(one|two|three|four|five|six|seven|eight|nine|\d{1})";
        var numberStrings = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var sum = 0;

        foreach(var l in input)
        {
            MatchCollection matches = Regex.Matches(l, pattern, RegexOptions.Compiled);

            var first = matches.First().Value;
            var last = matches.Last().Value;

            var a = 0;
            var b = 0;

            if (first.Length == 1)
                a = first[0] - '0';
            else
                a = numberStrings.IndexOf(first);

            if (last.Length == 1)
                b = last[0] - '0';
            else
                b = numberStrings.IndexOf(last);


            sum += a * 10 + b;
        }

        return sum;
    }
}