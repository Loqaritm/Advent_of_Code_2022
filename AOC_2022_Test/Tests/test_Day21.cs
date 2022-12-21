namespace AOC_2022_Test;

[TestClass]
public class Test_Day21 {
    private readonly string testData = @"root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day21.Day21_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(15, data.Count());
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day21.Day21_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(152, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
    }
}