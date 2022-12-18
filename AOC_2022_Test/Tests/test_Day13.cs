namespace AOC_2022_Test;

[TestClass]
public class Test_Day13 {
    private readonly string testData = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day13.Day13_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(8, data.Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day13.Day13_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(13, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day13.Day13_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(140, sut.RunInternal(data));
    }
}