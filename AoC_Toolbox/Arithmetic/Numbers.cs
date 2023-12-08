using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Toolbox
{
    public static class Arithmetic
    {
        public static long LCM(long[] numbers)
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
}
