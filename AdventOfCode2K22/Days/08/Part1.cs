namespace Day8;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var splitted = content.Split("\r\n");
        var lengthX = splitted[0].Length;
        var lengthY = splitted.Length;
        var area = new int[lengthX, lengthY];
        var visibleCount = 2 * (lengthX - 1) + 2 * (lengthY - 1);
        bool stillVisible = true, fromXLeft = true, fromXRight = true, fromYTop = true, fromYBottom = true;
        int
            cursorXLeft = 0,
            cursorXRight = 0,
            cursorYTop = 0,
            cursorYBottom = 0;
        for (int x = 0; x < lengthX; x++)
        {
            for (int y = 0; y < lengthY; y++)
            {
                area[x, y] = int.Parse($"{splitted[y][x]}");
            }
        }
        for (int y = 1; y < lengthY - 1; y++)
        {
            for (int x = 1; x < lengthX - 1; x++)
            {
                var treeSize = area[x, y];
                cursorXLeft = x - 1;
                cursorXRight = x + 1;
                cursorYTop = y - 1;
                cursorYBottom = y + 1;
                while (stillVisible
                    && (cursorXLeft >= 0
                    || cursorXRight < lengthX - 1
                    || cursorYTop >= 0
                    || cursorYBottom < lengthY))
                {
                    bool toTheLeft = (fromXLeft && cursorXLeft >= 0 && (fromXLeft = treeSize > area[cursorXLeft, y])),
                        toTheRight = (fromXRight && cursorXRight < lengthX && (fromXRight = treeSize > area[cursorXRight, y])),
                        toTheTop = (fromYTop && cursorYTop >= 0 && (fromYTop = treeSize > area[x, cursorYTop])),
                        toTheBottom = (fromYBottom && cursorYBottom < lengthY && (fromYBottom = treeSize > area[x, cursorYBottom]));

                    stillVisible = toTheLeft
                        || toTheRight
                        || toTheTop
                        || toTheBottom;

                    if (!stillVisible || (fromXLeft && cursorXLeft == 0)
                        || (fromXRight && cursorXRight == lengthX - 1)
                        || (fromYTop && cursorYTop == 0)
                        || (fromYBottom && cursorYBottom == lengthY - 1))
                        break;
                        
                    //move cursors
                    cursorXLeft -= cursorXLeft > 0 ? 1 : 0;
                    cursorXRight += cursorXRight < lengthX - 1 ? 1 : 0;
                    cursorYTop -= cursorYTop > 0 ? 1 : 0;
                    cursorYBottom += cursorYBottom < lengthY - 1 ? 1 : 0;
                }
                visibleCount += (stillVisible ? 1 : 0);
                stillVisible = true;
                fromXLeft = true;
                fromXRight = true;
                fromYTop = true;
                fromYBottom = true;
            }
        }
        return $"{visibleCount}";
    }
}
