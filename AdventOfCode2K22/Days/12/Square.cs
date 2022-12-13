public class Square
{
    public int X { get; set; }
    public int Y { get; set; }
    public int FromStart { get; }
    public int ToTarget { get; private set; }
    public Square(int x, int y, Square? start = null, Square? target = null)
    {
        X = x;
        Y = y;
        FromStart = start != null ? Math.Abs(start.X - X) + Math.Abs(start.Y - Y) : 0;
        ToTarget = target != null ? Math.Abs(target.X - X) + Math.Abs(target.Y - Y) : 0;
    }
    public void SetTargetDistance(Square target)
    {
        ToTarget = Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
    }
}