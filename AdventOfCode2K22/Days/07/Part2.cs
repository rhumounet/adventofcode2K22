namespace Day7;

public class Part2 : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var content = await reader.ReadToEndAsync();
        var commandLines = content.Split("\r\n");
        var root = Helper.CreateTree(commandLines);
        var totalSize = 70000000;
        var targetSize = 30000000;
        var actualSize = root.Size;
        var availableSize = totalSize - actualSize;
        var targetDirSize = targetSize - availableSize;
        var node = root.GetMinimumTreeSize(targetDirSize).MinBy(n => n.Size);
        return $"{node?.Size ?? 0}";
    }
}
