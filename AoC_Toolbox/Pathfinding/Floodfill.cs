namespace AoC_Toolbox.Pathfinding;

public class Floodfill<T> where T : IComparable<T>
{
    public T[,] FillArray(T[,] map, T fillValue, (int x, int y) startPoint, Func<T, T, bool> isFieldValid, bool allowDiagonalFill)
    {
        return fillArray(map, fillValue, startPoint, isFieldValid, allowDiagonalFill);
    }

    private T[,] fillArray(T[,] map, T fillValue, (int x, int y) startPoint, Func<T, T, bool> isFieldValid, bool allowDiagonalFill)
    {
        // Initialize queue with start element
        var queue = new Queue<(int x, int y)>();
        queue.Enqueue(startPoint);

        var width = map.GetLength(0);
        var height = map.GetLength(1);
        

        var exploredStates = new List<(int x, int y)>();

        var clonedMap = (T[,])map.Clone();

        // Dequeue as long as there is something
        while (queue.TryDequeue(out (int x, int y) current))
        {
            // Memorize the explored node
            if (exploredStates.Contains(current))
                continue;

            exploredStates.Add(current);

            clonedMap[current.x, current.y] = fillValue;

            /* Fields:
             * 
             *    1 2 3  
             *    4 X 5
             *    6 7 8
             */

            if (current.y > 0)
            {
                if (isFieldValid(map[current.x, current.y], map[current.x, current.y - 1])) // field above (2)
                    queue.Enqueue((current.x, current.y - 1));

                if (allowDiagonalFill && current.x > 0)
                    if (isFieldValid(map[current.x, current.y], map[current.x - 1, current.y - 1])) // field above to the left (1)
                        queue.Enqueue((current.x - 1, current.y - 1));

                if (allowDiagonalFill && current.x < width - 1)
                    if (isFieldValid(map[current.x, current.y], map[current.x + 1, current.y - 1])) // field above to the right (3)
                        queue.Enqueue((current.x + 1, current.y - 1));
            }

            if (current.y < height - 1)
            {
                if (isFieldValid(map[current.x, current.y], map[current.x, current.y + 1])) // field below (7)
                    queue.Enqueue((current.x, current.y + 1));

                if (allowDiagonalFill && current.x > 0)
                    if (isFieldValid(map[current.x, current.y], map[current.x - 1, current.y + 1])) // field below to the left (6)
                        queue.Enqueue((current.x - 1, current.y + 1));

                if (allowDiagonalFill && current.x < width - 1)
                    if (isFieldValid(map[current.x, current.y], map[current.x + 1, current.y + 1])) // field below to the right (8)
                        queue.Enqueue((current.x + 1, current.y + 1));
            }

            if (current.x > 0)
                if (isFieldValid(map[current.x, current.y], map[current.x - 1, current.y])) // field to the left (4)
                    queue.Enqueue((current.x - 1, current.y));

            if (current.x < width - 1)
                if (isFieldValid(map[current.x, current.y], map[current.x + 1, current.y])) // field to the right (5)
                    queue.Enqueue((current.x + 1, current.y));
        }

        return clonedMap;
    }
}
