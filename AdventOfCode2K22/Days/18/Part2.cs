namespace Day18;

public class Part2 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var cubes = content.Split("\r\n").Select<string, (int x, int y, int z)>(s =>
        {
            var split = s.Split(",");
            return (int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }).ToList();
        var minX = cubes.Min(c => c.x);
        var maxX = cubes.Max(c => c.x);
        var minY = cubes.Min(c => c.y);
        var maxY = cubes.Max(c => c.y);
        var minZ = cubes.Min(c => c.z);
        var maxZ = cubes.Max(c => c.z);
        var allCubes = new HashSet<(int x, int y, int z)>();
        var negatives = new HashSet<(int x, int y, int z)>();
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    allCubes.Add((x, y, z));
                    if (!cubes.Contains((x, y, z)))
                        negatives.Add((x, y, z));
                }
            }
        }
        var countCubes = cubes.Count();
        var faces = countCubes * 6;
        for (int i = 0; i < countCubes - 1; i++)
        {
            for (int j = i + 1; j < countCubes; j++)
            {
                var touches = cubes[i].Touches(cubes[j]);
                if (touches) faces -= 2;
            }
        }
        var keepDigging = true;
        var trapped = new List<List<(int x, int y, int z)>>();
        while (keepDigging)
        {
            var relativeQueue = new Queue<(int x, int y, int z)>();
            var currentGroup = new HashSet<(int x, int y, int z)>();
            relativeQueue.Enqueue(negatives.FirstOrDefault());
            while (relativeQueue.Any())
            {
                var current = relativeQueue.Dequeue();
                currentGroup.Add(current);
                var relatives = current.GetRelatives(negatives);
                foreach (var relative in relatives)
                {
                    relativeQueue.Enqueue(relative);
                }
                currentGroup = currentGroup.Concat(relatives).ToHashSet();
                negatives = negatives
                    .Except(currentGroup)
                    .ToHashSet();
            }
            if (!currentGroup.Any(r => r.x == minX || r.y == minY || r.z == minZ || r.x == maxX || r.y == maxY || r.z == maxZ))
            {
                trapped.Add(currentGroup.ToList());
            }
            if (!negatives.Any())
                keepDigging = false;
        }
        var negativeFaces = trapped.Sum(t => t.Count * 6);
        foreach (var trappedCollection in trapped)
        {
            for (int i = 0; i < trappedCollection.Count - 1; i++)
            {
                for (int j = i + 1; j < trappedCollection.Count; j++)
                {
                    var touches = trappedCollection[i].Touches(trappedCollection[j]);
                    if (touches) negativeFaces -= 2;
                }
            }
        }
        return $"{(faces - negativeFaces)}";
    }
}
