namespace Day15;

public static class BeaconHelper
{
    public static HashSet<(int, int)> RegisterBeaconRanges(IEnumerable<string> lines, int yToInspect)
    {
        var noBeacons = new HashSet<(int, int)>();
        var beacons = new HashSet<(int, int)>();
        foreach (var line in lines)
        {
            var parts = line.Split(": ");
            var sensorAt = parts[0][10..].Split(", ");
            var nearestBeacon = parts[1][21..].Split(", ");
            var covered = new Dictionary<int, List<(int, Range)>>();

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

        return noBeacons.Except(beacons).ToHashSet();
    }

    public static (int, int) GetLonelyPoint(IEnumerable<string> lines, int yToInspect)
    {
        var noBeacons = new HashSet<(int, int)>();
        var beacons = new HashSet<(int, int)>();
        var covered = new Dictionary<int, List<(int, Range)>>();
        for (int i = 0; i < 10; i++)
        {
            covered.Add(i, new List<(int, Range)>());
        }

        foreach (var line in lines)
        {
            var parts = line.Split(": ");
            var sensorAt = parts[0][10..].Split(", ");
            var nearestBeacon = parts[1][21..].Split(", ");
            var sensor = (int.Parse(sensorAt[0][2..]), int.Parse(sensorAt[1][2..]));
            var beacon = (int.Parse(nearestBeacon[0][2..]), int.Parse(nearestBeacon[1][2..]));
            beacons.Add(beacon);
            var distance = sensor.Distance(beacon);
            if (sensor is { Item1: < 4000000, Item2: < 4000000 })
            {
                for (int y = 0; y <= distance; y++)
                {
                    if (sensor.Item2 + y > 0 && sensor.Item2 + y < 4000000)
                    {
                        covered[GetDictIndex(sensor.Item2 + y)]
                            .Add(
                                (
                                    sensor.Item2 + y,
                                    new Range(sensor.Item1 - (distance - y), sensor.Item1 + (distance - y))
                                )
                            );
                    }

                    if (sensor.Item2 - y > 0 && sensor.Item2 - y < 4000000)
                    {
                        covered[GetDictIndex(sensor.Item2 - y)]
                            .Add(
                                (
                                    sensor.Item2 - y,
                                    new Range(sensor.Item1 - (distance - y), sensor.Item1 + (distance - y))
                                )
                            );
                    }
                }
            }
        }

        return (0, 0);
        //
        // for (int i = 0; i < covered.Count; i++)
        // {
        //     for (int j = 0; j < 500000; j++)
        //     {
        //         if (covered[i])
        //     }
        // }
        //
        // return noBeacons.Except(beacons).ToHashSet();
    }

    private static int GetDictIndex(int value)
    {
        return value switch
        {
            int _ when (value < 0) => -1,
            int _ when (value <= 500000) => 0,
            int _ when value is > 500000 and <= 1000000 => 1,
            int _ when value is > 1000000 and <= 1500000 => 2,
            int _ when value is > 1000000 and <= 1500000 => 3,
            int _ when value is > 1500000 and <= 2000000 => 4,
            int _ when value is > 2000000 and <= 2500000 => 5,
            int _ when value is > 2500000 and <= 3000000 => 6,
            int _ when value is > 3000000 and <= 3500000 => 7,
            int _ when value is > 3500000 and <= 4000000 => 8,
            int _ when value is > 4000000 and <= 4500000 => 9,
            _ => 10
        };
    }
}

public static class ValueTupleHelper
{
    public static int Distance(this (int, int) from, (int, int) to)
    {
        return Math.Abs(from.Item1 - to.Item1) + Math.Abs(from.Item2 - to.Item2);
    }
}