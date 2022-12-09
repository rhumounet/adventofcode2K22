namespace Day9;

public class Part2 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var splitted = content.Split("\r\n");
        HashSet<ValueTuple<int, int>>
            tailLocations = new HashSet<ValueTuple<int, int>>();
        ValueTuple<int, int>[] knots = Enumerable.Repeat((0, 0), 10).ToArray();
        tailLocations.Add((0,0));
        for (int i = 0; i < splitted.Length; i++)
        {
            var instruction = splitted[i].Split(' ');
            var direction = instruction[0];
            var speed = int.Parse(instruction[1]);
            for (int j = 0; j < speed; j++)
            {
                switch (direction)
                {
                    case "R":
                        knots[0].Item1++;
                        break;
                    case "L":
                        knots[0].Item1--;
                        break;
                    case "U":
                        knots[0].Item2++;
                        break;
                    case "D":
                        knots[0].Item2--;
                        break;
                }
                for (int k = 1; k < knots.Length; k++)
                {
                    var attemptMove = knots[k].MoveNextTo(knots[k - 1]);
                    if (attemptMove != knots[k])
                        knots[k] = attemptMove;
                    else
                        break;
                    if (k == 9) // tail
                        tailLocations.Add(knots[k]);
                }
            }
        }
        return $"{tailLocations.Count}";
    }
}
