namespace AOC_2022_Test;

[TestClass]
public class Test_Day09 {
    private readonly string testData = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

    private readonly string testData2 = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day9.Day9_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(8, data.Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day9.Day9_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(13, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day9.Day9_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(1, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2_moreComplicatedData() {
        var sut = new AOC_2022.Day9.Day9_2();
        var data = sut.ParseData(testData2);
        Assert.AreEqual(36, sut.RunInternal(data));
    }
}