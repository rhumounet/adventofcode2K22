namespace Day17;

public class Part2 : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var jetPattern = await reader.ReadToEndAsync();
        double finalHeight = TetrisPlayer.PlayButBetter(jetPattern, 1000000000000);

        return $"{finalHeight}";
    }
}
