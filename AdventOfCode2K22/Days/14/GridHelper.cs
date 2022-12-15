public static class GridHelper
{
    public static void PrintThisShit(HashSet<(int, int)> allRocks, int lengthX, int lengthY, char[,] grid)
    {
        var minX = 0;
        var minY = 0;
        var stringBuilder = new StringBuilder();
        for (int y = minY; y < lengthY; y++)
        {
            stringBuilder.AppendLine();
            for (int x = minX; x < lengthX; x++)
            {
                stringBuilder.Append(grid[x, y]);
            }
        }
        var writer = new StreamWriter("output.txt");
        writer.Write(stringBuilder.ToString());
    }

    public static int ItShallRainOnThesePeasants(int lengthX, int lengthY, char[,] grid, int part)
    {
        int total = 0;
        bool noOverflow = true;
        while (noOverflow)
        {
            // new grain of sand
            bool canFall = true;
            int cursorX = 500, cursorY = 0;
            while (canFall)
            {
                if (part == 1)
                {
                    if (cursorX - 1 < 0 || cursorX + 1 >= lengthX || cursorY + 1 >= lengthY)
                    {
                        noOverflow = false;
                        break;
                    }
                }
                else if (cursorX == 500 && cursorY == 0 && grid[cursorX, cursorY] != ' ')
                {
                    noOverflow = false;
                    break;
                }

                var rockBottomed = grid[cursorX, cursorY + 1] != ' '
                && grid[cursorX - 1, cursorY + 1] != ' '
                && grid[cursorX + 1, cursorY + 1] != ' ';
                if (rockBottomed)
                {
                    break;
                }

                if (grid[cursorX, cursorY + 1] == ' ')
                {
                    //keep falling
                    cursorY++;
                }
                else if (grid[cursorX - 1, cursorY + 1] == ' ')
                {
                    //keep falling left
                    cursorX--;
                    cursorY++;
                }
                else if (grid[cursorX + 1, cursorY + 1] == ' ')
                {
                    cursorX++;
                    cursorY++;
                }
            }
            if (noOverflow)
            {
                grid[cursorX, cursorY] = '.';
                total++;
            }
        }
        return total;
    }

    public static char[,] WhereDoesShitGo(HashSet<(int, int)> allRocks, int lengthX, int lengthY, int part)
    {
        var rocks = new char[lengthX, lengthY];
        for (int x = 0; x < lengthX; x++)
        {
            for (int y = 0; y < lengthY; y++)
            {
                rocks[x, y] = ' ';
            }
        }
        foreach (var rock in allRocks)
        {
            rocks[rock.Item1, rock.Item2] = '#';
        }

        if (part == 2)
        {
            for (int x = 0; x < lengthX; x++)
            {
                rocks[x, lengthY - 1] = '#';
            }
        }

        return rocks;
    }

    public static HashSet<(int, int)> GETALLTHEFUCKINGROCKS(string[] rockBlocks)
    {
        var allRocks = new HashSet<(int, int)>();
        foreach (var rock in rockBlocks)
        {
            (int, int) current = (int.MinValue, int.MinValue), target = (int.MinValue, int.MinValue);
            var lines = rock.Split(" -> ");
            foreach (var rockLine in lines)
            {
                if (current == (int.MinValue, int.MinValue)) current = WhereGoRock(rockLine);
                else if (target == (int.MinValue, int.MinValue)) target = WhereGoRock(rockLine);
                else
                {
                    current = target;
                    target = WhereGoRock(rockLine);
                }
                if (current != (int.MinValue, int.MinValue) && target != (int.MinValue, int.MinValue))
                {
                    for (int i = current.Item1; i <= target.Item1; i++) allRocks.Add((i, current.Item2));
                    for (int i = target.Item1; i <= current.Item1; i++) allRocks.Add((i, current.Item2));
                    for (int i = current.Item2; i <= target.Item2; i++) allRocks.Add((current.Item1, i));
                    for (int i = target.Item2; i <= current.Item2; i++) allRocks.Add((current.Item1, i));
                }
            }
        }

        return allRocks;
    }

    public static (int, int) WhereGoRock(string rockLine)
    {
        return (int.Parse(rockLine.Split(',')[0]), int.Parse(rockLine.Split(',')[1]));
    }
}