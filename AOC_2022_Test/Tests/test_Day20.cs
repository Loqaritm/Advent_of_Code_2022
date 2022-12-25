namespace AOC_2022_Test;

[TestClass]
public class Test_Day20 {
    private readonly string testData = @"1
2
-3
3
-2
0
4";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day20.Day20_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(7, data.Count());
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day20.Day20_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(3, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day20.Day20_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(1623178306, sut.RunInternal(data));
    }
}