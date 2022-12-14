namespace Day13;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var pairs = content.Split("\r\n\r\n");
        var orderedIndexes = new HashSet<int>();
        for (int i = 0; i < pairs.Length; i++)
        {
            var LR = pairs[i].Split("\r\n");
            var left = DynamicValueHelper.Parse(LR[0]);
            var right = DynamicValueHelper.Parse(LR[1]);
            if (left.CompareOrder(right) == 1) orderedIndexes.Add(i + 1);
        }
        return $"{orderedIndexes.Sum()}";
    }
}
