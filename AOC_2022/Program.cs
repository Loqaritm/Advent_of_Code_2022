using System;
using System.Threading;

namespace AOC_2022
{

static class Program {

	static void Main() {
		Console.Write("Performing some task... ");
		using (var progress = new ProgressBar()) {
			for (int i = 0; i <= 100; i++) {
				progress.Report((double) i / 100);
				Thread.Sleep(20);
			}
		}
		Console.WriteLine("Done.");
	}

}
}