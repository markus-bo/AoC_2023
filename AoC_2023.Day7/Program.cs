using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;

internal class Solution
{
    private class HandComparer : IComparer<string>
    {
        private int GetBaseHandValue(string cards)
        {
            var count = cards.Distinct()
                 .Select(c => (card: c, count: cards.Count(x => x==c)))
                 .OrderByDescending(c => c.count)
                 .ToList();

            if (count.First().count == 5)
            {
                return 7;
            }
            else if (count.First().count == 4)
            {
                return 6;
            }
            else if (count.First().count == 3 && count.Last().count == 2)
            {
                return 5;
            }
            else if (count.First().count == 3 && count.Count == 3)
            {
                return 4;
            }
            else if (count.First().count == 2 && count[1].count == 2 && count.Count == 3)
            {
                return 3;
            }
            else if (count.First().count == 2 && count.Count == 4)
            {
                return 2;
            }
            else if (count.Count == 5)
            {
                return 1;
            }
            else
            {
                throw new Exception($"Unconsidered: {cards}");
            }
        }

        public int Compare(string? x, string? y)
        {
            var a = GetBaseHandValue(x!);
            var b = GetBaseHandValue(y!);

            return a.CompareTo(b);
        }
    }

    private class HandComparerWithJoker : IComparer<string>
    {
        private string GetJokerHand(string cards)
        {
            var count = cards.Distinct()
                 .Select(c => (card: c, count: cards.Count(x => x==c)))
                 .OrderByDescending(c => c.count)
                 .ToList();

            var jokerChar = 'A';
            
            if (count.Any(c => c.card != 'J'))
            {
                jokerChar = count.Where(c => c.card !='J').First().card;
            }
            
            return cards.Replace('J', jokerChar);
        }

        public int Compare(string? x, string? y)
        {
            var x_joker = GetJokerHand(x!);
            var y_joker = GetJokerHand(y!);

            return new HandComparer().Compare(x_joker, y_joker);
        }
    }

    private class CardComparer : IComparer<string>
    {
        public string Order { init; get; }

        public CardComparer(string order)
        {
            Order = order;
        }

        public int Compare(string? x, string? y)
        {
            var a = 0;
            var b = 0;
            var i = 0;

            do{            
                a = Order.IndexOf(x![i].ToString());
                b = Order.IndexOf(y![i].ToString());

                if (a != b)
                {
                    return a.CompareTo(b);
                }

                if (++i == 5)
                {
                    return 0;
                }
            }
            while (a==b);

            throw new Exception("Unconsidered behaviour");
        }
    }

    static void Main(string[] args)
    {
        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static object? solutionPart1(string[] input)
    {
        var result = input.Select(l => l.Split())
             .Select(l => (cards: l[0], bid: int.Parse(l[1])))
             .OrderBy(x => x.cards, new HandComparer())
             .ThenBy(x => x.cards, new CardComparer("23456789TJQKA"))
             .Select((x,i) => (i+1) * x.bid)
             .Sum();

        return result;
    }

    static object? solutionPart2(string[] input)
    {
        var result = input.Select(l => l.Split())
             .Select(l => (cards: l[0], bid: int.Parse(l[1])))
             .OrderBy(x => x.cards, new HandComparerWithJoker())
             .ThenBy(x => x.cards, new CardComparer("J23456789TQKA"))
             .Select((x,i) => (i+1) * x.bid)
             .Sum();

        return result;
    }
}