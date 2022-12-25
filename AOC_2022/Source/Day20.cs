namespace AOC_2022.Day20;

public class NumberPosition {
    public int OriginalPosition;
    public int Value;

    public NumberPosition(int value, int originalPosition)
    {
        Value = value;
        OriginalPosition = originalPosition;
    }
}

public class Day20_1 : IDayPart<List<NumberPosition>, int>
{
    public string DataFileName => "Day20.txt";

    public List<NumberPosition> ParseData(string data)
    {
        var index = 0;
        return data.Split(Environment.NewLine).Select(int.Parse).Select(o => new NumberPosition(o, index++)).ToList();
    }

    public int RunInternal(List<NumberPosition> data, ProgressBar? progress = null)
    {
        for (int i = 0; i<data.Count; ++i) {
            var position = data.FindIndex(0, o => o.OriginalPosition == i);
            bool isMovingRight = (data[position].Value >= 0);

            if (isMovingRight) {
                var item = data[position];
                var amountToMoveRemaining = item.Value % (data.Count - 1);

                while (amountToMoveRemaining >= data.Count - position) {
                    // We'll be moving over the bounds
                    data.RemoveAt(position);
                    data.Add(item);

                    // Now that the item is at the end, swap it with first element to properly handle bounds
                    data.Swap(0,data.Count - 1);
                    amountToMoveRemaining -= data.Count - position;
                    position = 0;
                    item = data[position];
                }

                data.RemoveAt(position);
                data.Insert(position + amountToMoveRemaining, item);
            } else {
                var item = data[position];
                var amountToMoveRemaining = Math.Abs(item.Value) % (data.Count - 1);

                while (position - amountToMoveRemaining < 0) {
                    // We'll be moving over the bounds
                    data.RemoveAt(position);
                    data.Insert(0,item);

                    // Now that the item is at the end, swap it with first element to properly handle bounds
                    data.Swap(0,data.Count - 1);
                    amountToMoveRemaining -= position + 1;
                    position = data.Count - 1;
                    item = data[position];
                }

                data.RemoveAt(position);
                data.Insert(position - amountToMoveRemaining, item);
            }
        }

        var ZeroPos = data.FindIndex(o => o.Value == 0);

        int getNumberAtPos(int pos) {
            return data![(ZeroPos + pos) % data.Count].Value;
        }

        return getNumberAtPos(1000) + getNumberAtPos(2000) + getNumberAtPos(3000);
    }
}

public class NumberPosition2 {
    public int OriginalPosition;
    public long Value;

    public NumberPosition2(long value, int originalPosition)
    {
        Value = value;
        OriginalPosition = originalPosition;
    }
}

public class Day20_2 : IDayPart<List<NumberPosition2>, long>
{
    public string DataFileName => "Day20.txt";

    private readonly long DECRYPTION_KEY = 811589153;

    public List<NumberPosition2> ParseData(string data)
    {
        var index = 0;
        return data.Split(Environment.NewLine).Select(int.Parse).Select(o => new NumberPosition2(o * DECRYPTION_KEY, index++)).ToList();
    }

    public long RunInternal(List<NumberPosition2> data, ProgressBar? progress = null)
    {
        for (int numOfRepeats = 0; numOfRepeats < 10; ++numOfRepeats) {
            for (int i = 0; i<data.Count; ++i) {
                progress?.Report((double)((numOfRepeats+1) * (i+1)) / data.Count * 10 );

                var position = data.FindIndex(0, o => o.OriginalPosition == i);
                bool isMovingRight = (data[position].Value >= 0);

                var item = data[position];
                // This is the most important step:
                // When we move by data.Count - 1 steps left or right,
                // we actually end up at the exact same relative configuration
                // This simple operation, allows us to handle very large numbers
                var amountToMoveRemaining = Math.Abs(item.Value) % (data.Count - 1);
                
                if (isMovingRight) {
                    while (amountToMoveRemaining >= data.Count - position) {
                        // We'll be moving over the bounds
                        data.RemoveAt(position);
                        data.Add(item);

                        // Now that the item is at the end, swap it with first element to properly handle bounds
                        data.Swap(0,data.Count - 1);
                        amountToMoveRemaining -= data.Count - position;
                        position = 0;
                        item = data[position];
                    }

                    data.RemoveAt(position);
                    data.Insert(position + (int)amountToMoveRemaining, item);
                } else {
                    while (position - amountToMoveRemaining < 0) {
                        // We'll be moving over the bounds
                        data.RemoveAt(position);
                        data.Insert(0,item);

                        // Now that the item is at the end, swap it with first element to properly handle bounds
                        data.Swap(0,data.Count - 1);
                        amountToMoveRemaining -= position + 1;
                        position = data.Count - 1;
                        item = data[position];
                    }

                    data.RemoveAt(position);
                    data.Insert(position - (int)amountToMoveRemaining, item);
                }
            }
        }

        var ZeroPos = data.FindIndex(o => o.Value == 0);

        long getNumberAtPos(int pos) {
            return data![(ZeroPos + pos) % data.Count].Value;
        }

        return getNumberAtPos(1000) + getNumberAtPos(2000) + getNumberAtPos(3000);
    }
}



public static class Extensions {
    public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
        return list;
    }
}
