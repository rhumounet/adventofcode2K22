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

    public static bool CompareOrder(this List<DynamicValue> left, List<DynamicValue> right)
    {
        if (left.Count == 0) return true;
        if (right.Count == 0) return false;

        for (int i = 0; i < left.Count; i++)
        {
            if (right.Count <= i) return false;
            if (left[i].Value.HasValue && right[i].Value.HasValue)
            {
                if (left[i].Value > right[i].Value) return false;
                else if (left[i].Value < right[i].Value) return true;
                else continue;
            }
            else if (left[i].Value.HasValue)
            {
                var surround = new DynamicValue();
                surround.Items.Add(left[i]);
                if (CompareOrder(surround.Items, right[i].Items)) continue;
                else if (i == left.Count - 1) return false;
            }
            else if (right[i].Value.HasValue)
            {
                var surround = new DynamicValue();
                surround.Items.Add(right[i]);
                if (CompareOrder(left[i].Items, surround.Items)) continue;
                else if (i == left.Count - 1) return false;
            }
            else if (!left[i].Value.HasValue && !right[i].Value.HasValue)
            {
                if (CompareOrder(left[i].Items, right[i].Items)) continue;
                else if (i == left.Count - 1) return false;
            }
        }
        return left.Count < right.Count;
    }
}