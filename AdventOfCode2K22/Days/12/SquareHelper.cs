namespace Day12;
public static class SquareHelper
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
    private static int GetElevation(this char c)
    {
        return c == 'E' ? 25 : Alphabet.IndexOf(c);
    }
    public static IEnumerable<Square> GetEligibleSquares(this Square square, Square target, string[] squares)
    {
        int elevation;
        //top
        if (square.Y != 0
            && (elevation = GetElevation(squares[square.Y - 1][square.X])) <= square.Elevation + 1)
            yield return new Square(elevation, square.X, square.Y - 1,target, square);
        //bottom
        if (square.Y != squares.Length - 1
            && (elevation = GetElevation(squares[square.Y + 1][square.X])) <= square.Elevation + 1)
            yield return new Square(elevation, square.X, square.Y + 1, target, square);
        //left
        if (square.X != 0
            && (elevation = GetElevation(squares[square.Y][square.X - 1])) <= square.Elevation + 1)
            yield return new Square(elevation, square.X - 1, square.Y, target, square);
        //right
        if (square.X != squares[0].Length - 1
            && (elevation = GetElevation(squares[square.Y][square.X + 1])) <= square.Elevation + 1)
            yield return new Square(elevation, square.X + 1, square.Y, target, square);
    }
}