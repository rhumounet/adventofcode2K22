namespace Day12;

public class Part2 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var squares = content.Split("\r\n");
        var startPos = squares
            .Select((string s, int y) => (y, s))
            .FirstOrDefault(vp => vp.s.IndexOf("S", StringComparison.Ordinal) != -1);
        var endPos = squares
            .Select((string s, int y) => (y, s))
            .FirstOrDefault(vp => vp.s.IndexOf("E", StringComparison.Ordinal) != -1);
        var startSquare = new Square(0, startPos.s.IndexOf("S", StringComparison.Ordinal), startPos.y);
        var targetSquare = new Square(25, endPos.s.IndexOf("E", StringComparison.Ordinal), endPos.y, startSquare);
        startSquare.SetTargetDistance(targetSquare);
        var startingSquares = new List<Square>();
        var potentialWinners = new List<Square>();
        for (int i = 0; i < squares.Length; i++)
        {
            for (int j = 0; j < squares[0].Length; j++)
            {
                if (squares[i][j] == 'a')
                    startingSquares.Add(new Square(
                        0, j, i, targetSquare
                    ));
            }
        }
        foreach (var item in startingSquares)
        {
            var actives = new List<Square> { item };
            var visited = new List<Square>();
            Square? current = null;
            int g = 0;
            while (actives.Any())
            {
                current = actives.OrderBy(x => x.F).First();
                if (current.X == targetSquare.X && current.Y == targetSquare.Y)
                {
                    potentialWinners.Add(current);
                    break;
                }

                visited.Add(current);
                actives.Remove(current);

                var possibleSquares = current.GetEligibleSquares(targetSquare, squares);
                g++;
                foreach (var possible in possibleSquares)
                {
                    if (visited.Any(v => v.X == possible.X && v.Y == possible.Y))
                        continue;

                    var existing = actives.FirstOrDefault(a => a.X == possible.X && a.Y == possible.Y);
                    if (existing == null)
                    {
                        possible.G = g;
                        actives.Add(possible);
                    }
                    else if (g + possible.H < existing.F)
                    {
                        existing.G = g;
                        existing.Parent = current;
                    }
                }
            }
        }

        var hash = new HashSet<int>();
        // var print = Enumerable.Repeat(new string(Enumerable.Repeat('.', squares[0].Length).ToArray()), squares.Length).ToArray();
        foreach (var item in potentialWinners)
        {
            var copy = item;
            var steps = -1;
            while (copy != null)
            {
                steps++;
                // print[copy.Y] = print[copy.Y].Remove(copy.X, 1).Insert(copy.X, "@");
                copy = copy.Parent;
            }
            hash.Add(steps);
        }

        // Console.WriteLine(string.Join("\r\n", hash.Min()));
        return $"{hash.Min()}";
    }
}
