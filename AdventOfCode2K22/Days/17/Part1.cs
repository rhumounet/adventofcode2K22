using System.Reflection.Metadata.Ecma335;

namespace Day17;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var jetPattern = await reader.ReadToEndAsync();
        var blocks = new[]
        {
            new TetrisBlock(TetrisType.HLine),
            new TetrisBlock(TetrisType.Cross),
            new TetrisBlock(TetrisType.L),
            new TetrisBlock(TetrisType.VLine),
            new TetrisBlock(TetrisType.Cube),
        };
        var blockCount = 0;
        var blockIndex = 0;
        const int yDelta = 3, xDelta = 2, maxWidth = 7;
        var final = new char[maxWidth, 9000];
        var cursor = (x: xDelta, y: yDelta);
        var patternIndex = 0;
        while (blockCount < 2023)
        {
            var hitRockBottom = false;
            var currentRock = blocks[blockIndex];
            var movementCount = 0;
            while (!hitRockBottom)
            {
                char currentMovement;
                if (movementCount++ % 2 == 0)
                {
                    currentMovement = jetPattern[patternIndex];
                    patternIndex = ++patternIndex % jetPattern.Length;
                }
                else
                {
                    currentMovement = 'v';
                }
                var canMove = true;
                switch (currentMovement)
                {
                    case '>':
                        if (cursor.x + currentRock.Shape[0].Length >= maxWidth) continue;
                        var itLeft = 0;
                        for (var y = currentRock.Shape.Length - 1; y >= 0; y--)
                        {
                            var currentLine = currentRock.Shape[y];
                            var current = currentLine.Last();
                            var target = final[
                                cursor.x + currentLine.Length,
                                cursor.y + itLeft++];
                            if (target == '\0' || current == '\0') continue;
                            canMove = false;
                            break;
                        }
                        for (var x = 0; x < currentRock.Shape.Last().Length; x++)
                        {
                            var currentLine = currentRock.Shape.Last();
                            var current = currentLine[x];
                            var target = final[
                                cursor.x + currentLine.Length,
                                cursor.y];
                            if (target == '\0' || current == '\0') continue;
                            canMove = false;
                            break;
                        }

                        if (canMove)
                        {
                            cursor.x++;
                        }

                        break;
                    case '<':
                        if (cursor.x - 1 < 0) continue;
                        var itRight = 0;
                        for (var y = currentRock.Shape.Length - 1; y >= 0; y--)
                        {
                            var currentLine = currentRock.Shape[y];
                            var current = currentLine.First();
                            var target = final[
                                cursor.x - 1,
                                cursor.y + itRight++];
                            if (target == '\0' || current == '\0') continue;
                            canMove = false;
                            break;
                        }
                        for (var x = 0; x < currentRock.Shape.Last().Length; x++)
                        {
                            var currentLine = currentRock.Shape.Last();
                            var current = currentLine[x];
                            var target = final[
                                cursor.x - 1,
                                cursor.y];
                            if (target == '\0' || current == '\0') continue;
                            canMove = false;
                            break;
                        }

                        if (canMove)
                        {
                            cursor.x--;
                        }

                        break;
                    case 'v':
                        if (cursor.y - 1 < 0)
                        {
                            hitRockBottom = true;
                            continue;
                        }

                        for (var x = 0; x < currentRock.Shape[0].Length; x++)
                        {
                            var current = currentRock.Shape.Last()[x];
                            var target = final[
                                cursor.x + x,
                                cursor.y - 1];
                            if (target == '\0' || current == '\0') continue;
                            canMove = false;
                            break;
                        }

                        if (canMove)
                        {
                            cursor.y--;
                        }
                        else
                        {
                            hitRockBottom = true;
                        }

                        break;
                }
            }

            var invertedIterator = 0;
            for (var y = currentRock.Shape.Length - 1; y >= 0; y--)
            {
                for (var x = 0; x < currentRock.Shape[y].Length; x++)
                {
                    if (final[cursor.x + x, cursor.y + invertedIterator] == '\0')
                        final[cursor.x + x, cursor.y + invertedIterator] = currentRock.Shape[y][x];
                }

                invertedIterator++;
            }

            var currentHeight = 0;
            while (true)
            {
                var anyBlock = false;
                for (var i = 0; i < maxWidth; i++)
                {
                    if (final[i, currentHeight] == '#') anyBlock = true;
                }

                if (!anyBlock) break;
                currentHeight++;
            }

            //reset cursor
            cursor = (x: xDelta, y: currentHeight + yDelta);
            
            Console.WriteLine(blockCount);
            Print(final, maxWidth);

            blockIndex = ++blockIndex % blocks.Length;
            blockCount++;
        }

        //calculate height one last time
        var finalHeight = 0;
        while (true)
        {
            var anyBlock = false;
            for (var i = 0; i < maxWidth; i++)
            {
                if (final[i, finalHeight] == '#') anyBlock = true;
            }

            if (!anyBlock) break;
            finalHeight++;
        }

        return $"{finalHeight}";
    }

    private static void Print(char[,] toPrint, int maxWidth)
    {
        var height = 0;
        var temp = new StringBuilder();
        var result = new StringBuilder();
        while (true)
        {
            var anyBlock = false;
            temp.AppendLine();
            temp.Append('|');
            for (var i = 0; i < maxWidth; i++)
            {
                temp.Append(toPrint[i, height]);
                if (toPrint[i, height] == '#') anyBlock = true;
            }
            temp.Append('|');
            if (!anyBlock) break;
            height++;
        }

        temp.AppendLine();
        foreach (var line in temp.ToString().Split("\r\n").Reverse())
        {
            result.AppendLine(line.Replace('\0', ' '));
        }
        Console.WriteLine(result.ToString());
    }
}

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
}

internal enum TetrisType
{
    HLine = 0,
    Cross = 1,
    L = 2,
    VLine = 3,
    Cube = 4
}