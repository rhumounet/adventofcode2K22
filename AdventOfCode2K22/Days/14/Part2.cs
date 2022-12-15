namespace Day14;

public class Part2 : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var content = await reader.ReadToEndAsync();
        var rockBlocks = content.Split("\r\n");
        HashSet<(int, int)> allRocks = GridHelper.GETALLTHEFUCKINGROCKS(rockBlocks);
        var lengthX = allRocks.Max(r => r.Item1) * 2;
        var lengthY = allRocks.Max(r => r.Item2) + 3;

        char[,] grid = GridHelper.WhereDoesShitGo(allRocks, lengthX, lengthY, 2);
        var total = GridHelper.ItShallRainOnThesePeasants(lengthX, lengthY, grid, 2, out int minX);

        //optional but cute
        GridHelper.PrintThisShit(allRocks, lengthX, lengthY, grid, minX);

        return $"{total}";
    }
}
