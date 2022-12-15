public class DynamicValue
{
    public List<DynamicValue> Items { get; set; } = new List<DynamicValue>();
    public int? Value { get; set; }
}

public static class DynamicValueHelper
{
    public static DynamicValue Parse(string input)
    {
        var currents = new Stack<DynamicValue>();
        var baseArray = new DynamicValue();
        currents.Push(baseArray);
        var currentArray = currents.Peek();
        for (int i = 1; i < input.Length - 1; i++)
        {
            if (input[i] == '[')
            {
                var tempArray = new DynamicValue();
                currentArray.Items.Add(tempArray);
                currents.Push(tempArray);
                currentArray = currents.Peek();
            }
            if (int.TryParse(input[i].ToString(), out int value))
            {
                if (int.TryParse($"{input[i + 1]}", out int zero))
                {
                    currentArray.Items.Add(new DynamicValue { Value = 10 });
                    i++;
                }
                else
                {
                    currentArray.Items.Add(new DynamicValue { Value = value });
                }
            }
            if (input[i] == ']')
            {
                currents.Pop();
                currentArray = currents.Peek();
            }
        }
        return baseArray;
    }

    public static int CompareOrder(this DynamicValue left, DynamicValue right)
    {
        int value = 0;
        if (left.Value.HasValue && right.Value.HasValue)
        {
            int? diff = left.Value - right.Value;
            switch (diff)
            {
                case < 0: return 1;
                case > 0: return -1;
                default: return 0;
            }
        }
        else if (!left.Value.HasValue && !right.Value.HasValue)
        {
            for (var i = 0; i < left.Items.Count; i++)
            {
                if (right.Items.Count <= i) return -1;
                value = CompareOrder(left.Items[i], right.Items[i]);
                if (value != 0) return value;
            }
            return left.Items.Count == right.Items.Count ? 0 : 1;
        }
        else if (left.Value.HasValue)
        {
            var surround = new DynamicValue();
            surround.Items.Add(left);
            return CompareOrder(surround, right);
        }
        else
        {
            var surround = new DynamicValue();
            surround.Items.Add(right);
            return CompareOrder(left, surround);
        }
    }
}