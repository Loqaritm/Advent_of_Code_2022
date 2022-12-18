namespace AOC_2022_Test;

[TestClass]
public class Test_Day02 {
    private readonly string testData = @"A Y
B X
C Z";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day2_1();
        Assert.AreEqual(3, sut.ParseData(testData).Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day2_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(15, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day2_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(12, sut.RunInternal(data));
    }
}