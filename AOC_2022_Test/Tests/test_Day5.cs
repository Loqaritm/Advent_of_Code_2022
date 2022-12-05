namespace AOC_2022_Test;

[TestClass]
public class Test_Day5 {
    private readonly string testData = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day5.Day5_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(3, data.Towers.Count);
        Assert.AreEqual(4, data.Instructions.Count);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day5.Day5_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual("CMZ", sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day5.Day5_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual("MCD", sut.RunInternal(data));
    }
}