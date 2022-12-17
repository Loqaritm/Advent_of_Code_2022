namespace AOC_2022_Test;

[TestClass]
public class Test_Day17 {
    private readonly string testData = @">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day17.Day17_1();
        var data = sut.ParseData(testData);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day17.Day17_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(3068, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day17.Day17_2();
        var data = sut.ParseData(testData);
        Console.WriteLine($"Running...");
        using (var progress = new ProgressBar()) {
            Assert.AreEqual(1000000000000, sut.RunInternal(data, progress));
        }
    }
}