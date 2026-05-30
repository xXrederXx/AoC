namespace AoC.Common;

public static class CharHelper
{
    /// <summary>
    /// Convers a char to an int
    /// </summary>
    /// <param name="chr"></param>
    /// <returns></returns>
    public static int TryParseDigit(char chr)
    {
        return chr switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            _ => -1,
        };
    }
    public static int ParseDigit(char chr)
    {
        int num = TryParseDigit(chr);

        if(num == - 1)
        {
            throw new ArgumentException("Must be number 0-9", nameof(chr));
        }
        return num;
    }
}
