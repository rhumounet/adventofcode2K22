namespace Day15;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var lines = content.Split("\r\n");
        var noBeacons = RegisterBeaconRanges(lines, 2000000);
        return $"{noBeacons.Count(c => c.Item2 == 2000000)}";
    }

    private static HashSet<(int, int)> RegisterBeaconRanges(string[] lines, int? yToInspect)
    {
        var noBeacons = new HashSet<(int, int)>();
        var beacons = new HashSet<(int, int)>();
        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(": ");
            var sensorAt = parts[0][10..].Split(", ");
            var nearestBeacon = parts[1][21..].Split(", ");

            var sensor = (int.Parse(sensorAt[0][2..]), int.Parse(sensorAt[1][2..]));
            var beacon = (int.Parse(nearestBeacon[0][2..]), int.Parse(nearestBeacon[1][2..]));
            beacons.Add(beacon);
            var distance = sensor.Distance(beacon);
            if (yToInspect.HasValue)
            {
                for (int y = 0; y <= distance; y++)
                {
                    if (sensor.Item2 - y == yToInspect.Value)
                    {
                        for (int x = 0; x <= distance - y; x++)
                        {
                            noBeacons.Add((sensor.Item1 + x, yToInspect.Value));
                            noBeacons.Add((sensor.Item1 - x, yToInspect.Value));
                        }
                        break;
                    }
                    if (sensor.Item2 + y == yToInspect.Value)
                    {
                        for (int x = 0; x <= distance - y; x++)
                        {
                            noBeacons.Add((sensor.Item1 + x, yToInspect.Value));
                            noBeacons.Add((sensor.Item1 - x, yToInspect.Value));
                        }
                        break;
                    }
                }
            }
            else
            {
                
            }
        }
        return noBeacons.Except(beacons).ToHashSet();
    }
}

public static class ValueTupleHelper
{
    public static int Distance(this (int, int) from, (int, int) to)
    {
        return Math.Abs(from.Item1 - to.Item1) + Math.Abs(from.Item2 - to.Item2);
    }
}