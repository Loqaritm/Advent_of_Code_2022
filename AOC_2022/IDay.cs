namespace AOC_2022 {
    public interface IDay<DataType, ReturnType>
    {
        public DataType ParseData(string data);
        public ReturnType RunPart1(DataType data, ProgressBar? progress = null);
        public ReturnType RunPart2(DataType data, ProgressBar? progress = null);

        // Generic method to easily call this day from Main
        public void Run();
    }
}