namespace Day6;

public class Part1 : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var content = await reader.ReadToEndAsync();
        return $"{Helper.Trace(content, 4)}";
    }
}
