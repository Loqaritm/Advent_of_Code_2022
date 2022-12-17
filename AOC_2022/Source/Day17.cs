namespace AOC_2022.Day17;

public class Pair {
    public long X;
    public int Y;
    public Pair(long x, int y) {
        X = x;
        Y = y;
    }
}

// Represents shape
public class Rock {
    public List<Pair> RockShape;

    public Rock(List<Pair> rockShape)
    {
        RockShape = rockShape;
    }

    public Rock MovedBy(Pair move) {
        return new Rock(RockShape.Select(o=> new Pair(o.X + move.X, o.Y + move.Y)).ToList());
    }
}

public class RockProvider {
    private int _mRockIter;
    private List<Rock> _mRockList;
    public RockProvider() {
        // Initialize
        _mRockList = new List<Rock> {
            // - shape
            new Rock(new List<Pair>{new Pair(0,0), new Pair(0,1), new Pair(0,2), new Pair(0,3)}),
            // + shape
            new Rock(new List<Pair>{new Pair(0,1), new Pair(1,0), /*We do not need this middle point new Pair(1,1),*/ new Pair(1,2), new Pair(2,1)}),
            // J shape
            new Rock(new List<Pair>{new Pair(0,0), new Pair(0,1), new Pair(0,2), new Pair(1,2), new Pair(2,2)}),
            // I shape
            new Rock(new List<Pair>{new Pair(0,0), new Pair(1,0), new Pair(2,0), new Pair(3,0)}),
            // Square shape
            new Rock(new List<Pair>{new Pair(0,0), new Pair(0,1), new Pair(1,0), new Pair(1,1)})
        };
    }

    public Rock GetNext() {
        return new Rock(_mRockList[(_mRockIter++ % _mRockList.Count)].RockShape);
    }
}

public class MoveProvider {
    public enum DirectionEnum {
        L,
        R
    }

    private int _mMoveIter;
    private List<DirectionEnum> _mMovesList;

    public Pair GetNextMove() {
        return (_mMovesList[_mMoveIter++ % _mMovesList.Count] == DirectionEnum.L) ? new Pair(0,-1) : new Pair(0, 1);
    }

    public MoveProvider(string movesString) {
        _mMovesList = movesString.ToList().Select(o => (o == '<') ? DirectionEnum.L : DirectionEnum.R).ToList();
    }
}

public class Day17_1 : IDayPart<MoveProvider, int>
{
    public string DataFileName => "Day17.txt";

    public MoveProvider ParseData(string data)
    {
        return new MoveProvider(data);
    }

    public int RunInternal(MoveProvider data, ProgressBar? progress = null)
    {
        var rockProvider = new RockProvider();

        int currentLastLine = 0;

        // Lets go easy way here - assume board is 100000 high
        var board = new bool[100000,7];
        // Line 0 is floor
        for (int i=0; i<7; ++i) {
            board[0,i] = true;
        }

        for (int i=0; i<2022; ++i) {
            var currentRock = rockProvider.GetNext();
            // Move by 3 up and 2 right
            currentRock = currentRock.MovedBy(new Pair(currentLastLine + 2,2));

            // We can do 2 moves right away
            for (int j=0; j<2; ++j) {
                var movedRock = currentRock.MovedBy(data.GetNextMove());
                if (!movedRock.RockShape.Any(o => o.Y < 0 || o.Y > 6 || board[o.X, o.Y] == true)) {
                    // Can move
                    currentRock = movedRock;
                }
            }

            while (true) {
                // Move left / right
                var movedRock = currentRock.MovedBy(data.GetNextMove());
                if (!movedRock.RockShape.Any(o => o.Y < 0 || o.Y > 6 || board[o.X, o.Y] == true)) {
                    // Can move
                    currentRock = movedRock;
                }

                // Now fall
                movedRock = currentRock.MovedBy(new Pair(-1, 0));
                if (movedRock.RockShape.Any(o => board[o.X,o.Y] == true)) {
                    // End of this.
                    currentRock.RockShape.ForEach(o => board[o.X, o.Y] = true);
                    currentLastLine = Math.Max(currentLastLine, currentRock.RockShape.Max(o => (int)o.X));
                    break;
                }
                currentRock = movedRock;
            }

        }

        return currentLastLine;
    }
}




// The whole gist of part 2 is to find a cycle otherwise it would take WAY TO LONG
// I'm finding a cycle by checking the last n rows (found experimentally),
// computing a hash of sorts and checking if this key was ever found before.
// Some inputs require only 8, some 16, but 20 as n worked for me.
public class CycleDetector {
    private Dictionary<Int128, long> _mLastSeenHeight = new Dictionary<Int128, long>();
    private Dictionary<Int128, long> _mLastRoundSeen = new Dictionary<Int128, long>();

    public long CycleRoundLen = 0;
    public long CycleHeight = 0;
    public int CycleLenToCheck = 20;

    public bool AddDataAndCheckCycle(bool[,] wholeBoard, long currentHeight, long currentRound) {
        if (currentHeight < CycleLenToCheck) {
            // Not enough data
            return false;
        }

        Int128 key = 0;
        for (long i = currentHeight - CycleLenToCheck; i< currentHeight; ++i) {
            for (int j = 0; j < wholeBoard.GetLength(1); ++j) {
                key = (key << 1) | (wholeBoard[i,j] ? 0x01 : 0x00);
            }
        }

        if (_mLastRoundSeen.ContainsKey(key)) {
            CycleHeight = currentHeight - _mLastSeenHeight[key];
            CycleRoundLen = currentRound - _mLastRoundSeen[key];
            return true;
        } else {
            _mLastRoundSeen.Add(key, currentRound);
            _mLastSeenHeight.Add(key, currentHeight);
            return false;
        }
    }
}

public class Day17_2 : IDayPart<MoveProvider, long>
{
    private long _mNumberOfRounds;
    public Day17_2(long numberOfRounds) {
        _mNumberOfRounds = numberOfRounds;
    }

    public string DataFileName => "Day17.txt";

    public MoveProvider ParseData(string data)
    {
        return new MoveProvider(data);
    }

    public long RunInternal(MoveProvider data, ProgressBar? progress = null)
    {
        var rockProvider = new RockProvider();

        long currentLastLine = 0;
        long result = 0;

        // Lets go easy way here - assume board is 100000 high
        var board = new bool[100000,7];
        // Line 0 is floor
        for (int i=0; i<7; ++i) {
            board[0,i] = true;
        }

        var cycleDetector = new CycleDetector();
        var wasCycleDetected = false;

        for (long i=0; i<_mNumberOfRounds; ++i) {
            var currentRock = rockProvider.GetNext();
            // Move by 3 up and 2 right
            currentRock = currentRock.MovedBy(new Pair(currentLastLine + 2,2));

            // We can do 2 moves right away
            for (int j=0; j<2; ++j) {
                var movedRock = currentRock.MovedBy(data.GetNextMove());
                if (!movedRock.RockShape.Any(o => o.Y < 0 || o.Y > 6 || board[o.X, o.Y] == true)) {
                    // Can move
                    currentRock = movedRock;
                }
            }

            while (true) {
                // Move left / right
                var movedRock = currentRock.MovedBy(data.GetNextMove());
                if (!movedRock.RockShape.Any(o => o.Y < 0 || o.Y > 6 || board[o.X, o.Y] == true)) {
                    // Can move
                    currentRock = movedRock;
                }

                // Now fall
                movedRock = currentRock.MovedBy(new Pair(-1, 0));
                if (movedRock.RockShape.Any(o => board[o.X,o.Y] == true)) {
                    // End of this.
                    break;
                }
                currentRock = movedRock;
            }

            currentRock.RockShape.ForEach(o => board[o.X, o.Y] = true);

            var temp = currentRock.RockShape.Max(o => o.X);
            if (currentLastLine < temp) {
                result += temp - currentLastLine;
                currentLastLine = temp;

                if (wasCycleDetected) {
                    continue;
                }
                if (cycleDetector.AddDataAndCheckCycle(board, currentLastLine, i))  {
                    // Yay, cycle detected!
                    var numberOfCyclesThatFitTillEnd = (_mNumberOfRounds - i) / cycleDetector.CycleRoundLen;
                    result += numberOfCyclesThatFitTillEnd * cycleDetector.CycleHeight;
                    i += numberOfCyclesThatFitTillEnd * cycleDetector.CycleRoundLen;
                }
            }
        }

        return result;
    }
}