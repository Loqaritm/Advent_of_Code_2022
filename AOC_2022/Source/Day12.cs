// namespace AOC_2022.Day12;

// public class Node {
//     public int xPos;
//     public int yPos;
//     public char heightChar;
//     public int distance;

//     public Node(int xPos, int yPos, char heightChar, int distance)
//     {
//         this.xPos = xPos;
//         this.yPos = yPos;
//         this.heightChar = heightChar;
//         this.distance = distance;
//     }

//     public bool isAdjecent(Node other) {
//         return Math.Abs(xPos - other.xPos) <= 1 && Math.Abs(yPos - other.yPos) <= 1;
//     }

// }

// public class Day12_1 : IDayPart<List<Node>, int>
// {
//     public string DataFileName => "Day12.txt";

//     public List<Node> ParseData(string data)
//     {
//         var xPos = 0;
//         var yPos = 0;
//         return data.Split(Environment.NewLine).SelectMany(line => {
//             xPos++;
//             yPos = 1;
//             return line.ToList().Select(o => new Node(xPos, yPos++, o, int.MaxValue));
//         }).ToList();
//     }

//     public int RunInternal(List<Node> data, ProgressBar? progress = null)
//     {
//         var visited = new List<Node>{data.Find(o => o.heightChar == 'S')!};
//         var toVisit = data.FindAll(o => o.heightChar != 'S');


//         throw new NotImplementedException();
//     }
// }
