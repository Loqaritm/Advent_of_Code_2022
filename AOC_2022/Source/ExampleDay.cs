using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2022;

public class ExampleDay_1 : IDayPart<List<int>, int>
{
    public string DataFileName => "ExampleDay.txt";

    public List<int> ParseData(string data)
    {
        return data.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse).ToList();
    }

    public int RunInternal(List<int> data, ProgressBar? progress = null)
    {
        int? previousVal = null;
        var countOfBiggerValues = 0;
        data.ForEach(o => {
            if (previousVal is not null && o > previousVal) countOfBiggerValues++;
            previousVal = o;
        });

        return countOfBiggerValues;
    }
}

public class ExampleDay_2 : IDayPart<List<int>, int>
{
    public string Name => nameof(ExampleDay_2);

    public string DataFileName => "ExampleDay.txt";

    public List<int> ParseData(string data)
    {
        return data.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse).ToList();
    }

    public int RunInternal(List<int> data, ProgressBar? progress = null)
    {
        var countOfBiggerValues = 0;

        var currentSum = 0;
        var previousSum = 0;

        for (int i = 0; i<data.Count(); ++i)
        {
            if (i<3) {
                previousSum += data[i];
                continue;
            }

            currentSum = data[i-1] + data[i-2] + data[i];
            if (currentSum > previousSum) { countOfBiggerValues++;}

            previousSum = currentSum;

            // Optionally report progress
            if (progress is not null) {progress.Report((double)i / data.Count());}
        }

        return countOfBiggerValues;
    }
}