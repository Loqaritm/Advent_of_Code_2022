namespace AOC_2022_Test;

[TestClass]
public class test_ExampleDay
{
    private readonly string exampleData = @"199
                            200
                            208
                            210
                            200
                            207
                            240
                            269
                            260
                            263";
    [TestMethod]
    public void TestParseData()
    {
        var sut = new AOC_2022.ExampleDay_1();
        CollectionAssert.AreEqual(new List<int>{199,200,208,210,200,207,240,269,260,263}, sut.ParseData(exampleData));
    }

    [TestMethod]
    public void TestPart1()
    {
        var sut = new AOC_2022.ExampleDay_1();
        var data = sut.ParseData(exampleData);
        Assert.AreEqual(7,sut.RunInternal(data));
    }

    [TestMethod]
    public void TestPart2()
    {
        var sut = new AOC_2022.ExampleDay_2();
        var data = sut.ParseData(exampleData);
        Assert.AreEqual(5, sut.RunInternal(data));
    }
}