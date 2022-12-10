namespace AOC_2022.Day10;

public class Instruction {
    public enum InstructionType {
        noop,
        addx
    }
    public InstructionType Type;
    public int Value;
}

public class Day10_1 : IDayPart<List<Instruction>, int>
{
    public string DataFileName => "Day10.txt";

    public List<Instruction> ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o => {
            var splitLine = o.Split(' ');
            return new Instruction{Type = (Instruction.InstructionType)Enum.Parse(typeof(Instruction.InstructionType), splitLine[0]),
                                   Value = (splitLine.Length > 1) ? int.Parse(splitLine[1]) : 0};
        }).ToList();
    }

    public int RunInternal(List<Instruction> data, ProgressBar? progress = null)
    {
        bool isCycleToCheck(int cycleNum) {
            return (cycleNum - 20) % 40 == 0;
        }

        int computeScore(int cycleNum, int registerVal) {
            return cycleNum * registerVal;
        }

        var currentSumScores = 0;
        var xRegister = 1;
        var currentCycle = 1;
        
        void addScoreIfNecessary() {
            if (isCycleToCheck(currentCycle)) {
                currentSumScores += computeScore(currentCycle, xRegister);
            }
        }

        data.ForEach(instruction => {
            switch (instruction.Type)
            {
                case Instruction.InstructionType.addx:
                    currentCycle++;
                    addScoreIfNecessary();
                    currentCycle++;
                    xRegister += instruction.Value;
                    addScoreIfNecessary();
                    break;
                case Instruction.InstructionType.noop:
                    currentCycle++;
                    addScoreIfNecessary();
                    break;
            }
        });

        return currentSumScores;
    }
}

public class Day10_2 : Day10_1, IDayPart<List<Instruction>, string> {
    public new string RunInternal(List<Instruction> data, ProgressBar? progress = null)
    {
        var xRegister = 1;
        var currentCycle = 1;

        var stringToOutput = Environment.NewLine;
        
        void drawOnDisplay() {
            if (((currentCycle-1) %40) >= xRegister - 1 && ((currentCycle-1) %40) <= xRegister + 1) {
                stringToOutput += "#";
            }
            else {
                stringToOutput += ".";
            }
            if(currentCycle % 40 == 0) {
                stringToOutput += Environment.NewLine;
            }
        }

        data.ForEach(instruction => {
            switch (instruction.Type)
            {
                case Instruction.InstructionType.addx:
                    drawOnDisplay();
                    currentCycle++;
                    drawOnDisplay();
                    currentCycle++;
                    xRegister += instruction.Value;
                    break;
                case Instruction.InstructionType.noop:
                    drawOnDisplay();
                    currentCycle++;
                    break;
            }
        });

        return stringToOutput;
    }
}
