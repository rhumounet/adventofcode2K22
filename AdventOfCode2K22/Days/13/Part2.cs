namespace Day13;

public class Part2 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var lines = content.Split("\r\n\r\n").SelectMany(s => s.Split("\r\n")).ToArray();
        var divider1 = new DynamicValue
        {
            Items = new List<DynamicValue> {
                new DynamicValue { Items = new List<DynamicValue> {
                    new DynamicValue { Items = new List<DynamicValue> {
                        new DynamicValue { Value = 2}
                    }}
                }}
            }
        };
        var divider2 = new DynamicValue
        {
            Items = new List<DynamicValue> {
                new DynamicValue { Items = new List<DynamicValue> {
                    new DynamicValue { Value = 6}
                }}
            }
        };
        var orderedList = new List<DynamicValue> { divider1, divider2 };
        for (int i = 0; i < lines.Length; i++)
        {
            var result = -1;
            var left = DynamicValueHelper.Parse(lines[i]);
            for (int j = 0; j < orderedList.Count; j++)
            {
                result = left.CompareOrder(orderedList[j]);
                if (result == 1)
                {
                    orderedList.Insert(j, left);
                    break;
                }
            }
            if (result == -1) orderedList.Add(left);
        }
        return $"{(orderedList.IndexOf(divider1) + 1) * (orderedList.IndexOf(divider2) + 1)}";
    }
}
