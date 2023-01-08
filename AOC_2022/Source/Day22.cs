using System.Text;

namespace AOC_2022.Day22.Part1;

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

    public MonkeyMap(List<List<char>> map, Queue<char> instructions)
    {
        Map = map;
        Instructions = instructions;
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

    public Postiion GetNextPosition() {
        switch (CurrentDirection) {
            case Direction.U:
                if (CurrentX - 1 < 0 || Map[CurrentX-1].Count <= CurrentY || Map[CurrentX-1][CurrentY] == ' ') {
                    return new Postiion(Map.FindLastIndex(line => CurrentY < line.Count && line[CurrentY] != ' '), CurrentY);
                }
                return new Postiion(CurrentX - 1, CurrentY);
            case Direction.D:
                if (CurrentX + 1 >= Map.Count || Map[CurrentX+1].Count <= CurrentY || Map[CurrentX+1][CurrentY] == ' ') {
                    return new Postiion(Map.FindIndex(line => CurrentY < line.Count && line[CurrentY] != ' '), CurrentY);
                }
                return new Postiion(CurrentX + 1, CurrentY);
            case Direction.L:
                if (CurrentY - 1 < 0 || Map[CurrentX][CurrentY-1] == ' ') {
                    return new Postiion(CurrentX, Map[CurrentX].FindLastIndex(o => o != ' '));
                }
                return new Postiion(CurrentX, CurrentY - 1);
            case Direction.R:
                if (CurrentY + 1 >= Map[CurrentX].Count || Map[CurrentX][CurrentY+1] == ' ') {
                    return new Postiion(CurrentX, Map[CurrentX].FindIndex(o => o != ' '));
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

                var nextPos = GetNextPosition();

                if (Map[nextPos.X][nextPos.Y] == '#') {
                    break;
                }

                CurrentX = nextPos.X;
                CurrentY = nextPos.Y;
            }
        }
        return true;
    }
}

public class Day22_1 : IDayPart<MonkeyMap, int>
{
    public string DataFileName => "Day22.txt";

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
