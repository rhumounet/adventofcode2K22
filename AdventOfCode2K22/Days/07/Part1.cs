using System.Text.RegularExpressions;

namespace Day7;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var commandLines = content.Split("\r\n");
        var root = Helper.CreateTree(commandLines);
        var nodes = root.GetMaximumSizeNodes(100000);
        return $"{nodes.Sum(n => n.Size)}";
    }
}
