using System.Net;

namespace AOC_2022;

// Description:
// Handles reading the input and downloading it if necessary.
public class InputHelper {
    public static InputHelper ForYear(string year) {
        return new InputHelper(year);
    }

    private string _mYear;
    private InputHelper(string year) {
        _mYear = year;
    }
    public async Task<string> GetInput(int dayNumber) {
        // Use cached data. Do not bombard Eric's servers with redundant requests.
        if (!File.Exists($"{CommonDefs.ResourcePath}/Day{dayNumber}.txt")) {
            using (var client = new HttpClient())
            {
                Console.WriteLine($"Downloading input for day {dayNumber}");
                client.DefaultRequestHeaders.Add("cookie", $"session={SessionCookie.Value}");

                await HttpClientUtils.DownloadFileTaskAsync(client, new Uri($"https://adventofcode.com/{_mYear}/day/{dayNumber}/input"), $"{CommonDefs.ResourcePath}/Day{dayNumber}.txt");
            }
        }

        var readData = System.IO.File.ReadAllText($"{CommonDefs.ResourcePath}/Day{dayNumber}.txt");
        if (readData.EndsWith(Environment.NewLine)) {
            readData = readData.Remove(readData.Length - 1);
        }

        return readData;
    }
}

public static class HttpClientUtils
{
    public static async Task DownloadFileTaskAsync(this HttpClient client, Uri uri, string FileName)
    {
        using (var s = await client.GetStreamAsync(uri))
        {
            using (var fs = new FileStream(FileName, FileMode.CreateNew))
            {
                await s.CopyToAsync(fs);
            }
        }
    }
}