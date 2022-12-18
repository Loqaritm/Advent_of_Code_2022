namespace AOC_2022.Day13;

public class Tree {
    public List<Tree> Children;
    // Null denominates root
    public int? Value;

    // Leaf constructor
    public Tree(int? value = null) {
        Value = value;
        Children = new List<Tree>();
    }

    // Subtree constructor
    public Tree(string data) {
        Children = new List<Tree>();
        // We need to try and find all nested [] structures
        while (true) {
            var encounteredBeginnings = 0;
            var beginningOfNested = data.IndexOf('[');
            if (beginningOfNested == -1) {
                // No more nested structs
                break;
            }

            // Get all normal leaves that were before this beginning
            var beforeFirstNested = data.Substring(0, beginningOfNested);
            beforeFirstNested.Split(",").ToList().ForEach(o => {
                if (o != "") { Children.Add(new Tree(int.Parse(o))); }
            });

            var endOfNested = -1;
            for (int i = beginningOfNested; i < data.Count(); ++i) {
                if (data[i] == '[') {
                    encounteredBeginnings++;
                } else if (data[i] == ']') {
                    encounteredBeginnings--;
                    if (encounteredBeginnings == 0) {
                        endOfNested = i;
                        break;
                    }
                }
            }
            // We got the outermost [] structure
            // Parse deeper recursively
            Children.Add(new Tree(data.Substring(beginningOfNested + 1, endOfNested - beginningOfNested - 1)));

            // Now we need to do this whole shebang again without this nested struct
            data = data.Substring(endOfNested + 1);
        }

        // Now that we've removed all nested structs, the rest needs to be added as simple leaves
        data.Split(",").ToList().ForEach(o => {
            if (o != "") {
                Children.Add(new Tree(int.Parse(o)));
            }
        });
    }

    public enum State {
        Lesser,
        Greater,
        Equal,
        Failed
    }

    public State IsLesserThanOtherTree(Tree other) {
        if (this.Value is not null && other.Value is not null) {
            if (this.Value < other.Value) {
                return State.Lesser;
            }
            else if (this.Value > other.Value) {
                return State.Greater;
            }
            return State.Equal;
        }
        if (this.Value is null && other.Value is null) {
            // Meaning both have child lists
            for (int i = 0; i < Children.Count(); ++i) {
                if (i >= other.Children.Count()) {
                    // Right list ran out of items
                    return State.Greater;
                }
                var result = Children[i].IsLesserThanOtherTree(other.Children[i]);
                // Still not sure, need to continue
                if (result == State.Equal) { continue; }
                return result;
            }
            if (this.Children.Count() < other.Children.Count()) {
                // This side ran out of items
                return State.Lesser;
            }
            return State.Equal;
        }
        if (this.Value is null && other.Value is not null) {
            var convertedToChild = new Tree();
            convertedToChild.Children.Add(new Tree(other.Value));
            return this.IsLesserThanOtherTree(convertedToChild);
        }
        if (this.Value is not null && other.Value is null) {
            var convertedToChild = new Tree();
            this.Children.Add(new Tree(this.Value));
            this.Value = null;
            return this.IsLesserThanOtherTree(other);
        }


        // This should never happen;
        throw new NotSupportedException();
    }
}

public class Packet {
    public Tree FirstTree;
    public Tree SecondTree;
    public int PacketNum;

    public Packet(int packetNum, Tree firstTree, Tree secondTree)
    {
        PacketNum = packetNum;
        FirstTree = firstTree;
        SecondTree = secondTree;
    }
}

public class Day13_1 : IDayPart<List<Packet>, int>
{
    public string DataFileName => "Day13.txt";

    public List<Packet> ParseData(string data)
    {
        int i = 1;
        return data.Split(Environment.NewLine + Environment.NewLine).Select(packetData => {
            var lines = packetData.Split(Environment.NewLine).ToList();
            return new Packet(i++, new Tree(lines[0]), new Tree(lines[1]));

        }).ToList();
    }

    public int RunInternal(List<Packet> data, ProgressBar? progress = null)
    {
        return data.Sum(packet => {
            var result = packet.FirstTree.IsLesserThanOtherTree(packet.SecondTree);
            return (result == Tree.State.Lesser) ? packet.PacketNum : 0;
        });
    }
}


public class Day13_2 : IDayPart<List<Tree>, int>
{
    public string DataFileName => "Day13.txt";

    public List<Tree> ParseData(string data)
    {
        return data.Split(Environment.NewLine + Environment.NewLine).SelectMany(packetData => {
            var lines = packetData.Split(Environment.NewLine).ToList();
            return new List<Tree>{new Tree(lines[0]), new Tree(lines[1])};

        }).ToList();
    }

    public int RunInternal(List<Tree> data, ProgressBar? progress = null)
    {
        // Parse the additional driver packets to trees
        var driverData = @"[[2]]
[[6]]";
        var driverTrees = this.ParseData(driverData);

        data = data.Concat(driverTrees).ToList();

        data.Sort(delegate(Tree first, Tree second) {
            var comparisonResult = first.IsLesserThanOtherTree(second);
            return comparisonResult switch {
                Tree.State.Lesser => -1,
                Tree.State.Greater => 1,
                Tree.State.Equal => 0,
                _ => throw new NotSupportedException()
            };
        });


        return (data.IndexOf(driverTrees[0]) + 1) * (data.IndexOf(driverTrees[1]) + 1);
    }
}