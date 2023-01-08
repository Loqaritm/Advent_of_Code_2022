namespace AOC_2022_Test;

[TestClass]
public class Test_Day22 {
    private readonly string testData = @"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5";

    [TestMethod]
    public void TestParseData() {
        var sut = new AOC_2022.Day22.Part1.Day22_1();
        var data = sut.ParseData(testData);
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day22.Part1.Day22_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(6032, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        // N/A because part 2 was implemented as specific to my data
    }
}