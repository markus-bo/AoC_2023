using System;
using System.Collections.Generic;
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

        foreach(var element in input.First().Split(','))
        {
            result += hash(element);
        }

        return result;
    }

    static int hash(string input)
    {
        var current = 0;

        foreach (var c in input)
        {
            var ascii = (int)c;

            current += ascii;
            current *= 17;
            current %= 256;
        }

        return current;
    }

    static object? solutionPart2(string[] input)
    {
        var boxes = new List<(string label, int focal)>[256];

        for(int i=0; i<256; i++)
        {
            boxes[i] = new List<(string label, int focal)>();
        }

        foreach(var element in input.First().Split(','))
        {
            var separator = Math.Max(element.IndexOf('-'), element.IndexOf('='));

            var label = new string(element[..separator]);
            var boxId = hash(label);
            var op = element[separator];
            var focal = 0;

            if (op == '=')
            {
                focal = element[separator + 1] - '0';

                if (boxes[boxId].Any(b => b.label == label))
                {
                    var indexToReplace = boxes[boxId].FindIndex(b => b.label == label);

                    boxes[boxId][indexToReplace] = (label, focal);
                }
                else 
                {
                    boxes[boxId].Add((label, focal));
                }
            }
            else if (op == '-')
            {
                if (boxes[boxId].Any(b => b.label == label))
                {
                    var indexToRemove = boxes[boxId].FindIndex(b => b.label == label);

                    boxes[boxId].RemoveAt(indexToRemove);
                }
            }
            else
            {
                throw new NotImplementedException("unknown op");
            }
        }

        var result = 0L;

        for(int i=0; i<256; i++)
        {
            for (int j = 0; j < boxes[i].Count; j++)
            {
                result += (i + 1) * (long)(j + 1) * boxes[i][j].focal;
            }
        }
        return result;
    }
}