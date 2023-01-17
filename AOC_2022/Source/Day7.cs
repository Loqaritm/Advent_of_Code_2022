namespace AOC_2022.Day7;

public class TreeNode {
    public enum FileFolderType {
        Folder,
        File
    }

    private List<TreeNode> _mChildNodes = new List<TreeNode>();
    private int _mFileSize = 0;
    private TreeNode? _mParentNode;

    public TreeNode(TreeNode? parentNode, string name, FileFolderType type, int size)
    {
        _mParentNode = parentNode;
        Type = type;
        _mFileSize = size;
        Name = name;
    }

    public string Name {get;}
    public FileFolderType Type {get;}

    // Return file size else compute folder size
    public int Size => (Type == FileFolderType.File) ? _mFileSize : _mChildNodes.Sum(o => o.Size);


    public void AddChild(TreeNode treeNode) {
        _mChildNodes.Add(treeNode);
    }

    public TreeNode MoveTo(string name) {
        if (name == "..") {
            return _mParentNode ?? throw new ArgumentException($"No parent folder");
        }
        return _mChildNodes.Find(o => o.Name == name) ?? throw new ArgumentException($"Name {name} is not in children");
    }

    public string PrintTree(int level = 0) {
        var returnVal =  new String(' ', level * 2);
        returnVal += $"- {Name} ";
        if (Type == FileFolderType.File) returnVal += $"(file, size={Size})\n";
        else {
            returnVal += $"(dir)\n";
            returnVal += String.Join(string.Empty, _mChildNodes.Select(o => o.PrintTree(level + 1)));
        }
        return returnVal;
    }

    public List<int> GetListOfContainedFolderSizes()
    {
        if (Type == FileFolderType.File) {
            return new List<int>();
        }

        var returnVal = new List<int>{
            Size 
        };
        _mChildNodes.ForEach(o=> returnVal = returnVal.Concat(o.GetListOfContainedFolderSizes()).ToList());
        return returnVal;
    }
}

public class Day7_1 : IDayPart<TreeNode, int>
{
    public int DayNumber => 7;

    public TreeNode ParseData(string data)
    {
        // Leave the root so it's easy to return to
        var rootNode = new TreeNode(null, "/", TreeNode.FileFolderType.Folder, 0);

        var currentNode = rootNode;

        data.Split(Environment.NewLine).ToList().ForEach(line => {
            if (line.First() == '$') {
                // Command
                if (line == "$ ls") {
                    // Do nothing
                    return;
                } else if (line.Substring(0,6) == "$ cd /") {
                    currentNode = rootNode;
                } else if (line.Substring(0,5) == "$ cd ") {
                    currentNode = currentNode.MoveTo(line.Substring(5));
                }
            } else {
                if (line.Substring(0,3) == "dir") {
                    currentNode.AddChild(new TreeNode(currentNode, line.Substring(4), TreeNode.FileFolderType.Folder, 0));
                } else {
                    var splitLine = line.Split(" ");
                    currentNode.AddChild(new TreeNode(currentNode, splitLine[1], TreeNode.FileFolderType.File, int.Parse(splitLine[0])));
                }
            }
        });

        return rootNode;
    }

    public int RunInternal(TreeNode data, ProgressBar? progress = null)
    {
        var maxSizeOfDir = 100000;
        return data.GetListOfContainedFolderSizes().Sum(o => (o > maxSizeOfDir) ? 0 : o);
    }
}

public class Day7_2 : Day7_1, IDayPart<TreeNode, int> {
    public new int RunInternal(TreeNode data, ProgressBar? progress = null)
    {
        var TOTAL_DISK_SPACE = 70000000;
        var SPACE_NECESSARY = 30000000;

        var currentFreeSpace = TOTAL_DISK_SPACE - data.Size;
        var freeSpaceNeeded = SPACE_NECESSARY - currentFreeSpace;

        return data.GetListOfContainedFolderSizes().FindAll(o => o >= freeSpaceNeeded).Min();
    }
}
