internal static class TupleHelper
{
    internal static bool IsAdjacent(this ValueTuple<int, int> behind, ValueTuple<int, int> ahead, out ValueTuple<int, int> diff)
    {
        int diffX = ahead.Item1 - behind.Item1, diffY = ahead.Item2 - behind.Item2;
        diff = (diffX, diffY);
        return Math.Abs(ahead.Item1 - behind.Item1) <= 1
            && Math.Abs(ahead.Item2 - behind.Item2) <= 1;
    }

    internal static ValueTuple<int, int> MoveNextTo(this ValueTuple<int, int> behind, ValueTuple<int, int> ahead)
    {
        ValueTuple<int, int> diff;
        if (!behind.IsAdjacent(ahead, out diff))
        {
            //horizontally
            // . . . . .
            // H . T . H
            // . . . . .
            if (behind.Item1 == ahead.Item1 - 2 && behind.Item2 == ahead.Item2)
                return (behind.Item1 + 1, behind.Item2);
            if (behind.Item1 == ahead.Item1 + 2 && behind.Item2 == ahead.Item2)
                return (behind.Item1 - 1, behind.Item2);

            //vertically
            // . H .
            // . . .
            // . T .
            // . . .
            // . H .
            if (behind.Item1 == ahead.Item1 && behind.Item2 == ahead.Item2 - 2)
                return (behind.Item1, behind.Item2 + 1);
            if (behind.Item1 == ahead.Item1 && behind.Item2 == ahead.Item2 + 2)
                return (behind.Item1, behind.Item2 - 1);

            //diagonals
            // H H . H H
            // H . . . H
            // . . T . .
            // H . . . H 
            // H H . H H
            if (behind.Item1 == ahead.Item1 - 2 && behind.Item2 == ahead.Item2 - 1)
                return (behind.Item1 + 1, behind.Item2 + 1);
            if (behind.Item1 == ahead.Item1 - 1 && behind.Item2 == ahead.Item2 - 2)
                return (behind.Item1 + 1, behind.Item2 + 1);
            if (behind.Item1 == ahead.Item1 - 2 && behind.Item2 == ahead.Item2 + 1)
                return (behind.Item1 + 1, behind.Item2 - 1);
            if (behind.Item1 == ahead.Item1 - 1 && behind.Item2 == ahead.Item2 + 2)
                return (behind.Item1 + 1, behind.Item2 - 1);

            if (behind.Item1 == ahead.Item1 + 2 && behind.Item2 == ahead.Item2 + 1)
                return (behind.Item1 - 1, behind.Item2 - 1);
            if (behind.Item1 == ahead.Item1 + 1 && behind.Item2 == ahead.Item2 + 2)
                return (behind.Item1 - 1, behind.Item2 - 1);
            if (behind.Item1 == ahead.Item1 + 2 && behind.Item2 == ahead.Item2 - 1)
                return (behind.Item1 - 1, behind.Item2 + 1);
            if (behind.Item1 == ahead.Item1 + 1 && behind.Item2 == ahead.Item2 - 2)
                return (behind.Item1 - 1, behind.Item2 + 1);

            if (behind.Item1 == ahead.Item1 + 2 && behind.Item2 == ahead.Item2 + 2)
                return (behind.Item1 - 1, behind.Item2 - 1);
            if (behind.Item1 == ahead.Item1 + 2 && behind.Item2 == ahead.Item2 - 2)
                return (behind.Item1 - 1, behind.Item2 + 1);
            if (behind.Item1 == ahead.Item1 - 2 && behind.Item2 == ahead.Item2 - 2)
                return (behind.Item1 + 1, behind.Item2 + 1);
            if (behind.Item1 == ahead.Item1 - 2 && behind.Item2 == ahead.Item2 + 2)
                return (behind.Item1 + 1, behind.Item2 - 1);

        }
        return behind;
    }
}