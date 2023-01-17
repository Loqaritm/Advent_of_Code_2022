namespace AOC_2022.Day14;

public class Position {
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Day14_1 : IDayPart<char[,], int>
{
    public int DayNumber => 14;

    public char[,] ParseData(string data)
    {
        int maxX = 0, maxY = 0;
        var listOfWalls = data.Split(Environment.NewLine).SelectMany(line => {
            var points = line.Split(" -> ").Select(o => {
                var split = o.Split(",").Select(int.Parse).ToList();

                maxX = Math.Max(maxX, split[0]);
                maxY = Math.Max(maxY, split[1]);

                return new Position(split[0], split[1]);
            }).ToList();

            var walls = new List<Tuple<Position, Position>>();
            for (int i = 0; i < points.Count() - 1; ++i) {
                walls.Add(new Tuple<Position, Position>(points[i], points[i+1]));
            }
            return walls;
        }).ToList();

        var resultArray = new char[maxX + 1, maxY + 1];
        for (int i = 0; i < resultArray.GetLength(0); ++i) {
            for (int j = 0; j < resultArray.GetLength(1); ++j) {
                resultArray[i,j] = '.';
            }
        }

        foreach (var wall in listOfWalls) {
            if (wall.Item1.X == wall.Item2.X) {
                // We'll be going by Y
                var smallerItem = Math.Min(wall.Item1.Y, wall.Item2.Y);
                var biggerItem = Math.Max(wall.Item1.Y, wall.Item2.Y);
                for (int i = smallerItem; i <= biggerItem; ++i) {
                    resultArray[wall.Item1.X,i] = '#';
                }
            } else {
                // We'll be going by X
                var smallerItem = Math.Min(wall.Item1.X, wall.Item2.X);
                var biggerItem = Math.Max(wall.Item1.X, wall.Item2.X);
                for (int i = smallerItem; i <= biggerItem; ++i) {
                    resultArray[i,wall.Item1.Y] = '#';
                }
            }
        }

        return resultArray;
    }

    public int RunInternal(char[,] data, ProgressBar? progress = null)
    {
        var result = 0;
        while (true) {
            // create grain of sand.
            var grainOfSand = new Position(500,0);
            while (true) {
                if (grainOfSand.Y + 1 >= data.GetLength(1)) {
                    // Fell out bottom
                    return result;
                }
                if (data[grainOfSand.X,grainOfSand.Y + 1] == '.') {
                    // We can move down
                    grainOfSand = new Position(grainOfSand.X, grainOfSand.Y + 1);
                    continue;
                }

                if (grainOfSand.X - 1 < 0) {
                    // fell out left
                    return result;
                }
                if (data[grainOfSand.X - 1,grainOfSand.Y + 1] == '.') {
                    // We can move down left
                    grainOfSand = new Position(grainOfSand.X - 1, grainOfSand.Y + 1);
                    continue;
                }

                if (grainOfSand.X + 1 >= data.GetLength(0)) {
                    // fell out right
                    return result;
                }
                if (data[grainOfSand.X + 1,grainOfSand.Y + 1] == '.') {
                    // We can move down right
                    grainOfSand = new Position(grainOfSand.X + 1, grainOfSand.Y + 1);
                    continue;
                }

                // Stop this sand grain
                data[grainOfSand.X,grainOfSand.Y] = 'o';
                result++;
                break;
            }
        }
    }
}





// Lets do part 2 quick and dirty - just add an X offset of 2000
public class Day14_2 : IDayPart<char[,], int>
{
    private int X_OFFSET = 2000;

    public int DayNumber => 14;

    public char[,] ParseData(string data)
    {
        int maxX = 0, maxY = 0;
        var listOfWalls = data.Split(Environment.NewLine).SelectMany(line => {
            var points = line.Split(" -> ").Select(o => {
                var split = o.Split(",").Select(int.Parse).ToList();

                maxX = Math.Max(maxX, split[0]) + X_OFFSET;
                maxY = Math.Max(maxY, split[1]);

                return new Position(split[0] + X_OFFSET, split[1]);
            }).ToList();

            var walls = new List<Tuple<Position, Position>>();
            for (int i = 0; i < points.Count() - 1; ++i) {
                walls.Add(new Tuple<Position, Position>(points[i], points[i+1]));
            }
            return walls;
        }).ToList();

        var resultArray = new char[maxX + 1 + X_OFFSET, maxY + 1 + 2];
        for (int i = 0; i < resultArray.GetLength(0); ++i) {
            for (int j = 0; j < resultArray.GetLength(1); ++j) {
                // We now have a floor
                resultArray[i,j] = (j == resultArray.GetLength(1) - 1) ? '#' : '.';
            }
        }

        foreach (var wall in listOfWalls) {
            if (wall.Item1.X == wall.Item2.X) {
                // We'll be going by Y
                var smallerItem = Math.Min(wall.Item1.Y, wall.Item2.Y);
                var biggerItem = Math.Max(wall.Item1.Y, wall.Item2.Y);
                for (int i = smallerItem; i <= biggerItem; ++i) {
                    resultArray[wall.Item1.X,i] = '#';
                }
            } else {
                // We'll be going by X
                var smallerItem = Math.Min(wall.Item1.X, wall.Item2.X);
                var biggerItem = Math.Max(wall.Item1.X, wall.Item2.X);
                for (int i = smallerItem; i <= biggerItem; ++i) {
                    resultArray[i,wall.Item1.Y] = '#';
                }
            }
        }

        return resultArray;
    }

    public int RunInternal(char[,] data, ProgressBar? progress = null)
    {
        var result = 0;
        while (true) {
            // create grain of sand.
            var grainOfSand = new Position(500 + X_OFFSET,0);
            while (true) {
                if (data[grainOfSand.X,grainOfSand.Y + 1] == '.') {
                    // We can move down
                    grainOfSand = new Position(grainOfSand.X, grainOfSand.Y + 1);
                    continue;
                }

                if (data[grainOfSand.X - 1,grainOfSand.Y + 1] == '.') {
                    // We can move down left
                    grainOfSand = new Position(grainOfSand.X - 1, grainOfSand.Y + 1);
                    continue;
                }

                if (data[grainOfSand.X + 1,grainOfSand.Y + 1] == '.') {
                    // We can move down right
                    grainOfSand = new Position(grainOfSand.X + 1, grainOfSand.Y + 1);
                    continue;
                }

                if (grainOfSand.X == 500 + X_OFFSET && grainOfSand.Y == 0) {
                    // Meaning we blocked this thing, no moves
                    return result + 1;
                }

                // Stop this sand grain
                data[grainOfSand.X,grainOfSand.Y] = 'o';
                result++;
                break;
            }
        }
    }
}
