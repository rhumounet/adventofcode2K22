using System.Text.RegularExpressions;

namespace Day10;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var instructions = content.Split("\r\n");
        var nooprgx = new Regex("^noop");
        var addxrgx = new Regex(@"^addx (-)?(\d*)");
        var cycles = new Queue<int>();
        var list = new List<int>();
        int register = 1;
        Match match;
        int i = 0;
        while (cycles.Any() || i <= instructions.Length)
        {
            if (i < instructions.Length)
                if ((match = nooprgx.Match(instructions[i])).Success)
                {
                    cycles.Enqueue(0);
                    // noop
                }
                else if ((match = addxrgx.Match(instructions[i])).Success)
                {
                    cycles.Enqueue(0);
                    cycles.Enqueue(int.Parse($"{match.Groups[1].Value}{match.Groups[2].Value}"));
                }
            if ((i + 1) % 40 == 20)
                list.Add((i + 1) * register);
            if (cycles.Any())
                register += cycles.Dequeue();
            i++;
        }
        return $"{list.Sum()}";
    }
}
