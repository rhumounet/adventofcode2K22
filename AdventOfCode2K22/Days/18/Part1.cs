namespace Day18;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var cubes = content.Split("\r\n").Select(s =>
        {
            var split = s.Split(",");
            return (int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        })
        // .ToHashSet()
        // .Order()
        .ToList();
        var countCubes = cubes.Count();
        var faces = countCubes * 6;
        for (int i = 0; i < countCubes - 1; i++)
        {
            for (int j = i + 1; j < countCubes; j++)
            {
                var touches = Touches(cubes[i], cubes[j]);
                if (touches) faces -= 2;
            }
        }
        return $"{faces}";
    }

    internal static bool Touches((int x, int y, int z) c1, (int x, int y, int z) c2)
    {
        return c1.x == c2.x && c1.y == c2.y && Math.Abs(c1.z - c2.z) <= 1
            || c1.x == c2.x && c1.z == c2.z && Math.Abs(c1.y - c2.y) <= 1
            || c1.y == c2.y && c1.z == c2.z && Math.Abs(c1.x - c2.x) <= 1;
    }
}
