using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;
using System.Diagnostics.Metrics;
using System.Transactions;

internal class Solution
{
    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static object? solutionPart1(string[] input)
    {
        var directions = input[0];
        var map = new Dictionary<string, (string left, string right)>();

        foreach(var line in input.Skip(2))
        {
            var bigSplit = line.Split(" = ");
            var source = bigSplit[0];
            var direction = bigSplit[1].Split(", ");
            var left = direction[0].Trim('(');
            var right = direction[1].Trim(')');

            map.Add(source, (left, right));
        }

        var index = 0;
        var current = "AAA";
        var steps = 0;

        while(true)
        {
            if (directions[index] == 'L')
            {
                current = map[current].left;
            }
            else if (directions[index] == 'R')
            {
                current = map[current].right;
            }
            else
            {
                throw new NotImplementedException();
            }
                
            steps++;

            if (current == "ZZZ")
            {
                break;
            }

            if (++index >= directions.Length)
            {
                index = 0;
            }

        }

        return steps;
    }

    static object? solutionPart2(string[] input)
    {
        var directions = input[0];
        var map = new Dictionary<string, (string left, string right)>();

        foreach (var line in input.Skip(2))
        {
            var bigSplit = line.Split(" = ");
            var source = bigSplit[0];
            var direction = bigSplit[1].Split(", ");
            var left = direction[0].Trim('(');
            var right = direction[1].Trim(')');

            map.Add(source, (left, right));
        }

        var index = 0;
        var currents = map.Where(x => x.Key.EndsWith('A'))
                         .Select(x => x.Key)
                         .ToArray();

        var counter = new long[currents.Length];

        var steps = 0;

        while (true)
        {
            var zEnding = 0;

            steps++;

            for(int i = 0; i < currents.Length; i++)
            {   
                if (directions[index] == 'L')
                {
                    currents[i] = map[currents[i]].left;
                }
                else if (directions[index] == 'R')
                {
                    currents[i] = map[currents[i]].right;
                }
                else
                {
                    throw new NotImplementedException();
                }

                if (currents[i][^1] == 'Z')
                {
                    zEnding++;
                    counter[i] = steps-counter[i];
                }
            }

            if (counter.All(x => x!=0))
            {
                return AoC_Toolbox.Arithmetic.LCM(counter);
            }

            if (currents.Length == zEnding)
            {
                break;
            }

            if (++index >= directions.Length)
            {
                index = 0;
            }
        }

        return steps;
    }
}