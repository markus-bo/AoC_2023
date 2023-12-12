using AoC_Toolbox;

internal class Solution
{
    public enum DirectionsEnum
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    static Dictionary<(char, DirectionsEnum), (DirectionsEnum dir, int ymod, int xmod)> PipeTypes = new Dictionary<(char, DirectionsEnum), (DirectionsEnum, int ymod, int xmod)>()
    {
        { ('|', DirectionsEnum.North), (DirectionsEnum.South, 1, 0) },
        { ('|', DirectionsEnum.South), (DirectionsEnum.North, -1, 0) },
        { ('-', DirectionsEnum.West), (DirectionsEnum.East, 0, 1) },
        { ('-', DirectionsEnum.East), (DirectionsEnum.West, 0, -1) },
        { ('L', DirectionsEnum.North), (DirectionsEnum.East, 0, 1) },
        { ('L', DirectionsEnum.East), (DirectionsEnum.North, -1, 0) },
        { ('J', DirectionsEnum.North), (DirectionsEnum.West, 0, -1) },
        { ('J', DirectionsEnum.West), (DirectionsEnum.North, -1, 0) },
        { ('7', DirectionsEnum.West), (DirectionsEnum.South, 1, 0) },
        { ('7', DirectionsEnum.South), (DirectionsEnum.West, 0, -1) },
        { ('F', DirectionsEnum.East), (DirectionsEnum.South, 1, 0) },
        { ('F', DirectionsEnum.South), (DirectionsEnum.East, 0, 1) },
    };

    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static object? solutionPart1(string[] input)
    {
        height = input.Length;
        width = input[0].Length;

        directionMap = input.Select(x => x.ToCharArray())
                    .ToArray();

        path = new char[height][];

        (int y, int x) startPoint = (y: -1, x: -1);

        for (int i=0; i< height; i++)
        {
            var indexOfS = input[i].IndexOf('S');
            
            if (indexOfS > -1)
            {
                startPoint = (y: i, x: indexOfS);
            }

            path[i] = new string('.', width).ToCharArray();
        }

        var result = GetPathLength((y: startPoint.y - 1, x: startPoint.x), DirectionsEnum.South, 0);

        if (result > 0)
        {
            return result / 2;
        }

        for (int i = 0; i < height; i++)
        {
            path[i] = new string('.', width).ToCharArray();
        }

        result = GetPathLength((y: startPoint.y, x: startPoint.x + 1), DirectionsEnum.West, 0);
        
        if (result > 0)
        {
            return result / 2;
        }

        throw new Exception("Path not found");
    }

    static int GetPathLength((int y, int x) start, DirectionsEnum comingFrom, int length)
    {
        var current = start;

        while (true)
        {
            var symbol = directionMap[current.y][current.x];

            path[current.y][current.x] = '#';

            if (symbol == 'S')
            {
                return length + 1;
            }

            if (!PipeTypes.ContainsKey((symbol, comingFrom)))
            {
                return 0;
            }

            var pipe = PipeTypes[(symbol, comingFrom)];

            current = (y: current.y + pipe.ymod, x: current.x + pipe.xmod);
            comingFrom = ReverseDirection(pipe.dir);
            length++;

            if (current == start)
            {
                return length;
            }
        }
    }

    static DirectionsEnum ReverseDirection(DirectionsEnum direction)
    {
        return direction switch
        {
            DirectionsEnum.North => DirectionsEnum.South,
            DirectionsEnum.South => DirectionsEnum.North,
            DirectionsEnum.East => DirectionsEnum.West,
            DirectionsEnum.West => DirectionsEnum.East,
            _ => throw new NotImplementedException()
        };
    }

    static char[][] directionMap;
    static int width, height;
    static char[][] path;

    static object? solutionPart2(string[] input)
    {
        solutionPart1(input);

        var area = 0;

        for (int y = 0; y < height; y++)
        {
            var intersectionCount = 0;
            var fromBelow = false;

            for (int x = 0; x < width; x++)
            {
                if (path[y][(x + 1)..].All(x => x != '#'))
                    break;

                if (path[y][x] != '#' && intersectionCount % 2 == 1)
                {
                    area++;
                }

                if (path[y][x] != '#')
                {
                    continue;
                }

                if (directionMap[y][x] == '|')
                {
                    intersectionCount++;
                }

                if (directionMap[y][x] == 'L')
                {
                    fromBelow = false;
                }

                if (directionMap[y][x] == 'F')
                {
                    fromBelow = true;
                }

                if (directionMap[y][x] == 'J' && fromBelow)
                {
                    intersectionCount++;
                }

                if (directionMap[y][x] == '7' && !fromBelow)
                {
                    intersectionCount++;
                }
            }
        }

        return area;
    }
}