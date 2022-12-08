namespace AOC_2022.Day8;

public class Forest {
    public class Tree {
        public int Height;
        public bool WasSeen;

        public Tree(int height, bool wasSeen)
        {
            Height = height;
            WasSeen = wasSeen;
        }
    }


    public List<List<Tree>> TreeGrid {get;}

    public Forest(List<List<int>> treeGrid) {
        TreeGrid = treeGrid.Select(row => {
            return row.Select(item => new Tree(item, false)).ToList();
        }).ToList();
    }

    public int ComputeScoreForTreeAt(int row, int column) {
        var score = 0;
        var currentHeight = TreeGrid[row][column].Height;
        
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

public class Day8_1 : IDayPart<Forest, int>
{
    public string DataFileName => "Day8.txt";

    public Forest ParseData(string data)
    {
        return new Forest(data.Split(Environment.NewLine).Select(o => o.ToList().Select(x => int.Parse(x.ToString())).ToList()).ToList());
    }

    public int RunInternal(Forest data, ProgressBar? progress = null)
    {
        return data.NumberOfVisibleTreesFromOutside;
    }
} 