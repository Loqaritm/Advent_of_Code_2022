using System.Text.RegularExpressions;

namespace AOC_2022.Day11;

public class Monkey {
    public class Item {
        public int WorryLevel;

        public Item(int worryLevel)
        {
            WorryLevel = worryLevel;
        }
    }

    private Func<int, int> _mOperation;
    private Func<int, bool> _mTest;
    private int _mTrueMonkeyTarget;
    private int _mFalseMonkeyTarget;
    private int _mNumInspected = 0;

    public int NumInspected => _mNumInspected;
    public int MonkeyNum;
    public Queue<Item> Items;

    public Monkey(Func<int, int> mOperation, Func<int, bool> mTest, int mTrueMonkeyTarget, int mFalseMonkeyTarget, List<int> items, int monkeyNum)
    {
        _mOperation = mOperation;
        _mTest = mTest;
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

    public bool InspectFirstElement_2() {
        if (Items.Count == 0) {
            return false;
        }
        Items.Peek().WorryLevel = (int)_mOperation(Items.Peek().WorryLevel) % 100000000;
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
            Func<int,int> operation;
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
            regexResult = Regex.Match(lines[3], @"\d+");
            Func<int, bool> test = (argument) => argument % int.Parse(regexResult.Value) == 0;
            
            var monkeyToThrowTo1 = int.Parse(Regex.Match(lines[4], @"\d+").Value);
            var monkeyToThrowTo2 = int.Parse(Regex.Match(lines[5], @"\d+").Value);

            return new Monkey(operation, test, monkeyToThrowTo1, monkeyToThrowTo2, startingItems, monkeyNum);
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

        return data.OrderByDescending(o => o.NumInspected).Take(2).Select(o=>o.NumInspected).Aggregate((x,y) => x * y);
    }
}

public class Day11_2 : Day11_1, IDayPart<List<Monkey>, int> {
    public new int RunInternal(List<Monkey> data, ProgressBar? progress = null)
    {
        var NUM_OF_ROUNDS = 10000;
        for (var roundNumber = 1; roundNumber <= NUM_OF_ROUNDS; ++roundNumber) {
            progress?.Report((double)roundNumber / NUM_OF_ROUNDS);
            for (var monkeyNum = 0; monkeyNum < data.Count; ++monkeyNum) {
                while (data[monkeyNum].InspectFirstElement_2()) {
                    var monkeyNumToThrow = data[monkeyNum].GetMonkeyToThrowTo();
                    var itemToThrow = data[monkeyNum].Items.Dequeue();
                    data[monkeyNumToThrow].Items.Enqueue(itemToThrow);
                }
            }
        }

        return data.OrderByDescending(o => o.NumInspected).Take(2).Select(o=>o.NumInspected).Aggregate((x,y) => x * y);
    }
}