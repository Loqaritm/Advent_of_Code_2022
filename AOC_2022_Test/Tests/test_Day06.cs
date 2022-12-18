namespace AOC_2022_Test;

[TestClass]
public class Test_Day06 {
    private readonly string testData = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb";
    private readonly string testData2 = @"bvwbjplbgvbhsrlpgdmjqwftvncz";
    private readonly string testData3 = @"nppdvjthqldpwncqszvftbrmjlhg";
    private readonly string testData4 = @"nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
    private readonly string testData5 = @"zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day6.Day6_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(data, testData);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day6.Day6_1();
        Assert.AreEqual(7, sut.RunInternal(sut.ParseData(testData)));
        Assert.AreEqual(5, sut.RunInternal(sut.ParseData(testData2)));
        Assert.AreEqual(6, sut.RunInternal(sut.ParseData(testData3)));
        Assert.AreEqual(10, sut.RunInternal(sut.ParseData(testData4)));
        Assert.AreEqual(11, sut.RunInternal(sut.ParseData(testData5)));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day6.Day6_2();
        Assert.AreEqual(19, sut.RunInternal(sut.ParseData(testData)));
        Assert.AreEqual(23, sut.RunInternal(sut.ParseData(testData2)));
        Assert.AreEqual(23, sut.RunInternal(sut.ParseData(testData3)));
        Assert.AreEqual(29, sut.RunInternal(sut.ParseData(testData4)));
        Assert.AreEqual(26, sut.RunInternal(sut.ParseData(testData5)));
    }
}