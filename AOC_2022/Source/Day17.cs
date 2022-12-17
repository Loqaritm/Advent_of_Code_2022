namespace AOC_2022.Day17;

public class Pair {
    public int X;
    public int Y;
    public Pair(int x, int y) {
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
                    currentLastLine = Math.Max(currentLastLine, currentRock.RockShape.Max(o => o.X));
                    break;
                }
                currentRock = movedRock;
            }

        }

        return currentLastLine;
    }
}

public class Day17_2 : Day17_1, IDayPart<MoveProvider, long>
{
    public new long RunInternal(MoveProvider data, ProgressBar? progress)
    {
        var rockProvider = new RockProvider();

        long heightOfBlocks = 0;
        int currentLastLine = 0;

        long HOW_LONG_TO_RUN = 1000000000000;

        // Lets go easy way here - assume board is 100000 high
        var board = new bool[100000,7];
        // Line 0 is floor
        for (int i=0; i<7; ++i) {
            board[0,i] = true;
        }

        for (long i=0; i<HOW_LONG_TO_RUN; ++i) {

            progress?.Report((double)i / HOW_LONG_TO_RUN);

            var currentRock = rockProvider.GetNext();
            // Move by 3 up and 2 right
            currentRock = currentRock.MovedBy(new Pair(currentLastLine + 4,2));

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
                    currentLastLine = Math.Max(currentLastLine, currentRock.RockShape.Max(o => o.X));

                    if (currentLastLine >= 5000) {
                        // Need to cut down to have enough memory
                        // We're moving the board by 4000 down
                        var helperArray = new bool[100000,7];
                        Array.Copy(board, 4000, helperArray, 0, 2000);
                        board = helperArray;
                        currentLastLine -= 4000;
                        heightOfBlocks += 4000;
                    }

                    break;
                }
                currentRock = movedRock;
            }

        }

        return currentLastLine + heightOfBlocks;
    }
}

// expected for test =  1514285714288
// intMax =             2147483647
// int64 max =          9223372036854775807