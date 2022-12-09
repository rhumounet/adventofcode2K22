namespace Day9;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var splitted = content.Split("\r\n");
        HashSet<ValueTuple<int, int>> 
            tailLocations = new HashSet<ValueTuple<int, int>>();
        ValueTuple<int, int> currentHead = (0, 0), currentTail = (0, 0);
        tailLocations.Add(currentTail);
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
                        currentHead.Item1++;
                        break;
                    case "L":
                        currentHead.Item1--;
                        break;
                    case "U":
                        currentHead.Item2++;
                        break;
                    case "D":
                        currentHead.Item2--;
                        break;
                }
                currentTail = currentTail.MoveNextTo(currentHead);
                tailLocations.Add(currentTail);
            }
        }
        return $"{tailLocations.Count}";
    }
}
