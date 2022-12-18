namespace AOC_2022.Day18;

public class Cube1x1x1 {
    public int X;
    public int Y;
    public int Z;

    public Cube1x1x1(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public bool checkIfConnected(Cube1x1x1 other) {
        var dX = Math.Abs(X - other.X);
        var dY = Math.Abs(Y - other.Y);
        var dZ = Math.Abs(Z - other.Z);

        if (dX == 1 && dY == 0 && dZ == 0) {
            return true;
        }
        else if (dX == 0 && dY == 1 && dZ == 0) {
            return true;
        }
        else if (dX == 0 && dY == 0 && dZ == 1) {
            return true;
        }
        return false;
    }

    public override bool Equals(object? obj)
    {
        return obj is Cube1x1x1 x &&
               X == x.X &&
               Y == x.Y &&
               Z == x.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public int ExposedSides = 6;
}

public class Day18_1 : IDayPart<List<Cube1x1x1>, int>
{
    public string DataFileName => "Day18.txt";

    public List<Cube1x1x1> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o => {
            var split = o.Split(",").Select(int.Parse).ToList();
            return new Cube1x1x1(split[0], split[1], split[2]);
        }).ToList();
    }

    public int RunInternal(List<Cube1x1x1> data, ProgressBar? progress = null)
    {
        return data.Sum(cube => {
            data.ForEach(o => { if (cube.checkIfConnected(o)) cube.ExposedSides--; });
            return cube.ExposedSides;
        });
    }
}



public class Day18_2 : IDayPart<List<Cube1x1x1>, int>
{
    public string DataFileName => "Day18.txt";

    public List<Cube1x1x1> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o => {
            var split = o.Split(",").Select(int.Parse).ToList();
            return new Cube1x1x1(split[0], split[1], split[2]);
        }).ToList();
    }

    public int RunInternal(List<Cube1x1x1> data, ProgressBar? progress = null)
    {
        int ENCLOSING_SIZE_UPPER = data.Max(o => Math.Max(o.X, Math.Max(o.Y, o.Z))) + 1;
        int ENCLOSING_SIZE_LOWER = data.Min(o => Math.Min(o.X, Math.Min(o.Y, o.Z))) - 1;
        
        List<Cube1x1x1> getAdjecent(Cube1x1x1 cube) {
            var listToReturn = new List<Cube1x1x1>();
            if (cube.X + 1 <= ENCLOSING_SIZE_UPPER) { listToReturn.Add(new Cube1x1x1(cube.X + 1, cube.Y, cube.Z)); }
            if (cube.X - 1 >= ENCLOSING_SIZE_LOWER) { listToReturn.Add(new Cube1x1x1(cube.X - 1, cube.Y, cube.Z)); }
            if (cube.Y + 1 <= ENCLOSING_SIZE_UPPER) { listToReturn.Add(new Cube1x1x1(cube.X, cube.Y + 1, cube.Z)); }
            if (cube.Y - 1 >= ENCLOSING_SIZE_LOWER) { listToReturn.Add(new Cube1x1x1(cube.X, cube.Y - 1, cube.Z)); }
            if (cube.Z + 1 <= ENCLOSING_SIZE_UPPER) { listToReturn.Add(new Cube1x1x1(cube.X, cube.Y, cube.Z + 1)); }
            if (cube.Z - 1 >= ENCLOSING_SIZE_LOWER) { listToReturn.Add(new Cube1x1x1(cube.X, cube.Y, cube.Z - 1)); }
            return listToReturn;
        }

        // BFS
        var queue = new Queue<Cube1x1x1>();
        queue.Enqueue(new Cube1x1x1(ENCLOSING_SIZE_LOWER,ENCLOSING_SIZE_LOWER,ENCLOSING_SIZE_LOWER));
        List<Cube1x1x1> visitedOutsideCubes = new List<Cube1x1x1>();

        while (queue.Count > 0) {
            var element = queue.Dequeue();

            if (visitedOutsideCubes.Contains(element)) {
                continue;
            }

            visitedOutsideCubes.Add(element);
            foreach(var el in getAdjecent(element)) {       
                if (visitedOutsideCubes.Contains(el)) {
                    continue;
                }
                if (data.Contains(el)) {
                    continue;
                }
                queue.Enqueue(el);
            }
        }

        return data.Sum(cube => {
            return visitedOutsideCubes.Sum(o => (cube.checkIfConnected(o)) ? 1 : 0);
        });
    }
}
