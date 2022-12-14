namespace Day11;

public class Part2 : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var content = await reader.ReadToEndAsync();
        var monkeyStrings = content.Split("\r\n\r\n");
        var monkeys = new Dictionary<int, Monkey>();
        for (int i = 0; i < monkeyStrings.Length; i++)
        {
            var props = monkeyStrings[i].Split("\r\n");
            monkeys.Add(i, new Monkey(props[2].Substring(13), props[3].Substring(8))
            {
                Items = new Queue<double>(props[1]
                    .Substring(18)
                    .Split(',')
                    .Select(item => double.Parse(item))),
                Targets = new Dictionary<bool, int>{
                    { true, int.Parse($"{props[4].Last()}") },
                    { false, int.Parse($"{props[5].Last()}") },
                }
            });
        }
        var commonDenominator = monkeys.Select(m => m.Value.Denominator).Aggregate((x, y) => x * y);
        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Value.Items.Any())
                {
                    var handled = monkey.Value.HandleItem(commonDenominator);
                    monkeys[handled.Item1].Items.Enqueue(handled.Item2);
                }
            }
        }
        return $"{monkeys
            .Select(m => m.Value.ItemsHandled)
            .OrderByDescending(m => m)
            .Take(2)
            .Aggregate((x, y) => x * y)}";
    }
}
