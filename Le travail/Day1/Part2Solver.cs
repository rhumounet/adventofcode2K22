namespace Day1;

public class Part2Solver : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var elves = content.Split("\r\n\r\n");
        var top3 = elves
            .Select(elf => elf.Split("\r\n")
                .Sum(snack => int.Parse(snack)))
            .OrderByDescending(c => c)
            .Take(3);
        return top3.Sum().ToString();
    }
}
