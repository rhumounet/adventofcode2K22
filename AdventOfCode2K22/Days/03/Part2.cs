namespace Day3;

public class Part2 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var content = await reader.ReadToEndAsync();
        var i = 0;
        var rucksacks = content.Split("\r\n").Aggregate((a, b) =>
        {
            return $"{a}{(i++ != 0 && i % 3 == 0 ? "@" : "+")}{b}";
        });
        var getPriority = (char c) =>
        {
            return alphabet.IndexOf(c) + 1;
        };
        var totalPriorities = rucksacks.Split('@').Sum(g =>
        {
            var grouped = g.Split('+');
            return getPriority(grouped.ElementAt(0)
                .Intersect(grouped.ElementAt(1))
                .Intersect(grouped.ElementAt(2))
                .ElementAt(0));
        });

        return totalPriorities.ToString();
    }
}
