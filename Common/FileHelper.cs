namespace Common;

public static class FileHelper
{
    public static string[] GetLines(string path)
    {
        using (var sr = new StreamReader(path))
        {
            return sr.ReadToEnd()
                .Split(
                    Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                );
        }
    }
}