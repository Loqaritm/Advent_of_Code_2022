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
        var sut = new AOC_2022.Day17.Day17_2(2022);
        var data = sut.ParseData(testData);
        Assert.AreEqual(3068, sut.RunInternal(data));

        sut = new AOC_2022.Day17.Day17_2(1000000000000);
        data = sut.ParseData(testData);
        // Assert.AreEqual(3068, sut.RunInternal(data));
        Assert.AreEqual(1514285714288, sut.RunInternal(data));
    }
}