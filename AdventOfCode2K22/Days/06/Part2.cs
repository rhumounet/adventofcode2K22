namespace Day6;

public class Part2 : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var content = await reader.ReadToEndAsync();
        //Fais des trucs batard
        return $"{Helper.Trace(content, 14)}";
    }
}
