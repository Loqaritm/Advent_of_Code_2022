using System.Diagnostics;

namespace AOC_2022;

static class Program {

	static void Main() {
        // IDayPart<List<int>, int> task = new ExampleDay_1();
        // IDayPart<List<int>, int> task = new ExampleDay_2();
        // IDayPart<List<AOC_2022.Elf>, int> task = new Day1_1();
        IDayPart<List<AOC_2022.Elf>, int> task = new Day1_2();

        task.Run();
	}

}