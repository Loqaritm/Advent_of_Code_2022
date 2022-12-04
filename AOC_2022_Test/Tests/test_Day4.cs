namespace AOC_2022_Test;

[TestClass]
public class Test_Day4 {
    private readonly string testData = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day4.Day4_1();
        Assert.AreEqual(6, sut.ParseData(testData).Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day4.Day4_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(2, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day4.Day4_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(4, sut.RunInternal(data));
    }
}