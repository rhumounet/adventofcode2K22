
using System.Text.RegularExpressions;

internal class Monkey
{
    private string _operator;
    private string _operand;
    private string _denominator;
    public double Denominator { get { return double.Parse(_denominator); } }
    public double Operand { get { return double.Parse(_operand); } }
    private double Compute(double itemValue, double? denominator)
    {
        switch (_operator[0])
        {
            case '+':
                return itemValue = denominator.HasValue ? 
                    (itemValue + Operand) % denominator.Value 
                    : (itemValue + Operand);
            case '*':
                return itemValue = denominator.HasValue ?
                    (itemValue * (_operand == "old" ? itemValue : Operand)) % denominator.Value
                    : (itemValue * (_operand == "old" ? itemValue : Operand));
        }
        return 0;
    }
    public Monkey(string operation, string condition)
    {
        var opRegex = new Regex(@"new = old (\*|\+){1} (old|\d*)");
        var match = opRegex.Match(operation);
        _operator = match.Groups[1].Value;
        _operand = match.Groups[2].Value;

        var conditionRegex = new Regex(@"divisible by (\d*)");
        match = conditionRegex.Match(condition);
        _denominator = match.Groups[1].Value;
    }

    public Queue<double> Items { get; set; } = new Queue<double>();
    public Dictionary<bool, int> Targets { get; set; } = new Dictionary<bool, int>();
    public double ItemsHandled { get; set; } = 0;
    public ValueTuple<int, double> HandleItem(double? commonDenominator = null)
    {
        var item = Items.Dequeue();
        item = Compute(item, commonDenominator);
        ItemsHandled++;
        if (!commonDenominator.HasValue) item = Math.Floor(item / 3);
        return (Targets[item % Denominator == 0], item);
    }
}