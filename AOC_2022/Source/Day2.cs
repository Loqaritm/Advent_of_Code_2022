namespace AOC_2022;

using DataFormat = List<PlayData>;
using DataFormat2 = List<PlayData2>;

public struct PlayData {

    private Dictionary<string, int> _mScores = new Dictionary<string, int> () {
        {"X", 1},
        {"Y", 2},
        {"Z", 3}
    };
    private Dictionary<string, string> _mResponseWinningPlay = new Dictionary<string, string>() {
        {"A", "Y"},
        {"B", "Z"},
        {"C", "X"}
    };

    private Dictionary<string, string> _mDrawPlays = new Dictionary<string, string>() {
        {"A", "X"},
        {"B", "Y"},
        {"C", "Z"}
    };

    private string _mOpponentPlay;
    private string _mMyPlay;

    public PlayData(string oponnentPlay, string myPlay)
    {
        _mMyPlay = myPlay;
        _mOpponentPlay = oponnentPlay;
    }

    public int Score { get {
        var score = 0;
        if (_mMyPlay == _mResponseWinningPlay[_mOpponentPlay]) { score = 6;}
        else if (_mMyPlay == _mDrawPlays[_mOpponentPlay]) { score = 3; }
        score += _mScores[_mMyPlay];
        return score;
    }
    }
}

public class Day2_1 : IDayPart<DataFormat, int>
{
    public string DataFileName => "Day2.txt";

    public DataFormat ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o=> {
            var playPair = o.Split(" ").ToList();
            return new PlayData(playPair[0], playPair[1]);
        }).ToList();
    }

    public int RunInternal(DataFormat data, ProgressBar? progress = null)
    {
        return data.Sum(o => o.Score);
    }
}










public struct PlayData2 {

    private Dictionary<string, int> _mScores = new Dictionary<string, int> () {
        {"A", 1},
        {"B", 2},
        {"C", 3}
    };
    private Dictionary<string, string> _mResponseWinningPlay = new Dictionary<string, string>() {
        {"A", "B"},
        {"B", "C"},
        {"C", "A"}
    };
    private Dictionary<string, string> _mResponseLosingPlay = new Dictionary<string, string>() {
        {"A", "C"},
        {"B", "A"},
        {"C", "B"}
    };

    private string _mOpponentPlay;
    private string _mExpectedResult;

    public PlayData2(string oponnentPlay, string expectedResult)
    {
        _mExpectedResult = expectedResult;
        _mOpponentPlay = oponnentPlay;
    }

    public int Score { get {
        var score = 0;
        var myPlay = _mExpectedResult switch {
            "X" => _mResponseLosingPlay[_mOpponentPlay],
            "Y" => _mOpponentPlay,
            "Z" => _mResponseWinningPlay[_mOpponentPlay],
            _ => throw new NotSupportedException()
        };

        if (myPlay == _mResponseWinningPlay[_mOpponentPlay]) { score = 6;}
        else if (myPlay == _mOpponentPlay) { score = 3; }
        score += _mScores[myPlay];
        return score;
    }
    }
}

public class Day2_2 : IDayPart<DataFormat2, int>
{
    public string DataFileName => "Day2.txt";

    public DataFormat2 ParseData(string data)
    {
        return data.Split(Environment.NewLine).Select(o=> {
            var playPair = o.Split(" ").ToList();
            return new PlayData2(playPair[0], playPair[1]);
        }).ToList();
    }

    public int RunInternal(DataFormat2 data, ProgressBar? progress = null)
    {
        return data.Sum(o => o.Score);
    }
}
