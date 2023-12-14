using System.Runtime.CompilerServices;
using System.Text;
using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;
using Microsoft.VisualBasic;

internal class Solution
{
    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static IEnumerable<int> GetPossibleMirrorPositions(string line)
    {

        for (int i=0; i<line.Length-1; i++)
        {
            if (line[i] == line[i+1])
            {
                yield return i;
            }
        }
    }

    static int GetVerticalMirror(string[] map)
    {

        //Console.Error.WriteLine("vertical");
        var possibilities = new Dictionary<int, bool>();

        for(int i=0; i<map[0].Count(); i++)
        {
            possibilities.Add(i, true);
        }

        foreach(var line in map)
        {
            var linePossibilities = GetPossibleMirrorPositions(line).ToList();

            for(int i=0; i<map[0].Count(); i++)
            {
                if (!linePossibilities.Contains(i))
                    possibilities[i] = false;
            }

            //Console.Error.WriteLine($"{line} -- {string.Join(" ", GetPossibleMirrorPositions(line))}");
        }

        //debug
        foreach(var p in possibilities)
        {
            //Console.Error.WriteLine($"{p.Key} {p.Value}");
        }

        if (possibilities.Any(x => x.Value == true)  == false)
        {
            return 0;
        }

        if (possibilities.Count(x => x.Value == true) > 1)
        {
            return 0;
        }

        var indexSplit = possibilities.Where(x => x.Value == true).Select(x => x.Key).First();

        foreach(var line in map)
        {
            var left = new string(line[..(indexSplit+1)]);
            var right = new string(line[(indexSplit+1)..]);


            //Console.Error.WriteLine($"{new string(left)} {new string(right)}");
            if (left.Length < right.Length)
            {
                if (right.StartsWith(new string(left.Reverse().ToArray())) == false)
                {
                    //Console.Error.WriteLine("wrong 1");
                    return 0;
                }
            }
            else
            {
                if (left.EndsWith(new string(right.Reverse().ToArray())) == false)
                {
                    //Console.Error.WriteLine("Wrong 2");
                    return 0;
                }
            }
            
        }

        return indexSplit+1;
    }

    static int GetHoricontalMirror(string[] map)
    {
        //Console.Error.WriteLine($"horicontal {map[0]}");
        var possibilities = new Dictionary<int, bool>();

        for(int i=0; i<map.Length; i++)
        {
            possibilities.Add(i, true);
        }

        for(int j=0; j<map[0].Length; j++)
        {
            var line = new StringBuilder();

            for(int i=0; i<map.Length; i++)
            {
                line.Append(map[i][j]);
            }

            var linePossibilities = GetPossibleMirrorPositions(line.ToString()).ToList();

            for(int i=0; i<map.Length; i++)
            {
                if (!linePossibilities.Contains(i))
                    possibilities[i] = false;
            }

            //Console.Error.WriteLine($"horicontal {line.ToString()} -- {string.Join(" ", GetPossibleMirrorPositions(line.ToString()))}");
        }

        //debug
        foreach(var p in possibilities)
        {
            //Console.Error.WriteLine($"{p.Key} {p.Value}");
        }

        if (possibilities.Any(x => x.Value == true)  == false)
        {
            return 0;
        }

        if (possibilities.Count(x => x.Value == true) > 1 && possibilities.Count(x => x.Value == true) < 5)
        {
            Console.Error.WriteLine($"here");
            return 0;
        }
        
        if (possibilities.Count(x => x.Value == true) > 1)
        {
            return 0;
        }


        return possibilities.Where(x => x.Value == true).Select(x => x.Key).First() + 1;
    }

    static int GetValue(string[] map)
    {
        var a = GetVerticalMirror(map);
        var b = 0;

        if (a==0)
        b  =GetHoricontalMirror(map);

        //Console.Error.WriteLine($"{a} {b}");
        return a + b * 100;
    }

    static object? solutionPart1(string[] input)
    {
        var map = new List<string>();
        var result = 0;
        
        for(int i=0; i<input.Length; i++)
        {
            if (input[i] =="")
            {
                result += GetValue(map.ToArray());
                map.Clear();
            }
            else
            {
                map.Add(input[i]);
            }
        }

        result += GetValue(map.ToArray());
        
        Console.ReadLine();
        return result;
    }

    static object? solutionPart2(string[] input)
    {
        return null;
    }
}