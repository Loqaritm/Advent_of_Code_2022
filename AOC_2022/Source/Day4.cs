namespace AOC_2022.Day4;

public class ElfRange {
    public readonly int StartPoint;
    public readonly int EndPoint;

    public ElfRange(int startPoint, int endPoint) {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    public bool FullyOverwraps(ElfRange other) {
        return StartPoint <= other.StartPoint && EndPoint >= other.EndPoint;
    }

    public bool Overlaps(ElfRange other) {
        return EndPoint >= other.StartPoint && StartPoint <= other.EndPoint;
    }
}

public class ElfPair {
    public ElfRange Elf1Range;
    public ElfRange Elf2Range;

    public ElfPair(ElfRange elf1range, ElfRange elf2range) {
        Elf1Range = elf1range;
        Elf2Range = elf2range;
    }
}

public class Day4_1 : IDayPart<List<ElfPair>, int>
{
    public int DayNumber => 4;

    public List<ElfPair> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o=> {
            var elves = o.Split(',').Select(x => {
                var range = x.Split('-').Select(int.Parse).ToList();
                return new ElfRange(range[0], range[1]);
            }).ToList();
            return new ElfPair(elves[0], elves[1]);
        }).ToList();
    }

    public int RunInternal(List<ElfPair> data, ProgressBar? progress = null)
    {
        return data.Sum(o => { return (o.Elf1Range.FullyOverwraps(o.Elf2Range) || o.Elf2Range.FullyOverwraps(o.Elf1Range)) ? 1 : 0; });
    }
}


public class Day4_2 : IDayPart<List<ElfPair>, int>
{
    public int DayNumber => 4;

    public List<ElfPair> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o=> {
            var elves = o.Split(',').Select(x => {
                var range = x.Split('-').Select(int.Parse).ToList();
                return new ElfRange(range[0], range[1]);
            }).ToList();
            return new ElfPair(elves[0], elves[1]);
        }).ToList();
    }

    public int RunInternal(List<ElfPair> data, ProgressBar? progress = null)
    {
        return data.Sum(o => { return (o.Elf1Range.Overlaps(o.Elf2Range)) ? 1 : 0; });
    }
}
