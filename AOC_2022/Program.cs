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
        IDayPart<List<Day4.ElfPair>, int> task = new Day4.Day4_2();

        task.Run();
	}

}