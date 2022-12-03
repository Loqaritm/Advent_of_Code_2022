namespace AOC_2022;


public class Rucksack {
    public readonly List<char> Compartment1;
    public readonly List<char> Compartment2;
    public readonly List<char> BothCompartments;

    public Rucksack(string data) {
        var listData = data.ToList();
        Compartment1 = listData.Take(listData.Count / 2).ToList();
        Compartment2 = listData.Skip(listData.Count / 2).ToList();
        BothCompartments = listData;
    }
}

public class Day3_1 : IDayPart<List<Rucksack>, int>
{
    public string DataFileName => "Day3.txt";

    public List<Rucksack> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o=> new Rucksack(o)).ToList();
    }

    public int RunInternal(List<Rucksack> data, ProgressBar? progress = null)
    {
        return data.Sum(o => {
            var commonList = o.Compartment1.Intersect(o.Compartment2);
            return commonList.Sum(x => {
                var castVal = (int)x;
                if (castVal <= (int)'Z') { castVal -= (int)'A'; castVal += 27; }
                else {
                    castVal -= (int)'a';
                    castVal += 1;
                }
                return castVal;
            });
        });
    }
}




public class Day3_2 : IDayPart<List<Rucksack>, int>
{
    public string DataFileName => "Day3.txt";

    public List<Rucksack> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o=> new Rucksack(o)).ToList();
    }

    public int RunInternal(List<Rucksack> data, ProgressBar? progress = null)
    {
        return data.Partition(3).Sum(group=> {
            var x = group[0].BothCompartments.Intersect(group[1].BothCompartments).Intersect(group[2].BothCompartments).ToList().First();
            var castVal = (int)x;
            if (castVal <= (int)'Z') { castVal -= (int)'A'; castVal += 27; }
            else {
                castVal -= (int)'a';
                castVal += 1;
            }
            return castVal;
        });
    }
}


public static class Extensions
{
    public static List<List<T>> Partition<T>(this List<T> values, int chunkSize)
    {
        return values.Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
    }
}