namespace Day12;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var splitted = content.Split("\r\n");
        var startPos = splitted
            .Select((string s, int y) => (y, s))
            .FirstOrDefault(vp => vp.s.IndexOf("S") != -1);
        var endPos = splitted
            .Select((string s, int y) => (y, s))
            .FirstOrDefault(vp => vp.s.IndexOf("E") != -1);
        var lengthX = splitted[0].Length;
        var lengthY = splitted.Length;
        var startSquare = new Square(startPos.s.IndexOf("S"), startPos.y);
        var targetSquare = new Square(endPos.s.IndexOf("E"), startPos.y, startSquare);
        startSquare.SetTargetDistance(targetSquare);
        List<Square> actives = new List<Square> {
            startSquare
        };
        for (int x = 0; x < lengthX; x++)
        {
            for (int y = 0; y < lengthY; y++)
            {
                
            }
        }
        return content;
    }
}
