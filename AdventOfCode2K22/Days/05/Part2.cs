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
        var regex = new Regex(@"\[(.{1})\]");
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
            var currentLine = placementLines[i];
            var arrangementIndex = 0;
            var lineIndex = 0;
            var cargoArrangement = new Dictionary<int, string>();
            while (lineIndex <= currentLine.Length)
            {
                cargoArrangement.Add(arrangementIndex, currentLine.Substring(lineIndex, 3));
                lineIndex += 4;
                arrangementIndex++;
            }
            for (int j = 0; j < 9; j++)
            {
                var match = regex.Match(cargoArrangement[j]);
                if (match.Success)
                {
                    var value = match.Groups[1].Value[0];
                    dictionary[j].Push(match.Groups[1].Value[0]);
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
