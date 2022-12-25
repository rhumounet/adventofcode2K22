namespace Day17;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var jetPattern = await reader.ReadToEndAsync();
        double finalHeight = TetrisPlayer.Play(jetPattern, 100000);

        return $"{finalHeight}";
    }
}