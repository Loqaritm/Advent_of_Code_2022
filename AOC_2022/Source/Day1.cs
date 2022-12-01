namespace AOC_2022;

public struct Elf {
    public readonly List<int> _mCalories;
    public Elf(List<int> calories) {
        this._mCalories = calories;
    }
    public int TotalCalories => _mCalories.Sum();
}

public class Day1_1 : IDayPart<List<Elf>, int>
{
    public string DataFileName => "Day1.txt";

    public List<Elf> ParseData(string data)
    {
        var elves = data.Split(Environment.NewLine + Environment.NewLine,StringSplitOptions.RemoveEmptyEntries).ToList().Select(o=>
            {
                var splitCalories = o.Split(Environment.NewLine).ToList().Select(int.Parse).ToList();
                return new Elf(splitCalories);
            }
        ).ToList();
        return elves;
    }

    public int RunInternal(List<Elf> data, ProgressBar? progress = null)
    {
        Elf biggest = data.Aggregate((i1,i2) => i1.TotalCalories > i2.TotalCalories ? i1 : i2);

        return biggest.TotalCalories;
    }
}

public class Day1_2 : IDayPart<List<Elf>, int>
{
    public string DataFileName => "Day1.txt";

    public List<Elf> ParseData(string data)
    {
        var elves = data.Split(Environment.NewLine + Environment.NewLine,StringSplitOptions.RemoveEmptyEntries).ToList().Select(o=>
            {
                var splitCalories = o.Split(Environment.NewLine).ToList().Select(int.Parse).ToList();
                return new Elf(splitCalories);
            }
        ).ToList();
        return elves;
    }

    public int RunInternal(List<Elf> data, ProgressBar? progress = null)
    {
        List<Elf> biggest = data.OrderByDescending(o => o.TotalCalories).ToList();

        // Return sum of 3 biggest
        return  biggest.Take(3).Sum(o=> o.TotalCalories);
    }
}
