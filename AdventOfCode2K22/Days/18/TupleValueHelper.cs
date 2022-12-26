namespace Day18;

public static class TupleValueHelper
{
    public static HashSet<(int x, int y, int z)> GetRelatives(
        this (int x, int y, int z) c,
        IEnumerable<(int x, int y, int z)> possible)
    {
        return possible
            .Where(r => Math.Abs(r.x - c.x) <= 1 && Math.Abs(r.y - c.y) <= 1 && Math.Abs(r.z - c.z) <= 1)
            .Where(r => r.Touches(c))
            .ToHashSet();
    }

    public static bool Touches(this (int x, int y, int z) c1, (int x, int y, int z) c2)
    {
        return !(c1.x == c2.x && c1.y == c2.y && c1.z == c2.z) &&
            (c1.x == c2.x && c1.y == c2.y && Math.Abs(c1.z - c2.z) <= 1
            || c1.x == c2.x && c1.z == c2.z && Math.Abs(c1.y - c2.y) <= 1
            || c1.y == c2.y && c1.z == c2.z && Math.Abs(c1.x - c2.x) <= 1);
    }
}