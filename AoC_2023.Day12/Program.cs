using System.Diagnostics;
using System.Runtime.CompilerServices;
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

    static object? solutionPart1(string[] input)
    {
        var stopWatch = new Stopwatch();
        
        stopWatch.Start();
        var result = 0;
        var first = true;
        foreach(var line in input)
        {
            var pattern = line.Split()[0];
            var groups = line.Split()[1].Split(',')
                                        .Select(int.Parse)
                                        .ToArray();

            memoization.Clear();

            var combinations = GetAllCombinations2(0, pattern, "", groups);
            
            /*var combinations = GetAllCombinations(0, 0, pattern, "", groups,
                                                    groups.Sum(x => x),
                                                    pattern.Count(x => x=='#'),
                                                    pattern.Count(x => x=='?'),
                                                    groups.Length - 1,
                                                    pattern.Count(x => x=='.'));*/

            if (first)
            {
                first = false;

                //Console.WriteLine($"{recursionCalls} {validPermutations} {invalidPermutations}");
            }
            result += combinations;

            Console.WriteLine($"  --> {combinations}");
            Console.ReadLine();
        }

         stopWatch.Stop();
        Console.Error.WriteLine(stopWatch.Elapsed);
        return result;
    }

    static Dictionary<string, int> memoization = new Dictionary<string, int>();
    
    static int recursionCalls = 0;
     static int validPermutations = 0;   
     static int invalidPermutations = 0;   
    static int GetAllCombinations(int index, int indexGroup, string target, string current, int[] groups, int neededOnes, int countedOnes, int unassignedQuestionMarks, int neededZeros, int countedZeros)
    {

      /*  if (memoization.ContainsKey(index))
        {
            return memoization[index];
        }*/

            //Console.Error.WriteLine($"{current} {neededOnes} {countedOnes} {unassignedQuestionMarks}");
        recursionCalls++;

        if (countedOnes > neededOnes)
        {
           return 0;
        }

        if (countedZeros + unassignedQuestionMarks < neededZeros)
        {
           //return 0;
        }

        if (countedOnes + unassignedQuestionMarks < neededOnes)
        {
          //return 0;
        }

        if (index == target.Length || indexGroup == groups.Length)
        {
            if (current.Split('.')
                      .Where(x => x != "")
                      .Select(x => x.Length)
                      .SequenceEqual(groups))
                      {
                         validPermutations++;
                         //Console.Error.WriteLine($"{target} {current}");
                         //Console.Error.WriteLine($"  valid");
                        return 1;
                       
                      }
                      else
                      {
                        
                        invalidPermutations++;
                        //Console.Error.WriteLine($"  invalid 1");
                        return 0;
                      }
        }

        

        var combinations = 0;

    
        if (target[index] == '.')
        {
            combinations += GetAllCombinations(index + 1, indexGroup, target, current + target[index], groups, neededOnes, countedOnes, unassignedQuestionMarks, neededZeros, countedZeros);
        }
        else
        {        
            if (target[index] == '?')
            {
                combinations += GetAllCombinations(index + 1, indexGroup, target, current + '.', groups, neededOnes, countedOnes, unassignedQuestionMarks-1, neededZeros, countedZeros+1);
            }

                var questionMarkCount = 0;
                var hashTagCount = 0;

                if (current.Length + groups[indexGroup] > target.Length)
                {
                    //Console.Error.WriteLine($"  invalid 2");
                    //memoization.Add(index, combinations);
                    return combinations;
                }

                for (int i=index; i<index+groups[indexGroup]; i++)
                {
                    if (target[i] == '.')
                    {
                        //Console.Error.WriteLine($"  invalid 3");
                        //memoization.Add(index, combinations);
                        return combinations;
                    }
                    else if (target[i] == '?')
                    {
                        questionMarkCount++;
                        hashTagCount++;
                    }
                    else if (target[i] == '#')
                    {
                        //hashTagCount++;
                    }
                }

                var addZero = false;
                if (indexGroup < groups.Length - 1)
                {
                    if (index+groups[indexGroup] >= target.Length)
                    {
                        //Console.Error.WriteLine($"  invalid 4");
                       // memoization.Add(index, combinations);
                        return combinations;
                    }

                    if (target[index+groups[indexGroup]] == '#')
                    {
                        //Console.Error.WriteLine($"  invalid 5 {current} - {target}");
                       // memoization.Add(index, combinations);
                        return combinations;
                    }
                    else if (target[index+groups[indexGroup]] == '?')
                    {
                        questionMarkCount++;
                    }

                    addZero = true;
                }
            

                combinations += GetAllCombinations(index + groups[indexGroup] + (addZero ? 1 : 0), indexGroup + 1, target, current + new string('#', groups[indexGroup]) + (addZero ? "." : ""), groups, neededOnes, countedOnes + hashTagCount, unassignedQuestionMarks-questionMarkCount, neededZeros, countedZeros+(addZero ? 1 : 0));
                

                //combinations += GetAllCombinations(index + 1, target, current + '#', groups, neededOnes, countedOnes+1, unassignedQuestionMarks-1, neededZeros, countedZeros);
        }

        //memoization.Add((index, current), combinations);

        //memoization.Add(index, combinations);
        return combinations;

    }

    static int GetAllCombinations2(int index, string target, string current, int[] groups)
    {
        if (memoization.ContainsKey(index.ToString()))
        {
            return memoization[index.ToString()];
        }

        if (index >= target.Length)
        {
            if (current.Split('.')
                      .Where(x => x != "")
                      .Select(x => x.Length)
                      .SequenceEqual(groups))
                      {
                        return 1;
                      }
                      else
                      {
                        return 0;
                      }
        }

        

        var combinations = 0;

        if (target[index] == '?')
        {
            combinations += GetAllCombinations2(index + 1, target, current +'#', groups);
            combinations += GetAllCombinations2(index + 1, target, current + '.', groups);
        }
        else
        {
            combinations += GetAllCombinations2(index + 1, target, current + target[index], groups);
        }

        memoization.Add(index.ToString(), combinations);
        return combinations;

    }

    static object? solutionPart2(string[] input)
    {
        var stopWatch = new Stopwatch();
        
        stopWatch.Start();
        var result = 0L;

        var j=0;
        foreach(var line in input)
        {
            Console.Error.WriteLine(j++);
            var pattern = line.Split()[0];
            var groups = line.Split()[1].Split(',')
                                        .Select(int.Parse)
                                        .ToArray();

            var unfoldedPattern = "";
            var unfoldedGroups = new List<int>();

            for (int i=0; i<5; i++)
            {
                unfoldedPattern += pattern + "?";


                unfoldedGroups.AddRange(groups);
            }

            unfoldedPattern = new string(unfoldedPattern[..^1]);

            //Console.Error.WriteLine($"{unfoldedPattern} {string.Join(',', unfoldedGroups)}");

            memoization.Clear();

            var combinations = (long)GetAllCombinations2(0, unfoldedPattern, "", unfoldedGroups.ToArray());

           Console.Error.WriteLine($"{pattern} {combinations}");

           // Console.ReadLine();
            result += combinations;
        }

        stopWatch.Stop();
        Console.Error.WriteLine(stopWatch.Elapsed);
        Console.Error.WriteLine($"{result}");
        Console.ReadLine();
        
        return result;
    }
}