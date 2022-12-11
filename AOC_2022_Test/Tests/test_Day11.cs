namespace AOC_2022_Test;

[TestClass]
public class Test_Day11 {
    private readonly string testData = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day11.Day11_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(4, data.Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day11.Day11_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(10605, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day11.Day11_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(2713310158, sut.RunInternal(data));
    }
}