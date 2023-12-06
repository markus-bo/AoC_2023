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
        var times = input[0].Split().Where(x => x != "").Skip(1).Select(int.Parse).ToList();
        var distances = input[1].Split().Where(x => x != "").Skip(1).Select(int.Parse).ToList();

        var result = 1;

        foreach(var race in times.Zip(distances))
        {
            var recordExceeded = 0;

            for (int i=0; i<race.First; i++)
            {
                var speed = i; // mm/ms
                var remainingTime = race.First - i;

                var travelDistance = speed * remainingTime;

                if (travelDistance > race.Second)
                {
                    recordExceeded++;
                }
            }

            result *= recordExceeded;
        }

        return result;
    }

    static object? solutionPart2(string[] input)
    {
        var time = long.Parse(input[0].Replace(" ", "").Split(":")[1]);
        var distances = long.Parse(input[1].Replace(" ", "").Split(":")[1]);

        var result = 1;

        var recordExceeded = 0;

        for (long i = 0; i < time; i++)
        {
            var speed = i; // mm/ms
            var remainingTime = time - i;

            var travelDistance = speed * remainingTime;

            if (travelDistance > distances)
            {
                recordExceeded++;
            }
        }

        result *= recordExceeded;

        return result;
    }
}