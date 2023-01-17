namespace AOC_2022.Day8;

public class Forest {
    public class Tree {
        public int Height;
        public bool WasSeen;
        public int RowPosition;
        public int ColPosition;

        public Tree(int height, bool wasSeen, int rowPosition, int colPosition) {
            Height = height;
            WasSeen = wasSeen;
            RowPosition = rowPosition;
            ColPosition = colPosition;
        }
    }


    public List<List<Tree>> TreeGrid {get;}

    public Forest(List<List<int>> treeGrid) {
        TreeGrid = treeGrid.Select((rowVal, rowIndex) => {
            return rowVal.Select((item, colIndex) => new Tree(item, false, rowIndex, colIndex)).ToList();
        }).ToList();
    }

    public int ComputeScenicScoreForTree(Tree tree) {
        var score = 1;
        int getTreeVisibilityRange(List<Tree> trees) {
            // First tree is our tree
            var currentTree = trees[0];
            var visibility = 0;
            foreach (var tree in trees.Skip(1)) {
                visibility++;
                if (tree.Height >= currentTree.Height) { break; }
            }
            return visibility;
        }

        var rowToCheck = TreeGrid[tree.RowPosition];
        var columnToCheck = TreeGrid.Select(o => o[tree.ColPosition]).ToList();

        var rangesToCheck = new List<List<Tree>>{
            Enumerable.Reverse(rowToCheck.Take(tree.ColPosition + 1)).ToList(),
            rowToCheck.Skip(tree.ColPosition).ToList(),
            Enumerable.Reverse(columnToCheck.Take(tree.RowPosition + 1)).ToList(),
            columnToCheck.Skip(tree.RowPosition).ToList()
        };
        
        foreach ( var rangeToCheck in rangesToCheck) {
            var partScore = getTreeVisibilityRange(rangeToCheck);
            if (partScore == 0) { return 0; }
            score *= partScore;
        }

        return score;
    }


    public int NumberOfVisibleTreesFromOutside {
        get {
            var visibleTrees = 0;

            void AddVisibleTreeForRowsFromLeft(List<Tree> row) {
                int? maxTree = null;
                row.ForEach(item => {
                    if (maxTree is null || maxTree < item.Height) {
                        maxTree = item.Height;
                        if (!item.WasSeen) {
                            item.WasSeen = true;
                            visibleTrees++;
                        }
                    }
                });
            }

            TreeGrid.ForEach(AddVisibleTreeForRowsFromLeft);
            TreeGrid.Select(o=> Enumerable.Reverse(o).ToList()).ToList().ForEach(AddVisibleTreeForRowsFromLeft);
            
            var rotatedGrid = TreeGrid.Pivot().Select(o=> o.ToList()).ToList();
            rotatedGrid.ForEach(AddVisibleTreeForRowsFromLeft);
            rotatedGrid.Select(o=> Enumerable.Reverse(o).ToList()).ToList().ForEach(AddVisibleTreeForRowsFromLeft);

            return visibleTrees;
        }
    }

    
}

public class Day8_1 : IDayPart<Forest, int>
{
    public int DayNumber => 8;

    public Forest ParseData(string data)
    {
        return new Forest(data.Split(Environment.NewLine).Select(o => o.ToList().Select(x => int.Parse(x.ToString())).ToList()).ToList());
    }

    public int RunInternal(Forest data, ProgressBar? progress = null)
    {
        return data.NumberOfVisibleTreesFromOutside;
    }
}

public class Day8_2 : Day8_1, IDayPart<Forest, int> {
    public new int RunInternal(Forest data, ProgressBar? progress = null)
    {
        return data.TreeGrid.Max(row => {
            return row.Max(item => data.ComputeScenicScoreForTree(item));
        });
    }
}




public static class IEnumerableExtensions {
    public static IEnumerable<IEnumerable<T>> Pivot<T>(this IEnumerable<IEnumerable<T>> source)
    {
        var enumerators = source.Select(e => e.GetEnumerator()).ToList();
        try
        {
            while (enumerators.All(e => e.MoveNext()))
            {
                yield return enumerators.Select(e => e.Current).ToList();
            }
        }
        finally
        {
            enumerators.ForEach(e => e.Dispose());
        }
    }
}