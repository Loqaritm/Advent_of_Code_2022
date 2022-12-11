using System.Text.RegularExpressions;

namespace AOC_2022.Day11;

public class Monkey {
    public class Item {
        public long WorryLevel;

        public Item(long worryLevel)
        {
            WorryLevel = worryLevel;
        }
    }

    private Func<long, long> _mOperation;
    private Func<long, bool> _mTest;

    // Useful for part 2
    public long Divisor;

    private int _mTrueMonkeyTarget;
    private int _mFalseMonkeyTarget;
    private int _mNumInspected = 0;

    public long NumInspected => _mNumInspected;
    public int MonkeyNum;
    public Queue<Item> Items;

    public Monkey(Func<long, long> mOperation, Func<long, bool> mTest, int mTrueMonkeyTarget, int mFalseMonkeyTarget, List<int> items, int monkeyNum, int divisor)
    {
        _mOperation = mOperation;
        _mTest = mTest;
        Divisor = divisor;
        _mTrueMonkeyTarget = mTrueMonkeyTarget;
        _mFalseMonkeyTarget = mFalseMonkeyTarget;
        Items = new Queue<Item>(items.Select(o=> new Item(o)).ToList());
        MonkeyNum = monkeyNum;
    }

    public bool InspectFirstElement() {
        if (Items.Count == 0) {
            return false;
        }
        Items.Peek().WorryLevel = (int)_mOperation(Items.Peek().WorryLevel) / 3;
        _mNumInspected++;
        return true;
    }

    public bool InspectFirstElement_2(long superModulo) {
        if (Items.Count == 0) {
            return false;
        }
        // This is basing on a fact that:
        // If n is divisible by P, so is n-kP
        // Let's have something as P:
        // if P = 2:
        // n-2k % 2 == 0
        // if P = 3;
        // n-3k % 3 == 0
        // extending this:
        // n-6k % 2 == 0 && n-6k % 3 == 0 also.
        //
        // So by extension n - k * productOfAllDivisors is also divisible by each of the divisors
        // Which basically boils down to n % productAfAllDivisors being divisible by each of the divisors
        // Not gonna lie, learned it from someone much smarter tham me apperently: u/1234abcdcba4321
        //
        // Anyway, this explains why we can just module the worry level by supermodulo
        Items.Peek().WorryLevel %= superModulo;
        Items.Peek().WorryLevel = _mOperation(Items.Peek().WorryLevel);
        _mNumInspected++;
        return true;
    }

    public int GetMonkeyToThrowTo() {
        return (_mTest(Items.Peek().WorryLevel)) ? _mTrueMonkeyTarget : _mFalseMonkeyTarget;
    }
}


public class Day11_1 : IDayPart<List<Monkey>, int>
{
    public string DataFileName => "Day11.txt";

    public List<Monkey> ParseData(string data)
    {
        return data.Split(Environment.NewLine + Environment.NewLine).ToList().Select(monkeyData => {
            var lines = monkeyData.Split(Environment.NewLine);
            var monkeyNum = int.Parse(Regex.Match(lines[0], @"\d+").Value);
            var startingItems = Regex.Matches(lines[1], @"\d+").OfType<Match>().Select(m => int.Parse(m.Value)).ToList();
            var symbol = lines[2].Substring(lines[2].IndexOf("old ") + 4,1);

            // Operation computation
            var regexResult = Regex.Match(lines[2], @"\d+");
            int? operationValue = (regexResult.Success) ? int.Parse(regexResult.Value) : null;
            Func<long,long> operation;
            if (symbol == "+") {
                if (operationValue is not null) {
                    operation = (argument) => argument + operationValue.Value;
                } else {
                    operation = (argument) => argument + argument;
                }
            } else {
                if (operationValue is not null) {
                    operation = (argument) => argument * operationValue.Value;
                } else {
                    operation = (argument) => argument * argument;
                }
            }

            // Test
            var divisor = int.Parse(Regex.Match(lines[3], @"\d+").Value);
            Func<long, bool> test = (argument) => argument % divisor == 0;
            
            var monkeyToThrowTo1 = int.Parse(Regex.Match(lines[4], @"\d+").Value);
            var monkeyToThrowTo2 = int.Parse(Regex.Match(lines[5], @"\d+").Value);

            return new Monkey(operation, test, monkeyToThrowTo1, monkeyToThrowTo2, startingItems, monkeyNum, divisor);
        }).ToList();
    }

    public int RunInternal(List<Monkey> data, ProgressBar? progress = null)
    {
        for (var roundNumber = 1; roundNumber <= 20; ++roundNumber) {
            for (var monkeyNum = 0; monkeyNum < data.Count; ++monkeyNum) {
                while (data[monkeyNum].InspectFirstElement()) {
                    var monkeyNumToThrow = data[monkeyNum].GetMonkeyToThrowTo();
                    var itemToThrow = data[monkeyNum].Items.Dequeue();
                    data[monkeyNumToThrow].Items.Enqueue(itemToThrow);
                }
            }
        }

        return (int)data.OrderByDescending(o => o.NumInspected).Take(2).Select(o=>o.NumInspected).Aggregate((x,y) => x * y);
    }
}

public class Day11_2 : Day11_1, IDayPart<List<Monkey>, long> {
    public new long RunInternal(List<Monkey> data, ProgressBar? progress = null)
    {
        var NUM_OF_ROUNDS = 10000;
        var productOfAllDivisors = data.Select(o=> o.Divisor).Aggregate((x,y) => x*y);
        for (var roundNumber = 1; roundNumber <= NUM_OF_ROUNDS; ++roundNumber) {
            progress?.Report((double)roundNumber / NUM_OF_ROUNDS);
            for (var monkeyNum = 0; monkeyNum < data.Count; ++monkeyNum) {
                while (data[monkeyNum].InspectFirstElement_2(productOfAllDivisors)) {
                    var monkeyNumToThrow = data[monkeyNum].GetMonkeyToThrowTo();
                    var itemToThrow = data[monkeyNum].Items.Dequeue();
                    data[monkeyNumToThrow].Items.Enqueue(itemToThrow);
                }
            }
        }

        return data.OrderByDescending(o => o.NumInspected).Take(2).Select(o=>o.NumInspected).Aggregate<long>((x,y) => x * y);
    }
}

// 9223372036854775807 - long value max
// 94083986096100 - max possible value of worry (after ^2)
// So should be ok.