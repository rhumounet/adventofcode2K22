public class DynamicValue
{
    public List<DynamicValue> Items { get; set; } = new List<DynamicValue>();
    public int? Value { get; set; }
}

public static class DynamicValueHelper
{
    public static List<DynamicValue> Parse(string input)
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
                currentArray.Items.Add(new DynamicValue { Value = value });
            }
            if (input[i] == ']')
            {
                currents.Pop();
                currentArray = currents.Peek();
            }
        }
        return baseArray.Items;
    }

    public static int CompareOrder(this List<DynamicValue> left, List<DynamicValue> right)
    {
        if (left.Count == 0) return 1;
        if (right.Count == 0) return -1;
        for (var i = 0; i < left.Count; i++)
        {
            if (right.Count <= i) return 1;
            if (left[i].Value.HasValue && right[i].Value.HasValue)
            {
                if (left[i].Value > right[i].Value) return -1;
                if (left[i].Value < right[i].Value) return 1;
                return 0;
            }
            int value;
            if (left[i].Value.HasValue)
            {
                var surround = new DynamicValue();
                surround.Items.Add(left[i]);
                if ((value = CompareOrder(surround.Items, right[i].Items)) != 0) return value;
            }
            else if (right[i].Value.HasValue)
            {
                var surround = new DynamicValue();
                surround.Items.Add(right[i]);
                if ((value = CompareOrder(left[i].Items, surround.Items)) != 0) return value;
            }
            else if (!left[i].Value.HasValue && !right[i].Value.HasValue)
            {
                if ((value = CompareOrder(left[i].Items, right[i].Items)) != 0) return value;
            }
        }
        return left.Count < right.Count ? 1 : -1;
    }
}