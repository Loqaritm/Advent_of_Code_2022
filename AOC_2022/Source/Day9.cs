using System;

namespace AOC_2022.Day9;

public class Move {
    public enum DirectionEnum {
        U,
        D,
        L,
        R
    };

    public DirectionEnum Direction;
    public int Distance;
}

public struct Position : IEquatable<Position>{
    public int x;
    public int y;

    public Position(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public bool Equals(Position other)
    {
        return this.x == other.x && this.y == other.y;
    }
}

public class Day9_1 : IDayPart<List<Move>, int>
{
    public int DayNumber => 9;

    public List<Move> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o=> {
            var splitData = o.Split(' ').ToList();
            return new Move{
                Direction = (Move.DirectionEnum)Enum.Parse(typeof(Move.DirectionEnum), splitData[0]),
                Distance = int.Parse(splitData[1])
            };
        }).ToList();
    }

    private bool needToMoveKnot(Position tailPostion, Position newHeadPosition) {
        var xDelta = Math.Abs(newHeadPosition.x - tailPostion.x);
        var yDelta = Math.Abs(newHeadPosition.y - tailPostion.y);
        return xDelta > 1 || yDelta > 1;
    }

    private bool needToMoveDiagonally(Position tailPosition, Position newHeadPosition) {
        var xDelta = Math.Abs(newHeadPosition.x - tailPosition.x);
        var yDelta = Math.Abs(newHeadPosition.y - tailPosition.y);
        return (xDelta > 1 && yDelta > 0) || (xDelta > 0 && yDelta > 1);
    }

    protected Position getNewTailPosAfterMove(Position tailPosition, Position newHeadPosition) {
        if (!needToMoveKnot(tailPosition, newHeadPosition)) { return tailPosition; }

        var xDelta = newHeadPosition.x - tailPosition.x;
        var yDelta = newHeadPosition.y - tailPosition.y;

        int normalizeToOne(int delta) {
            return (int)delta / (int)Math.Abs(delta);
        }

        if (needToMoveDiagonally(tailPosition, newHeadPosition)) {
            return new Position(tailPosition.x + normalizeToOne(xDelta), tailPosition.y + normalizeToOne(yDelta));
        }

        if (xDelta != 0) {
            return new Position(tailPosition.x + normalizeToOne(xDelta), tailPosition.y);
        }
        
        return new Position(tailPosition.x, tailPosition.y + normalizeToOne(yDelta));
    }

    public int RunInternal(List<Move> data, ProgressBar? progress = null)
    {
        Position startingPoint = new Position(0,0);
        var headPos = startingPoint;
        var tailPos = startingPoint;
        var visitedByTail = new List<Position>{startingPoint};

        foreach(var move in data) {
            foreach(var moveBy in Enumerable.Range(1, move.Distance)) {
                Position newHeadPosition = move.Direction switch
                {
                    Move.DirectionEnum.L => new Position(headPos.x - 1, headPos.y),
                    Move.DirectionEnum.R => new Position(headPos.x + 1, headPos.y),
                    Move.DirectionEnum.D => new Position(headPos.x, headPos.y - 1),
                    Move.DirectionEnum.U => new Position(headPos.x, headPos.y + 1),
                    _ => throw new ArgumentOutOfRangeException(nameof(move.Direction))
                };

                // Ugh...
                // The task specifies, that if the knots aren't touching,
                // the second one will move always DIAGONALLY
                // With two knots it doesn't matter, but with more it does.
                
                tailPos = getNewTailPosAfterMove(tailPos, newHeadPosition);
                visitedByTail.Add(tailPos);
                headPos = newHeadPosition;
            }
        }

        return visitedByTail.Distinct().ToList().Count;
    }
}

public class Day9_2 : Day9_1, IDayPart<List<Move>, int> {
    public new int RunInternal(List<Move> data, ProgressBar? progress = null) {
        var KNOTS_COUNT = 10;
        List<Position> knotPositions = Enumerable.Repeat(new Position(0,0), KNOTS_COUNT).ToList();
        var visitedByTail = new List<Position>{new Position(0,0)};

        foreach(var move in data) {
            foreach(var moveBy in Enumerable.Range(1, move.Distance)) {
                var headPos = knotPositions[0];
                Position newHeadPosition = move.Direction switch
                {
                    Move.DirectionEnum.L => new Position(headPos.x - 1, headPos.y),
                    Move.DirectionEnum.R => new Position(headPos.x + 1, headPos.y),
                    Move.DirectionEnum.D => new Position(headPos.x, headPos.y - 1),
                    Move.DirectionEnum.U => new Position(headPos.x, headPos.y + 1),
                    _ => throw new ArgumentOutOfRangeException(nameof(move.Direction))
                };

                // Update just actual head
                knotPositions[0] = newHeadPosition;

                // Now we need to update all the other knots
                for (int i = 1; i < knotPositions.Count; ++i) {
                    var nextKnotInHeadDir = knotPositions[i-1];
                    var thisKnot = knotPositions[i];

                    // Ugh...
                    // The task specifies, that if the knots aren't touching,
                    // the second one will move always DIAGONALLY
                    // With two knots it doesn't matter, but with more it does.
                    
                    thisKnot = getNewTailPosAfterMove(thisKnot, nextKnotInHeadDir);
                    knotPositions[i] = thisKnot;
                    if (i == knotPositions.Count - 1) {
                        // Last knot
                        visitedByTail.Add(thisKnot);
                    }
                }
            }
        }

        return visitedByTail.Distinct().ToList().Count;
    }
}