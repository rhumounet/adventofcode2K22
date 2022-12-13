using System.Runtime.CompilerServices;

public class Square
{
    public int X { get; }
    public int Y { get; }
    public int Elevation { get; }
    public int G { get; set; }
    public int H { get; private set; }
    public int F => G + H;
    public Square? Parent { get; set; }

    public Square(int elevation, int x, int y, Square? target = null, Square? parent = null)
    {
        Elevation = elevation;
        X = x;
        Y = y;
        H = target != null ? Math.Abs(target.X - X) + Math.Abs(target.Y - Y) : 0;
        Parent = parent;
    }

    public void SetTargetDistance(Square target)
    {
        H = Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
    }
}