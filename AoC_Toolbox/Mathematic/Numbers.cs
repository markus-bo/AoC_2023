using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Toolbox.Mathematic;

public static class Arithmetic
{
    public static long LCM(IEnumerable<long> numbers)
    {
        long lcm = 1L;

        foreach (long number in numbers)
        {
            lcm = LCM(lcm, number);
        }

        return lcm;
    }

    public static long LCM(long a, long b)
    {
        return (a * b) / GCD(a, b);
    }

    public static long GCD(IEnumerable<long> numbers)
    {
        long gcd = numbers.First();

        foreach (long number in numbers.Skip(1))
        {
            gcd = GCD(gcd, number);

            if (gcd == 1)
            {
                break;
            }
        }

        return gcd;
    }


    public static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}
