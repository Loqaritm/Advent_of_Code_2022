namespace AOC_2022;

public struct CaloricElf {
    public readonly List<int> _mCalories;
    public CaloricElf(List<int> calories) {
        this._mCalories = calories;
    }
    public int TotalCalories => _mCalories.Sum();
}

public class Day1_1 : IDayPart<List<CaloricElf>, int>
{
    public string DataFileName => "Day1.txt";

    public List<CaloricElf> ParseData(string data)
    {
        return data.Split(Environment.NewLine + Environment.NewLine,StringSplitOptions.RemoveEmptyEntries).ToList().Select(o=>
            {
                var splitCalories = o.Split(Environment.NewLine).ToList().Select(int.Parse).ToList();
                return new CaloricElf(splitCalories);
            }
        ).ToList();
    }

    public int RunInternal(List<CaloricElf> data, ProgressBar? progress = null)
    {
        CaloricElf biggest = data.Aggregate((i1,i2) => i1.TotalCalories > i2.TotalCalories ? i1 : i2);

        return biggest.TotalCalories;
    }
}

public class Day1_2 : IDayPart<List<CaloricElf>, int>
{
    public string DataFileName => "Day1.txt";

    public List<CaloricElf> ParseData(string data)
    {
        return data.Split(Environment.NewLine + Environment.NewLine,StringSplitOptions.RemoveEmptyEntries).ToList().Select(o=>
            {
                var splitCalories = o.Split(Environment.NewLine).ToList().Select(int.Parse).ToList();
                return new CaloricElf(splitCalories);
            }
        ).ToList();
    }

    public int RunInternal(List<CaloricElf> data, ProgressBar? progress = null)
    {
        List<CaloricElf> biggest = data.OrderByDescending(o => o.TotalCalories).ToList();

        // Return sum of 3 biggest
        return  biggest.Take(3).Sum(o=> o.TotalCalories);
    }
}
