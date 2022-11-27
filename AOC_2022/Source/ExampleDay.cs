using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2022
{
    public class ExampleDay : IDay<List<int>, int>
    {
        public List<int> ParseData(string data)
        {
            return data.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse).ToList();
        }

        public void Run()
        {
            Console.WriteLine("Running Day1...");
            var returnValue = 0;
            using (var progress = new ProgressBar())
            {
                var data = System.IO.File.ReadAllText($"{CommonDefs.ResourcePath}/ExampleDay.txt");
                var parsedData = ParseData(data);
                // returnValue = RunPart1(parsedData, progress);
                returnValue = RunPart2(parsedData, progress);
            }
            Console.WriteLine($"The value is: {returnValue}");
        }

        public int RunPart1(List<int> data, ProgressBar? progress = null)
        {
            int? previousVal = null;
            var countOfBiggerValues = 0;
            data.ForEach(o => {
                if (previousVal is not null && o > previousVal) countOfBiggerValues++;
                previousVal = o;
            });

            return countOfBiggerValues;
        }

        public int RunPart2(List<int> data, ProgressBar? progress = null)
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
}