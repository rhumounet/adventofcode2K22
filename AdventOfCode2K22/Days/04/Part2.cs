namespace Day4;

public class Part2 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();

        var pairs = content.Split("\r\n");
        var total = 0;
        foreach (var pair in pairs)
        {
            var elves = pair.Split(',');
            var elf1 = elves[0].Split('-');
            var elf2 = elves[1].Split('-');
            var elf1Range = new int[2] { int.Parse(elf1[0]), int.Parse(elf1[1]) };
            var elf2Range = new int[2] { int.Parse(elf2[0]), int.Parse(elf2[1]) };
            if (elf1Range[1] >= elf2Range[0] && elf1Range[0] <= elf2Range[0]
                || elf2Range[1] >= elf1Range[0] && elf2Range[0] <= elf1Range[0]
                )
            {
                total++;
            }
        }
        return $"{total}";
    }
}
