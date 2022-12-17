namespace AOC_2022_Test;

[TestClass]
public class Test_Day14 {
    private readonly string testData = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day14.Day14_1();
        var data = sut.ParseData(testData);
    }

    [TestMethod]
    public void TestParseDataPart2() {
        var sut = new AOC_2022.Day14.Day14_2();
        var data = sut.ParseData(testData);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day14.Day14_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(24, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day14.Day14_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(93, sut.RunInternal(data));
    }
}