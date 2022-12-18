namespace AOC_2022_Test;

[TestClass]
public class Test_Day03 {
    private readonly string testData = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day3_1();
        Assert.AreEqual(6, sut.ParseData(testData).Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day3_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(157, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day3_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(70, sut.RunInternal(data));
    }
}