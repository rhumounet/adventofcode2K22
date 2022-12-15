namespace Day14;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var rockBlocks = content.Split("\r\n");
        HashSet<(int, int)> allRocks = GridHelper.GETALLTHEFUCKINGROCKS(rockBlocks);
        var lengthX = allRocks.Max(r => r.Item1) + 1;
        var lengthY = allRocks.Max(r => r.Item2) + 1;

        char[,] grid = GridHelper.WhereDoesShitGo(allRocks, lengthX, lengthY, 1);
        var total = GridHelper.ItShallRainOnThesePeasants(lengthX, lengthY, grid, 1);

        //optional but cute
        GridHelper.PrintThisShit(allRocks, lengthX, lengthY, grid);

        return $"{total}";
    }

}
