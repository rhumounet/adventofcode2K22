namespace Day15;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var lines = content.Split("\r\n");
        var noBeacons = new HashSet<(int, int)>();
        var beacons = new HashSet<(int, int)>();
        var yToInspect = 2000000;
        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(": ");
            var sensorAt = parts[0][10..].Split(", ");
            var nearestBeacon = parts[1][21..].Split(", ");

            var sensor = (int.Parse(sensorAt[0][2..]), int.Parse(sensorAt[1][2..]));
            var beacon = (int.Parse(nearestBeacon[0][2..]), int.Parse(nearestBeacon[1][2..]));
            beacons.Add(beacon);
            var distance = sensor.Distance(beacon);
            for (int y = 0; y <= distance; y++)
            {
                if (sensor.Item2 - y == yToInspect)
                {
                    for (int x = 0; x <= distance - y; x++)
                    {
                        noBeacons.Add((sensor.Item1 + x, yToInspect));
                        noBeacons.Add((sensor.Item1 - x, yToInspect));
                    }
                    break;
                }
                
                if (sensor.Item2 + y == yToInspect)
                {
                    for (int x = 0; x <= distance - y; x++)
                    {
                        noBeacons.Add((sensor.Item1 + x, yToInspect));
                        noBeacons.Add((sensor.Item1 - x, yToInspect));
                    }
                    break;
                }
            }
        }
        return $"{noBeacons.Except(beacons).Count(c => c.Item2 == yToInspect)}";
    }
}

public static class ValueTupleHelper
{
    public static int Distance(this (int, int) from, (int, int) to)
    {
        return Math.Abs(from.Item1 - to.Item1) + Math.Abs(from.Item2 - to.Item2);
    }
}