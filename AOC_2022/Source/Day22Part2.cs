using System.Text;

namespace AOC_2022.Day22.Part2;

public class Postiion {
    public int X;
    public int Y;

    public Postiion(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class MonkeyMap {
    public List<List<char>> Map;
    public Queue<char> Instructions;
    private int _mSideLenth;

    public MonkeyMap(List<List<char>> map, Queue<char> instructions)
    {
        Map = map;
        Instructions = instructions;
        _mSideLenth = map[0].Count / 3; // First row is _12 so that's why
        CurrentX = 0; // Start at top row
        CurrentY = map[0].IndexOf('.'); // Start at first possible place from left
        CurrentDirection = Direction.R; // Start facing right
    }

    public int CurrentX;
    public int CurrentY;
    public Direction CurrentDirection;

    public enum Direction {
        R = 0,
        D = 1,
        L = 2,
        U = 3
    }

    // Cube sides as seen by my input
    //  12
    //  3
    // 45
    // 6
    public Postiion GetNextPosition(out Direction newDirection) {
        newDirection = CurrentDirection;
        switch (CurrentDirection) {
            case Direction.U:
                if (CurrentX - 1 < 0) {
                    // Went up over the board - meaning 1 or 2
                    if (CurrentY / _mSideLenth == 1) {
                        // 1 -> up -> 6 (from left)
                        newDirection = Direction.R;
                        return new Postiion(_mSideLenth*3 + CurrentY % _mSideLenth, 0);
                    }
                    else {
                        // 2 -> up -> 6 (drom down)
                        return new Postiion(Map.Count - 1, CurrentY % _mSideLenth);
                    }
                }
                if (Map[CurrentX - 1][CurrentY] == ' ') {
                    // Means this is 4 to 3
                    newDirection = Direction.R;
                    return new Postiion(_mSideLenth + CurrentY, _mSideLenth);
                }
                return new Postiion(CurrentX - 1, CurrentY);
            case Direction.D:
                if (CurrentX + 1 >= Map.Count) {
                    // 6 -> down -> 2 (from up)
                    return new Postiion(0, CurrentY + _mSideLenth * 2);
                }
                if (CurrentY >= Map[CurrentX + 1].Count) {
                    if (CurrentY / _mSideLenth == 1) {
                        // 5 -> down -> 6 (from right)
                        newDirection = Direction.L;
                        return new Postiion(_mSideLenth * 3 + CurrentY % _mSideLenth, _mSideLenth - 1);
                    }
                    else {
                        newDirection = Direction.L;
                        return new Postiion(_mSideLenth + CurrentY % _mSideLenth, 2*_mSideLenth - 1);
                    }
                }
                return new Postiion(CurrentX + 1, CurrentY);
            case Direction.L:
                if (CurrentY - 1 < 0) {
                    // 4 or 6
                    if (CurrentX / _mSideLenth == 3) {
                        // 6 -> left -> 1 (from up)
                        newDirection = Direction.D;
                        return new Postiion(0, _mSideLenth + (CurrentX % _mSideLenth));
                    }
                    else {
                        // 4 -> left -> 1 (from left)
                        newDirection = Direction.R;
                        return new Postiion(_mSideLenth - 1 - (CurrentX % _mSideLenth), _mSideLenth);
                    }
                }
                if (Map[CurrentX][CurrentY - 1] == ' ') {
                    // 1 or 3
                    if (CurrentX / _mSideLenth == 0) {
                        // 1 -> left -> 4 (from left)
                        newDirection = Direction.R;
                        return new Postiion(3 * _mSideLenth - 1 - CurrentX, 0);
                    }
                    else {
                        // 3 -> left -> 4 (from up)
                        newDirection = Direction.D;
                        return new Postiion(_mSideLenth * 2, CurrentX % _mSideLenth);
                    }
                }
                return new Postiion(CurrentX, CurrentY - 1);
            case Direction.R:
                if (CurrentY + 1 >= Map[0].Count) {
                    // 2 -> right -> 5 (from right (upside down))
                    newDirection = Direction.L;
                    return new Postiion(3 * _mSideLenth - 1 - CurrentX, 2 * _mSideLenth - 1);
                }
                if (CurrentY + 1 >= Map[CurrentX].Count) {
                    // 3, 5, 6
                    if (CurrentX / _mSideLenth == 1) {
                        // 3 -> right -> 2 (from down)
                        newDirection = Direction.U;
                        return new Postiion(_mSideLenth - 1, 2 * _mSideLenth + (CurrentX % _mSideLenth));
                    }
                    else if (CurrentX / _mSideLenth == 2) {
                        // 5 -> right -> 2 (from left (upside down))
                        newDirection = Direction.L;
                        return new Postiion(_mSideLenth - 1 - (CurrentX % _mSideLenth), Map[0].Count - 1);
                    }
                    else {
                        // 6 -> right -> 5 (from down)
                        newDirection = Direction.U;
                        return new Postiion(3 * _mSideLenth - 1, _mSideLenth + (CurrentX % _mSideLenth));
                    }
                }
                return new Postiion(CurrentX, CurrentY + 1);
            default:
                throw new NotSupportedException();
        }
    }

    public bool ProcessInstruction() {
        if (Instructions.Count == 0) { return false; }

        var currentInstruction = Instructions.Dequeue();
        if (currentInstruction == 'L' || currentInstruction == 'R') {
            // Rotation
            CurrentDirection = (Direction)(((int)CurrentDirection + 4 + ((currentInstruction == 'L') ? -1 : 1)) % 4);
        } else {
            // Move
            // Numbers can be more than one symbol
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(currentInstruction);
            while(Instructions.Count > 0 && Char.IsDigit(Instructions.Peek())) {
                stringBuilder.Append(Instructions.Dequeue());
            }
            
            var numberToMove = int.Parse(stringBuilder.ToString());
            for (int i = 0; i < numberToMove; ++i) {
                
                var newDirection = CurrentDirection;
                var nextPos = GetNextPosition(out newDirection);

                if (Map[nextPos.X][nextPos.Y] == '#') {
                    break;
                }

                CurrentX = nextPos.X;
                CurrentY = nextPos.Y;
                CurrentDirection = newDirection;
            }
        }
        return true;
    }
}

public class Day22_2 : IDayPart<MonkeyMap, int>
{
    public int DayNumber => 22;

    public MonkeyMap ParseData(string data)
    {  
        var split = data.Split(Environment.NewLine + Environment.NewLine);
        var instructions = split[1].ToList();
        var map = split[0].Split(Environment.NewLine).Select(line => {
            return line.ToList();
        }).ToList();
        return new MonkeyMap(map, new Queue<char>(instructions));
    }

    public int RunInternal(MonkeyMap data, ProgressBar? progress = null)
    {
        while (data.ProcessInstruction()) {
            // Do nothing
        }

        return 1000 * (data.CurrentX + 1) + 4 * (data.CurrentY + 1) + (int)data.CurrentDirection;
    }
}
