namespace AOC_2022_Test;

[TestClass]
public class Test_Day7 {
    private readonly string testData = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

    [TestMethod]
    public void TestParseData() {

        var expectedOutput = @"- / (dir)
  - a (dir)
    - e (dir)
      - i (file, size=584)
    - f (file, size=29116)
    - g (file, size=2557)
    - h.lst (file, size=62596)
  - b.txt (file, size=14848514)
  - c.dat (file, size=8504156)
  - d (dir)
    - j (file, size=4060174)
    - d.log (file, size=8033020)
    - d.ext (file, size=5626152)
    - k (file, size=7214296)
";

        var sut = new AOC_2022.Day7.Day7_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(expectedOutput, data.PrintTree());
    }

    [TestMethod]
    public void TestPart1() {
        var sut = new AOC_2022.Day7.Day7_1();
        var data = sut.ParseData(testData);
        Assert.AreEqual(95437, sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2() {
        var sut = new AOC_2022.Day7.Day7_2();
        var data = sut.ParseData(testData);
        Assert.AreEqual(24933642, sut.RunInternal(data));
    }
}