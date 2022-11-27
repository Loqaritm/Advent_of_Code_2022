using System.Diagnostics;

namespace AOC_2022;

static class Program {

	static void Main() {
        // IDayPart<List<int>, int> task = new ExampleDay_1();
        IDayPart<List<int>, int> task = new ExampleDay_2();

        task.Run();
	}

}