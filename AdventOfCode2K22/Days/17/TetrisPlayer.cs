namespace Day17;

public static class TetrisPlayer
{
    public static double Play(string jetPattern, double totalBlocks)
    {
        var blocks = new[]
        {
            new TetrisBlock(TetrisType.HLine),
            new TetrisBlock(TetrisType.Cross),
            new TetrisBlock(TetrisType.L),
            new TetrisBlock(TetrisType.VLine),
            new TetrisBlock(TetrisType.Cube),
        };
        double blockCount = 0, totalHeight = 0;
        int currentHeight = 0;
        var blockIndex = 0;
        const int yDelta = 3, xDelta = 2, maxWidth = 7, arraySize = 1000000, threshold = 999950, warning = 999970;
        var final = new char[maxWidth, arraySize];
        var cursor = (x: xDelta, y: yDelta);
        var patternIndex = 0;
        while (blockCount < totalBlocks)
        {
            var hitRockBottom = false;
            var currentRock = blocks[blockIndex];
            var movementCount = 0;
            while (!hitRockBottom)
            {
                if (patternIndex == 0)
                {
                    Console.WriteLine($"index: {blockIndex}, count: {blockCount}, currentHeight: {currentHeight}");
                    Print(final, maxWidth, 5);
                }

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
                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x + 1 + extremity.x, cursor.y + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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
                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x - 1 + extremity.x, cursor.y + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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

                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x + extremity.x, cursor.y - 1 + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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

            currentHeight = Math.Max(cursor.y + currentRock.Shape.Length, currentHeight);
            if (currentHeight >= warning)
            {
                var temp = new char[maxWidth, arraySize];
                var tempY = 0;
                for (int y = threshold; y < currentHeight; y++)
                {
                    for (int x = 0; x < maxWidth; x++)
                    {
                        temp[x, tempY] = final[x, y];
                    }
                    tempY++;
                }
                final = temp;
                currentHeight -= threshold;
                cursor = (x: xDelta, y: currentHeight + yDelta);
                totalHeight += threshold;
            }
            else
            {
                cursor = (x: xDelta, y: currentHeight + yDelta);
            }
            blockIndex = ++blockIndex % blocks.Length;
            blockCount++;
        }

        return totalHeight + currentHeight;
    }

    public static double PlayButBetter(string jetPattern, double totalBlocks)
    {
        var blocks = new[]
        {
            new TetrisBlock(TetrisType.HLine),
            new TetrisBlock(TetrisType.Cross),
            new TetrisBlock(TetrisType.L),
            new TetrisBlock(TetrisType.VLine),
            new TetrisBlock(TetrisType.Cube),
        };
        double blockCount = 0;
        int currentHeight = 0;
        var blockIndex = 0;
        const int yDelta = 3, xDelta = 2, maxWidth = 7, arraySize = 10000000;
        var final = new char[maxWidth, arraySize];
        var cursor = (x: xDelta, y: yDelta);
        var patternIndex = 0;
        //calculate first part (not pattern)
        var started = false;
        var emergencyStop = false;
        while (true)
        {
            var currentRock = blocks[blockIndex];
            var hitRockBottom = false;
            var movementCount = 0;
            while (!hitRockBottom)
            {
                char currentMovement;
                if (movementCount++ % 2 == 0)
                {
                    currentMovement = jetPattern[patternIndex];
                    patternIndex = ++patternIndex % jetPattern.Length;
                    if (patternIndex == 0 && started) emergencyStop = true;
                    if (!started) started = true;
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
                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x + 1 + extremity.x, cursor.y + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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
                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x - 1 + extremity.x, cursor.y + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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

                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x + extremity.x, cursor.y - 1 + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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
            currentHeight = Math.Max(cursor.y + currentRock.Shape.Length, currentHeight);
            cursor = (x: xDelta, y: currentHeight + yDelta);
            blockIndex = ++blockIndex % blocks.Length;
            blockCount++;
            if (emergencyStop) break;
        }
        double firstBlockCount = blockCount;
        double firstHeight = currentHeight;
        double remaining = totalBlocks - firstBlockCount;
        //calculate pattern height
        started = false;
        emergencyStop = false;
        while(true)
        {
            var currentRock = blocks[blockIndex];
            var hitRockBottom = false;
            var movementCount = 0;
            while (!hitRockBottom)
            {
                char currentMovement;
                if (movementCount++ % 2 == 0)
                {
                    currentMovement = jetPattern[patternIndex];
                    patternIndex = ++patternIndex % jetPattern.Length;
                    if (patternIndex == 0 && started) emergencyStop = true;
                    if (!started) started = true;
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
                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x + 1 + extremity.x, cursor.y + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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
                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x - 1 + extremity.x, cursor.y + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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

                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x + extremity.x, cursor.y - 1 + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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
            currentHeight = Math.Max(cursor.y + currentRock.Shape.Length, currentHeight);
            cursor = (x: xDelta, y: currentHeight + yDelta);
            blockIndex = ++blockIndex % blocks.Length;
            blockCount++;
            if (emergencyStop) break;
        }
        double patternBlocks = blockCount - firstBlockCount;
        double patternHeight = currentHeight - firstHeight;
        double quotient = (int)(remaining / patternBlocks);
        double lastBlocks = remaining % patternBlocks;
        double bigHeight = patternHeight * quotient;
        blockCount = 0;
        //calc last chunk height
        while (blockCount < lastBlocks)
        {
            var currentRock = blocks[blockIndex];
            var hitRockBottom = false;
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
                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x + 1 + extremity.x, cursor.y + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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
                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x - 1 + extremity.x, cursor.y + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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

                        foreach (var extremity in currentRock.Extremities)
                        {
                            (int x, int y) targetCursor = (cursor.x + extremity.x, cursor.y - 1 + extremity.y);
                            var target = final[targetCursor.x, targetCursor.y];
                            if (target == '\0') continue;
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
            currentHeight = Math.Max(cursor.y + currentRock.Shape.Length, currentHeight);
            cursor = (x: xDelta, y: currentHeight + yDelta);
            blockIndex = ++blockIndex % blocks.Length;
            blockCount++;
        }
        double lastChunkHeight = currentHeight - patternHeight - firstHeight;
        return firstHeight + bigHeight + lastChunkHeight;
    }

    public static void Print(char[,] toPrint, int maxWidth, int lines)
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
        foreach (var line in temp.ToString().Split("\r\n").Reverse().Take(lines))
        {
            result.AppendLine(line.Replace('\0', ' '));
        }
        Console.WriteLine(result.ToString());
    }
}