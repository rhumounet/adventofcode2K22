namespace Day17;

internal class TetrisBlock
{
    private readonly TetrisType _type;

    public TetrisBlock(TetrisType type)
    {
        _type = type;
    }

    public string[] Shape
    {
        get
        {
            return _type switch
            {
                TetrisType.HLine => new[]
                {
                    "####"
                },
                TetrisType.Cross => new[]
                {
                    "\0#\0",
                    "###",
                    "\0#\0",
                },
                TetrisType.L => new[]
                {
                    "\0\0#",
                    "\0\0#",
                    "###",
                },
                TetrisType.VLine => new[]
                {
                    "#",
                    "#",
                    "#",
                    "#",
                },
                TetrisType.Cube => new[]
                {
                    "##",
                    "##",
                },
                _ => new[] { string.Empty }
            };
        }
    }

    public (int x, int y)[] Extremities
    {
        get
        {
            return _type switch
            {
                TetrisType.HLine => new[] { (0, 0), (1, 0), (2, 0), (3, 0) },
                TetrisType.Cross => new[] { (0, 1), (1, 0), (2, 1), (1, 2) },
                TetrisType.L => new[] { (2, 2), (2, 1), (2, 0), (1, 0), (0, 0) },
                TetrisType.VLine => new[] { (0, 0), (0, 1), (0, 2), (0, 3) },
                TetrisType.Cube => new[] { (0, 1), (1, 0), (1, 1), (0, 0) },
                _ => Array.Empty<(int, int)>()
            };
        }
    }
}

internal enum TetrisType
{
    HLine = 0,
    Cross = 1,
    L = 2,
    VLine = 3,
    Cube = 4
}