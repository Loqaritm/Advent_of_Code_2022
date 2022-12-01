using System.Diagnostics;

namespace AOC_2022 {
    /// Summary:
    /// Represents only one part of a given AOC challenge.
    /// I have decided to split it like this, even if it leads to code dupliation
    /// because it will be interesting to see how much more complicated the
    /// part 2 solutions are.
    public interface IDayPart<DataType, ReturnType>
    {
        // Default to just class name of whoever is implementing this interface
        public string Name => this.GetType().Name;
        public string DataFileName {get;}
        public DataType ParseData(string data);
        public ReturnType RunInternal(DataType data, ProgressBar? progress = null);

        // Generic method to easily call this day from Main
        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine($"Running {Name}...");
            ReturnType returnValue;
            using (var progress = new ProgressBar())
            {
                var data = System.IO.File.ReadAllText($"{CommonDefs.ResourcePath}/{DataFileName}");
                var parsedData = ParseData(data);
                returnValue = RunInternal(parsedData, progress);
            }
            Console.WriteLine($"The value is: {returnValue}");

            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        }
    }
}