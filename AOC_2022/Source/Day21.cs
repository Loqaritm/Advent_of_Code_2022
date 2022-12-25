namespace AOC_2022.Day21.Part1 {
public abstract class Monkey {
    public abstract string Name {get;}
    public abstract long Value {get;}
};

public class OperationMonkey : Monkey
{
    public OperationMonkey(string name, OperationType operationType, List<string> monkeyNamesToWaitFor) {
        _mMonkeyName = name;
        _mOperationType = operationType;
        _mMonkeyNamesToWaitFor = monkeyNamesToWaitFor;
        _mMonkeysToWaitFor = new List<Monkey>();
    }

    public void SetupMonkey(List<Monkey> allMonkeyList) {
        _mMonkeysToWaitFor = new List<Monkey>();
        _mMonkeyNamesToWaitFor.ForEach(monkeyName => {
            _mMonkeysToWaitFor.Add(allMonkeyList.Find(o=> o.Name == monkeyName)!);
        });
    }

    public enum OperationType {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    private List<string> _mMonkeyNamesToWaitFor;
    private List<Monkey> _mMonkeysToWaitFor;
    private OperationType _mOperationType;

    private string _mMonkeyName;
    public override string Name => _mMonkeyName;

    private long? _mCachedValue;
    public override long Value {
        get {
            if (_mCachedValue is not null) { return _mCachedValue.Value; }
            _mCachedValue = _mOperationType switch {
                OperationType.Add => _mMonkeysToWaitFor[0].Value + _mMonkeysToWaitFor[1].Value,
                OperationType.Subtract => _mMonkeysToWaitFor[0].Value - _mMonkeysToWaitFor[1].Value,
                OperationType.Multiply => _mMonkeysToWaitFor[0].Value * _mMonkeysToWaitFor[1].Value,
                OperationType.Divide => _mMonkeysToWaitFor[0].Value / _mMonkeysToWaitFor[1].Value,
                _ => throw new NotSupportedException()
            };
            return _mCachedValue.Value;
        }
    }
}

public class ValueMonkey : Monkey
{
    private string _mMonkeyName;
    public override string Name => _mMonkeyName;

    private long _mValue;

    public ValueMonkey(string name, int value)
    {
        _mMonkeyName = name;
        _mValue = value;
    }

    public override long Value => _mValue;
}


public class Day21_1 : IDayPart<List<Monkey>, long>
{
    public string DataFileName => "Day21.txt";

    public List<Monkey> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select<string, Monkey>(line => {
            var parts = line.Split(": ");
            var number = 0;
            if (int.TryParse(parts[1], out number)) {
                return new ValueMonkey(parts[0], number);
            } else {
                var operationType = parts[1].ToList()[5] switch {
                    '+' => OperationMonkey.OperationType.Add,
                    '-' => OperationMonkey.OperationType.Subtract,
                    '*' => OperationMonkey.OperationType.Multiply,
                    '/' => OperationMonkey.OperationType.Divide,
                    _ => throw new NotSupportedException()
                };
                return new OperationMonkey(parts[0], operationType, new List<string>{parts[1].Substring(0, 4), parts[1].Substring(7,4)});
            }
        }).ToList();
    }

    public long RunInternal(List<Monkey> data, ProgressBar? progress = null)
    {
        // First we need to call setup on each operation monkey
        foreach (var monkey in data) {
            OperationMonkey? operationMonkey;
            if ((operationMonkey = monkey as OperationMonkey) is not null) {
                operationMonkey.SetupMonkey(data);
            }
        }

        return data.Find(o=>o.Name == "root")!.Value;
    }
}
}








namespace AOC_2022.Day21.Part2 {

public abstract class Monkey {
    public abstract string Name {get;}
    public abstract long? Value {get;}
    public abstract string GetStringRepresentation();
};

public class OperationMonkey : Monkey
{
    public OperationMonkey(string name, OperationType operationType, List<string> monkeyNamesToWaitFor) {
        _mMonkeyName = name;
        _mOperationType = operationType;
        _mMonkeyNamesToWaitFor = monkeyNamesToWaitFor;
        _mMonkeysToWaitFor = new List<Monkey>();
    }

    public void SetupMonkey(List<Monkey> allMonkeyList) {
        _mMonkeysToWaitFor = new List<Monkey>();
        _mMonkeyNamesToWaitFor.ForEach(monkeyName => {
            _mMonkeysToWaitFor.Add(allMonkeyList.Find(o=> o.Name == monkeyName)!);
        });
    }

    public enum OperationType {
        Add,
        Subtract,
        Multiply,
        Divide,
        Equal
    }
    private List<string> _mMonkeyNamesToWaitFor;
    private List<Monkey> _mMonkeysToWaitFor;
    private OperationType _mOperationType;

    private string _mMonkeyName;
    public override string Name => _mMonkeyName;

    private long? _mCachedValue;
    public override long? Value {
        get {
            if (_mCachedValue is not null) { return _mCachedValue.Value; }
            return _mCachedValue = _mOperationType switch {
                OperationType.Add => _mMonkeysToWaitFor[0].Value + _mMonkeysToWaitFor[1].Value,
                OperationType.Subtract => _mMonkeysToWaitFor[0].Value - _mMonkeysToWaitFor[1].Value,
                OperationType.Multiply => _mMonkeysToWaitFor[0].Value * _mMonkeysToWaitFor[1].Value,
                OperationType.Divide => _mMonkeysToWaitFor[0].Value / _mMonkeysToWaitFor[1].Value,
                _ => throw new NotSupportedException()
            };
        }
    }

    public override string GetStringRepresentation() {
        var symbol = _mOperationType switch {
            OperationType.Add => " + ",
            OperationType.Subtract => " - ",
            OperationType.Multiply => " * ",
            OperationType.Divide => " / ",
            OperationType.Equal => " = ",
            _ => throw new NotSupportedException()
        };

        return $"({_mMonkeysToWaitFor[0].GetStringRepresentation()}{symbol}{_mMonkeysToWaitFor[1].GetStringRepresentation()})";
    }
}

public class ValueMonkey : Monkey
{
    private string _mMonkeyName;
    public override string Name => _mMonkeyName;

    private long? _mValue;

    public ValueMonkey(string name, int? value)
    {
        _mMonkeyName = name;
        _mValue = value;
    }

    public override long? Value => _mValue;

    public override string GetStringRepresentation()
    {
        return (Value is null) ? "x" : Value.Value.ToString();
    }
}


public class Day21_2 : IDayPart<List<Monkey>, string>
{
    public string DataFileName => "Day21.txt";

    public List<Monkey> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select<string, Monkey>(line => {
            var parts = line.Split(": ");
            var number = 0;
            if (parts[0] == "humn") {
                // Need to return null here
                return new ValueMonkey(parts[0], null);
            }
            if (int.TryParse(parts[1], out number)) {
                return new ValueMonkey(parts[0], number);
            } else {
                var operationType = parts[1].ToList()[5] switch {
                    '+' => OperationMonkey.OperationType.Add,
                    '-' => OperationMonkey.OperationType.Subtract,
                    '*' => OperationMonkey.OperationType.Multiply,
                    '/' => OperationMonkey.OperationType.Divide,
                    _ => throw new NotSupportedException()
                };
                if (parts[0] == "root") {
                    operationType = OperationMonkey.OperationType.Equal;
                }
                return new OperationMonkey(parts[0], operationType, new List<string>{parts[1].Substring(0, 4), parts[1].Substring(7,4)});
            }
        }).ToList();
    }

    public string RunInternal(List<Monkey> data, ProgressBar? progress = null)
    {
        // First we need to call setup on each operation monkey
        foreach (var monkey in data) {
            OperationMonkey? operationMonkey;
            if ((operationMonkey = monkey as OperationMonkey) is not null) {
                operationMonkey.SetupMonkey(data);
            }
        }

        // Doing this the easiest way possible - using a proper tool for the job.
        // Online solver used to find x from the operation string representation:
        // https://quickmath.com/#form?v2=solve
        return data.Find(o => o.Name == "root")!.GetStringRepresentation();
    }
}

}