namespace AOC_2022.Day6;

public class Day6_1 : IDayPart<string, int>
{
    public string DataFileName => "Day6.txt";

    public string ParseData(string data)
    {
        // No changes
        return data;
    }

    public int RunInternal(string data, ProgressBar? progress = null)
    {
        for (var startPoint = 0; startPoint < data.Length - 4; startPoint++) {
            if (data.Substring(startPoint, 4).ToList().Distinct().Count() == 4) {
                return startPoint + 4;
            }
        }

        throw new  NotSupportedException();
    }
}

public class Day6_2 : IDayPart<string, int>
{
    public string DataFileName => "Day6.txt";

    public string ParseData(string data)
    {
        // No changes
        return data;
    }

    public int RunInternal(string data, ProgressBar? progress = null)
    {
        for (var startPoint = 0; startPoint < data.Length - 14; startPoint++) {
            if (data.Substring(startPoint, 14).ToList().Distinct().Count() == 14) {
                return startPoint + 14;
            }
        }

        throw new  NotSupportedException();
    }
}
