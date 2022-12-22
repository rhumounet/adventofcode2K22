namespace Day15;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var lines = content.Split("\r\n");
        var noBeacons = BeaconHelper.RegisterBeaconRanges(lines, 2000000);
        return $"{noBeacons.Count(c => c.Item2 == 2000000)}";
    }
}
