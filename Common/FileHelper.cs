namespace AoC.Common;

public static class FileHelper
{
    public static string[] GetLines(string path, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    {
        using (var sr = new StreamReader(path))
        {
            return sr.ReadToEnd()
                .Split(
                    Environment.NewLine,
                    splitOptions
                );
        }
    }
}