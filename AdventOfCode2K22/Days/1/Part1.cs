namespace Day1;
public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var elves = content.Split("\r\n\r\n");
        
        return elves.Max(elf => elf.Split("\r\n").Sum(snack => int.Parse(snack))).ToString();
    }
}
