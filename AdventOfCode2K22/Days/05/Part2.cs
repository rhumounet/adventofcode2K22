using System.Text.RegularExpressions;

namespace Day5;

public class Part2 : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var content = await reader.ReadToEndAsync();

        var fileContent = content.Split("\r\n\r\n");

        // file parsing
        var placementPart = fileContent[0];
        var instructionsPart = fileContent[1];
        var regexLineOfCargo = new Regex(@"(\[\w{1}\]|\s{3})?\s(\[\w{1}\]|\s{3})?\s(\[\w{1}\]|\s{3})?\s(\[\w{1}\]|\s{3})?\s(\[\w{1}\]|\s{3})?\s(\[\w{1}\]|\s{3})?\s(\[\w{1}\]|\s{3})?\s(\[\w{1}\]|\s{3})?\s(\[\w{1}\]|\s{3})?");
        var dictionary = new Dictionary<int, Stack<char>>{
            { 0, new Stack<char>() },
            { 1, new Stack<char>() },
            { 2, new Stack<char>() },
            { 3, new Stack<char>() },
            { 4, new Stack<char>() },
            { 5, new Stack<char>() },
            { 6, new Stack<char>() },
            { 7, new Stack<char>() },
            { 8, new Stack<char>() },
        };
        var placementLines = placementPart.Split("\r\n").Reverse().Skip(1).ToArray();
        for (int i = 0; i < placementLines.Length; i++)
        {
            var match = regexLineOfCargo.Match(placementLines[i]);
            if (match.Success && match.Groups.Count == 10)
            {
                for (int j = 0; j < match.Groups.Count; j++)
                {
                    var cargo = match.Groups[j+1].Value;
                    if (!string.IsNullOrWhiteSpace(cargo))
                        dictionary[j].Push(cargo[1]);
                }
            }
        }

        var instructionRegex = new Regex(@"move (\d*) from (\d)* to (\d)*");
        var instructions = instructionsPart.Split("\r\n");
        var tempStack = new Stack<char>();
        for (int i = 0; i < instructions.Length; i++)
        {
            var regexResult = instructionRegex.Match(instructions[i]);
            var crates = int.Parse(regexResult.Groups[1].Value);
            var from = int.Parse(regexResult.Groups[2].Value);
            var to = int.Parse(regexResult.Groups[3].Value);
            for (int j = 0; j < crates; j++)
            {
                if (dictionary[from - 1].Any()) tempStack.Push(dictionary[from - 1].Pop());
            }
            foreach (var c in tempStack)
            {
                dictionary[to - 1].Push(c);
            }
            tempStack.Clear();
        }

        // final result
        var result = new String(dictionary.Select(d => d.Value.FirstOrDefault()).Select(c => c == '\0' ? ' ' : c).ToArray());

        return result;
    }
}
