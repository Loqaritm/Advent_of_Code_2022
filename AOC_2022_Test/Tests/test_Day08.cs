namespace AOC_2022_Test;

[TestClass]
public class Test_Day08 {
    private readonly string testData = @"30373
25512
65332
33549
35390";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day8.Day8_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(5, data.TreeGrid.Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day8.Day8_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(21, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day8.Day8_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(8, sut.RunInternal(data));
    }
}