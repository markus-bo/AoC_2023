using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;


internal class Rule
{
    public string CategoryToCompare { get; init; }

    public string Operator { get; init; }

    public int Comparer { get; init; }

    public string IfValidGoTo { get; init; }

    public Rule(string rule)
    {
        var split = rule.Split(':');
        CategoryToCompare  = split[0][0].ToString();
        Operator = split[0][1].ToString();
        Comparer = int.Parse(new string(split[0][2..]));
        IfValidGoTo = split[1];
    }

    public (bool valid, string next) Compare(Dictionary<string, int> rating)
    {
        if (Operator == "<")
        {
            if (rating[CategoryToCompare] < Comparer)
            {
                return (true, IfValidGoTo);    
            }
            else
            {
                return (false, "");
            }
        }
        else if (Operator == ">")
        {
            if (rating[CategoryToCompare] > Comparer)
            {
                return (true, IfValidGoTo);    
            }
            else
            {
                return (false, "");
            }
        }
        else
        {
            throw new NotImplementedException("Unknown operator");
        }
    }

    public override string ToString()
    {
        return $"{this.CategoryToCompare}{this.Operator}{this.Comparer}:{this.IfValidGoTo}";
    }
}

internal class Solution
{
    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static object? solutionPart1(string[] input)
    {
       var workflows = new Dictionary<string, (List<Rule> ruleset, string defaultPath)>();
       var ratings = new List<Dictionary<string, int>>();

       foreach(var line in input.TakeWhile(x => x != ""))
       {
            var split = line.Split('{');
            var name = split[0];
            var rules = split[1].TrimEnd('}').Split(',');

            workflows.Add(name, (rules.Take(rules.Length - 1).Select(x => new Rule(x)).ToList(), rules.Last()));
       }

       foreach(var line in input.SkipWhile(x => x != "").Skip(1))
       {
            var split = line.TrimStart('{').TrimEnd('}').Split(',')
                            .ToDictionary(k => k.Split('=')[0], v => int.Parse(v.Split('=')[1]));
            ratings.Add(split);
       }

        var result = 0;
       
       foreach(var rating in ratings)
       {

            Console.Error.WriteLine(string.Join(", ", rating.Select(x => $"{x.Key}:{x.Value}")));

            var currentWorkflow = "in";
            
            while(currentWorkflow != "A" && currentWorkflow != "R")
            {
               // Console.Error.WriteLine($"  {currentWorkflow}");

                var useDefault = true;


                foreach(var rule in workflows[currentWorkflow].ruleset)
                {
                    var appliedRule = rule.Compare(rating);

                    if (appliedRule.valid == true)
                    {
                     //   Console.Error.WriteLine($"    found valid {rule.IfValidGoTo}");
                        currentWorkflow = appliedRule.next;
                        useDefault = false;
                        break;
                    }
                }
                
                if (useDefault == true)
                {
                   // Console.Error.WriteLine($"    apply default");

                    currentWorkflow = workflows[currentWorkflow].defaultPath;
                }

            //Console.ReadLine();
            }

            if (currentWorkflow == "A")
            {
                result += rating.Select(x => x.Value).Sum();
            }
       }

        Console.Error.WriteLine(result);
        //Console.ReadLine();
       return result;
    }
    
    static object? solutionPart2(string[] input)
    {
       var workflows = new Dictionary<string, (List<Rule> ruleset, string defaultPath)>();
       var ratings = new List<Dictionary<string, int>>();

       foreach(var line in input.TakeWhile(x => x != ""))
       {
            var split = line.Split('{');
            var name = split[0];
            var rules = split[1].TrimEnd('}').Split(',');

            workflows.Add(name, (rules.Take(rules.Length - 1).Select(x => new Rule(x)).ToList(), rules.Last()));
       }

       foreach(var line in input.SkipWhile(x => x != "").Skip(1))
       {
            var split = line.TrimStart('{').TrimEnd('}').Split(',')
                            .ToDictionary(k => k.Split('=')[0], v => int.Parse(v.Split('=')[1]));
            ratings.Add(split);
       }

       var listX = new List<(int low, int high)>();
       var current = 1;

       foreach(var workflow in workflows.Where(x => x.Value.ruleset.Any(y => y.CategoryToCompare == "x"))
                                        .SelectMany(x => x.Value.ruleset.Where(y => y.CategoryToCompare =="x"))
                                        .OrderBy(x => x.Comparer))
       {
            if (workflow.Operator == ">")
            {
                listX.Add((current, workflow.Comparer));
                current = workflow.Comparer + 1;
            }
            else if (workflow.Operator == "<")
            {
                listX.Add((current, workflow.Comparer - 1));
                current = workflow.Comparer;
            }
       }

       if (listX.Last().high != 4000)
       {
            listX.Add((current, 4000));
       }

       var listM = new List<(int low, int high)>();
       current = 1;

       foreach(var workflow in workflows.Where(x => x.Value.ruleset.Any(y => y.CategoryToCompare == "m"))
                                        .SelectMany(x => x.Value.ruleset.Where(y => y.CategoryToCompare =="m"))
                                        .OrderBy(x => x.Comparer))
       {
            if (workflow.Operator == ">")
            {
                listM.Add((current, workflow.Comparer));
                current = workflow.Comparer + 1;
            }
            else if (workflow.Operator == "<")
            {
                listM.Add((current, workflow.Comparer - 1));
                current = workflow.Comparer;
            }
       }

       if (listM.Last().high != 4000)
       {
            listM.Add((current, 4000));
       }

       var listA = new List<(int low, int high)>();
       current = 1;

       foreach(var workflow in workflows.Where(x => x.Value.ruleset.Any(y => y.CategoryToCompare == "a"))
                                        .SelectMany(x => x.Value.ruleset.Where(y => y.CategoryToCompare =="a"))
                                        .OrderBy(x => x.Comparer))
       {
            if (workflow.Operator == ">")
            {
                listA.Add((current, workflow.Comparer));
                current = workflow.Comparer + 1;
            }
            else if (workflow.Operator == "<")
            {
                listA.Add((current, workflow.Comparer - 1));
                current = workflow.Comparer;
            }
       }

       if (listA.Last().high != 4000)
       {
            listA.Add((current, 4000));
       }

       var listS = new List<(int low, int high)>();
       current = 1;

       foreach(var workflow in workflows.Where(x => x.Value.ruleset.Any(y => y.CategoryToCompare == "s"))
                                        .SelectMany(x => x.Value.ruleset.Where(y => y.CategoryToCompare =="s"))
                                        .OrderBy(x => x.Comparer))
       {
            if (workflow.Operator == ">")
            {
                listS.Add((current, workflow.Comparer));
                current = workflow.Comparer + 1;
            }
            else if (workflow.Operator == "<")
            {
                listS.Add((current, workflow.Comparer - 1));
                current = workflow.Comparer;
            }
       }

       if (listS.Last().high != 4000)
       {
            listS.Add((current, 4000));
       }


        //Console.Error.WriteLine($"X: {string.Join(", ", listX)}");
        //Console.Error.WriteLine($"M: {string.Join(", ", listM)}");
        //Console.Error.WriteLine($"A: {string.Join(", ", listA)}");
        //Console.Error.WriteLine($"S: {string.Join(", ", listS)}");

        Console.WriteLine($"Ranges {listX.Count} {listM.Count} {listA.Count} {listS.Count}");
        // compose ranges
        var composedRanges = new Dictionary<int, (int lowX, int highX, int lowM, int highM, int lowA, int highA, int lowS, int highS)>();
        var index = 0;

        for (int x = 0; x < listX.Count; x++)
        {
            for (int m = 0; m < listM.Count; m++)
            {
                for (int a = 0; a < listA.Count; a++)
                {
                    for (int s = 0; s < listS.Count; s++)
                    {
                        composedRanges.Add(index, (listX[x].low, listX[x].high, listM[m].low, listM[m].high,
                                                   listA[a].low, listA[a].high, listS[s].low, listS[s].high));

                        index++;
                    }
                }
            }
        }

        Console.WriteLine($"Ranges {composedRanges.Count}");

        Console.ReadLine();

        var result = 0;
       
       foreach(var rating in ratings)
       {

            Console.Error.WriteLine(string.Join(", ", rating.Select(x => $"{x.Key}:{x.Value}")));

            var currentWorkflow = "in";
            
            while(currentWorkflow != "A" && currentWorkflow != "R")
            {
               // Console.Error.WriteLine($"  {currentWorkflow}");

                var useDefault = true;


                foreach(var rule in workflows[currentWorkflow].ruleset)
                {
                    var appliedRule = rule.Compare(rating);

                    if (appliedRule.valid == true)
                    {
                     //   Console.Error.WriteLine($"    found valid {rule.IfValidGoTo}");
                        currentWorkflow = appliedRule.next;
                        useDefault = false;
                        break;
                    }
                }
                
                if (useDefault == true)
                {
                   // Console.Error.WriteLine($"    apply default");

                    currentWorkflow = workflows[currentWorkflow].defaultPath;
                }

            //Console.ReadLine();
            }

            if (currentWorkflow == "A")
            {
                result += rating.Select(x => x.Value).Sum();
            }
       }

        Console.Error.WriteLine(result);
        //Console.ReadLine();
       return result;
    }
}