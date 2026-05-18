namespace AoC.Common;

public class SolutionVerifier
{
    public static void VerifyAndLog(string prefix, string? expected, string accual)
    {
        Console.Write($"{prefix}\t{accual}\t");

        if (expected is null)
        {
            System.Console.WriteLine("UNKNOWN");
        }
        else if (expected == accual)
        {
            System.Console.WriteLine("RIGHT");
        }
        else
        {
            System.Console.WriteLine("WRONG");
        }

    }
}