namespace AOC_2022_Test;

[TestClass]
public class test_Day1 {
    private readonly string testData = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day1_1();
        Assert.AreEqual(5, sut.ParseData(testData).Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day1_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(24000, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day1_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(45000 , sut.RunInternal(data));
    }

}