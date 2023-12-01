namespace AoC_Toolbox.InputParsing;

public static class ArrayConverterExtensionMethods
{
    public static int[] ToInt32Array(this IEnumerable<string> stringArray)
    {
        return stringArray.Select(int.Parse).ToArray();
    }

    public static long[] ToInt64Array(this IEnumerable<string> stringArray)
    {
        return stringArray.Select(long.Parse).ToArray();
    }

    public static char[,] To2DCharArray(this string[] stringArray)
    {
        var height = stringArray.Length;
        var width = stringArray.First().Length;

        var ouputArray = new char[height, width];

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                ouputArray[i, j] = stringArray[i][j];

        return ouputArray;
    }
}
