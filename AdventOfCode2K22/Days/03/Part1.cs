namespace Day3;

public class Part1 : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var content = await reader.ReadToEndAsync();
        var rucksacks = content.Split("\r\n");
        var getPriority = (char c) => {
            return alphabet.IndexOf(c) + 1;
        };
        var compartments = rucksacks.Select(r => r.Chunk(r.Length / 2));
        var totalPriorities = compartments
            .Sum(c => getPriority(c.ElementAt(0)
                .Intersect(c.ElementAt(1)).ElementAt(0)));

        return totalPriorities.ToString();
    }
}
