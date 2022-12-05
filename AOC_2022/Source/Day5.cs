using System.Text.RegularExpressions;

namespace AOC_2022.Day5;

public class DataFormat {
    public DataFormat(SortedDictionary<int, Tower> towers, List<Instruction> instructions)
    {
        Towers = towers;
        Instructions = instructions;
    }

    public class Tower {
        public List<string> Contents {get; set;} = new List<string>();

        public string? TopElement => Contents.FirstOrDefault();
    }

    public class Instruction {
        public int FromTowerNum;
        public int ToTowerNum;
        public int NumOfCrates;
    }

    public void HandleInstruction(Instruction instruction) {
        var actualToMove = Math.Min(instruction.NumOfCrates, Towers[instruction.FromTowerNum].Contents.Count);
        var cratesToMove = Towers[instruction.FromTowerNum].Contents.Take(actualToMove).ToList();
        Towers[instruction.FromTowerNum].Contents.RemoveRange(0, actualToMove);
        cratesToMove.ForEach(o => {
            Towers[instruction.ToTowerNum].Contents.Insert(0, o);
        });
    }

    public void HandleInstruction_2(Instruction instruction) {
        var actualToMove = Math.Min(instruction.NumOfCrates, Towers[instruction.FromTowerNum].Contents.Count);
        var cratesToMove = Towers[instruction.FromTowerNum].Contents.Take(actualToMove).ToList();
        Towers[instruction.FromTowerNum].Contents.RemoveRange(0, actualToMove);
        cratesToMove.Reverse();
        cratesToMove.ForEach(o => {
            Towers[instruction.ToTowerNum].Contents.Insert(0, o);
        });
    }

    public SortedDictionary<int, Tower> Towers {get;}
    public List<Instruction> Instructions {get;}
}

public class Day5_1 : IDayPart<DataFormat, string>
{
    public string DataFileName => "Day5.txt";

    public DataFormat ParseData(string data)
    {
        var parts = data.Split(Environment.NewLine + Environment.NewLine);
        var towersString = parts[0];
        var instructionsString = parts[1];

        // Towers
        SortedDictionary<int, DataFormat.Tower> towers = new SortedDictionary<int, DataFormat.Tower>();
        foreach (var towerString in towersString.Split(Environment.NewLine))
        {
            foreach (Match match in Regex.Matches(towerString, "[A-Z]")) {
                var towerIndex = (match.Index / 4) + 1;
                if (!towers.ContainsKey(towerIndex)) {
                    towers.Add(towerIndex, new DataFormat.Tower());
                }
                towers[towerIndex].Contents.Add(match.Value);
            }
        }

        // Instructions
        List<DataFormat.Instruction> instructions = instructionsString.Split(Environment.NewLine).Select(o=> {
            var matches = Regex.Matches(o, @"\d+");
            return new DataFormat.Instruction
            {
                NumOfCrates = int.Parse(matches[0].Value),
                FromTowerNum = int.Parse(matches[1].Value),
                ToTowerNum = int.Parse(matches[2].Value)
            };
        }).ToList();

        return new DataFormat(towers, instructions);
    }

    public string RunInternal(DataFormat data, ProgressBar? progress = null)
    {
        data.Instructions.ForEach(data.HandleInstruction);

        return String.Join("", data.Towers.Select(o=> o.Value.TopElement ?? ""));
    }
}

public class Day5_2 : Day5_1, IDayPart<DataFormat, string> {
    public new string RunInternal(DataFormat data, ProgressBar? progress = null) {
        data.Instructions.ForEach(data.HandleInstruction_2);

        return String.Join("", data.Towers.Select(o=> o.Value.TopElement ?? ""));
    }
}
