namespace AOC_2022_Test;

[TestClass]
public class Test_Day18 {
    private readonly string simpleData = @"1,1,1
2,1,1";
    private readonly string testData = @"2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5";

    [TestMethod]
    public void TestParseSimpleData() {
        var sut = new AOC_2022.Day18.Day18_1();
        var data = sut.ParseData(simpleData);
        Assert.AreEqual(2, data.Count());
    }
    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day18.Day18_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(13, data.Count());
    }

    [TestMethod]
    public void TestPart1_Simple() {
        var sut = new AOC_2022.Day18.Day18_1();
        var data = sut.ParseData(simpleData);
        Assert.AreEqual(10, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day18.Day18_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(64, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day18.Day18_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(58, sut.RunInternal(data));
    }
}