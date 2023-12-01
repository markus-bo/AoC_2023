using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Toolbox
{
    public class PuzzleSetup
    {
        public static void Solve(Func<string[], object?> solutionPart1, Func<string[], object?> solutionPart2)
        {
            var input0 = GetData($"{Environment.CurrentDirectory}\\Input0.txt");
            var answer0P1 = GetData($"{Environment.CurrentDirectory}\\Answer0_Part1.txt");
            var answer0P2 = GetData($"{Environment.CurrentDirectory}\\Answer0_Part2.txt");
            var input1 = GetData($"{Environment.CurrentDirectory}\\Input1.txt");

            if (input0.Any() == false || input1.Any() == false)
            {
                if (input0.Any() == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FAILED");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($" Input 0 missing");
                }

                if (input1.Any() == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FAILED");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($" Input 1 missing");
                }

                return;
            }

            // do part2 first in case it is already implemented
            if (answer0P2.Any())
            {
                var resultP2input0 = solutionPart2(input0)?.ToString();
                var resultP2input1 = solutionPart2(input1)?.ToString();

                if (answer0P2.First() == resultP2input0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("PASSED");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(" Part 2 example input");
                    Console.WriteLine("");
                    Console.WriteLine("Part 2 custom input result:");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(resultP2input1);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    
                    return;
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FAILED");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($" Part 2 example input [{resultP2input0}]");

                    return;
                }
            }
            else if (answer0P1.First() != "")
            {
                var resultP1input0 = solutionPart1(input0)?.ToString();
                var resultP1input1 = solutionPart1(input1)?.ToString();

                if (answer0P1.First() == resultP1input0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("PASSED");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(" Part 1 example input");
                    Console.WriteLine("");
                    Console.WriteLine("Part 1 custom input result:");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(resultP1input1);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FAILED");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($" Part 1 example input [{resultP1input0}]");

                    return;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Correct answer missing");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private static string[] GetData(string inputPath)
        {
            return new StreamReader(inputPath)
                .ReadToEnd()
                .Split('\n')
                .Select(x => x.Trim('\r'))
                .Reverse()
                .SkipWhile(x => x == "") // remove new lines at end of file
                .Reverse()
                .ToArray();
        }
    }
}
