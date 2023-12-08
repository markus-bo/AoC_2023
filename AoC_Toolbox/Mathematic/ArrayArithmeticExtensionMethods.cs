using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Toolbox.Mathematic;

public static class ArrayArithmeticExtensionMethods
{
    public static long GetGCD(this IEnumerable<long> array)
    {
        return Arithmetic.GCD(array);
    }

    public static long GetLCM(this IEnumerable<long> array)
    {
        return Arithmetic.LCM(array);
    }
}
