namespace Day2;

public class Part2 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var totalScore = 0;
        var plays = content.Split("\r\n");
        var scorer = (char a, char b) =>
        {
            if (a == 'A')
            {
                if (b == 'X') return 0 + 3;
                if (b == 'Y') return 3 + 1;
                if (b == 'Z') return 6 + 2;
            }
            else if (a == 'B')
            {
                if (b == 'X') return 0 + 1;
                if (b == 'Y') return 3 + 2;
                if (b == 'Z') return 6 + 3;
            }
            else
            {
                if (b == 'X') return 0 + 2;
                if (b == 'Y') return 3 + 3;
                if (b == 'Z') return 6 + 1;
            }
            return 0;
        };
        foreach (var play in plays)
        {
            var opponent = play[0];
            var you = play[2];
            totalScore += scorer(opponent, you);
        }
        return totalScore.ToString();
    }
}