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

    static char[][] map;

    static void RollNorth()
    {
        for (int x = 0; x < map[0].Length; x++)
        {
            var fallIndex = 0;

            for (int y = 0; y < map.Length; y++)
            {
                if (map[y][x] == '#')
                {
                    fallIndex = y+1;
                }
                else if (map[y][x] == 'O' && fallIndex == y)
                {
                    fallIndex += 1;
                }
                else if (map[y][x] == 'O')
                {
                    map[fallIndex][x] = 'O';
                    map[y][x] = '.';
                    fallIndex += 1;
                }
            }
        }
    }

    static void RollSouth()
    {
        for (int x = 0; x < map[0].Length; x++)
        {
            var fallIndex = map.Length - 1;

            for (int y = map.Length - 1; y >= 0; y--)
            {
                if (map[y][x] == '#')
                {
                    fallIndex = y-1;
                }
                else if (map[y][x] == 'O' && fallIndex == y)
                {
                    fallIndex -= 1;
                }
                else if (map[y][x] == 'O')
                {
                    map[fallIndex][x] = 'O';
                    map[y][x] = '.';
                    fallIndex -= 1;
                }
            }
        }
    }

    static void RollWest()
    {
        for (int y = 0; y < map.Length; y++)
        {
            var fallIndex = 0;

            for (int x = 0; x < map[0].Length; x++)
            {
                
            
                if (map[y][x] == '#')
                {
                    fallIndex = x+1;
                }
                else if (map[y][x] == 'O' && fallIndex == x)
                {
                    fallIndex += 1;
                }
                else if (map[y][x] == 'O')
                {
                    map[y][fallIndex] = 'O';
                    map[y][x] = '.';
                    fallIndex += 1;
                }
            }
        }
    }

    static void RollEast()
    {
        for (int y = 0; y < map.Length; y++)
        {
            var fallIndex = map[0].Length - 1;

            for (int x = map[0].Length - 1; x >= 0; x--)
            {
                
            
                if (map[y][x] == '#')
                {
                    fallIndex = x-1;
                }
                else if (map[y][x] == 'O' && fallIndex == x)
                {
                    fallIndex -= 1;
                }
                else if (map[y][x] == 'O')
                {
                    map[y][fallIndex] = 'O';
                    map[y][x] = '.';
                    fallIndex -= 1;
                }
            }
        }
    }

    static object? solutionPart1(string[] input)
    {
        map = input.Select(x => x.ToCharArray())
                   .ToArray();

        RollNorth();
        
        return GetLoad();
    }

    static int GetLoad()
    {
        var result = 0;

        for(int i=0; i < map.Length; i++)
        {
            var roundRocks = map[i].Count(x => x == 'O');
            result += roundRocks * (map.Length - i);
        }

        return result;
    }

    static Dictionary<string, int> dict = new Dictionary<string, int>();

    static object? solutionPart2(string[] input)
    {
        map = input.Select(x => x.ToCharArray())
                   .ToArray();

        for (int i = 0; i < 1000000000; i++)
        {
            RollNorth();
            RollWest();
            RollSouth();
            RollEast();

            var key = string.Join("", map.Select(x => new string(x)));

            if (dict.ContainsKey(key))
            {
                i = 1000000000 - ((1000000000 - dict[key]) % (i - (dict[key])));
            }
            else
            {
                dict.Add(key, i);
            }
        }

        return GetLoad();
    }
}