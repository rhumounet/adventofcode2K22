using System.Text.RegularExpressions;

namespace Day10;

public class Part2 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var instructions = content.Split("\r\n");
        var nooprgx = new Regex("^noop");
        var addxrgx = new Regex(@"^addx (-)?(\d*)");
        var cycles = new Queue<int>();
        int register = 1;
        int lineIndex = 0;
        Match match;
        int i = 0;
        var stringBuilder = new StringBuilder();
        while (cycles.Any() || i <= instructions.Length)
        {
            if (i < instructions.Length)
            {
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
            }
            char toDraw;
            if (lineIndex <= register + 1 && lineIndex >= register - 1)
                toDraw = '#';
            else
                toDraw = '.';
            if (lineIndex == 0)
                stringBuilder.AppendLine();
            stringBuilder.Append(toDraw);
            if (cycles.Any())
                register += cycles.Dequeue();

            i++;
            lineIndex += lineIndex == 39 ? -39 : 1;
        }
        return $"{stringBuilder.ToString()}";
    }
}
