using System.Diagnostics;

namespace AOC_2022;

static class Program {

	static void Main() {
        // IDayPart<List<int>, int> task = new ExampleDay_1();
        // IDayPart<List<int>, int> task = new ExampleDay_2();
        // IDayPart<List<AOC_2022.CaloricElfUnit>, int> task = new Day1_1();
        // IDayPart<List<AOC_2022.CaloricElf>, int> task = new Day1_2();
        // IDayPart<List<AOC_2022.PlayData>, int> task = new Day2_1();
        // IDayPart<List<AOC_2022.PlayData2>, int> task = new Day2_2();
        // IDayPart<List<AOC_2022.Rucksack1>, int> task = new Day3_1();
        // IDayPart<List<AOC_2022.Rucksack>, int> task = new Day3_2();
        // IDayPart<List<Day4.ElfPair>, int> task = new Day4.Day4_1();
        // IDayPart<List<Day4.ElfPair>, int> task = new Day4.Day4_2();
        // IDayPart<Day5.DataFormat, string> task = new Day5.Day5_1();
        // IDayPart<Day5.DataFormat, string> task = new Day5.Day5_2();
        // IDayPart<string, int> task = new Day6.Day6_1();
        // IDayPart<string, int> task = new Day6.Day6_2();
        // IDayPart<Day7.TreeNode, int> task = new Day7.Day7_1();
        // IDayPart<Day7.TreeNode, int> task = new Day7.Day7_2();
        // IDayPart<Day8.Forest, int> task = new Day8.Day8_1();
        // IDayPart<Day8.Forest, int> task = new Day8.Day8_2();
        // IDayPart<List<Day9.Move>, int> task = new Day9.Day9_1();
        // IDayPart<List<Day9.Move>, int> task = new Day9.Day9_2();
        // IDayPart<List<Day10.Instruction>, int> task = new Day10.Day10_1();
        // IDayPart<List<Day10.Instruction>, string> task = new Day10.Day10_2();
        // IDayPart<List<Day11.Monkey>, int> task = new Day11.Day11_1();
        // IDayPart<List<Day11.Monkey>, long> task = new Day11.Day11_2();
        // IDayPart<Day17.MoveProvider, int> task = new Day17.Day17_1();
        IDayPart<Day17.MoveProvider, long> task = new Day17.Day17_2(1000000000000);

        task.Run();
	}

}