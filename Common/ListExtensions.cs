namespace AoC.Common;

public static class ListExtensions
{
    public static void AddOrUpdate<T>(
        this List<T> list,
        T value,
        Func<T, bool> match,
        Func<T, T> updateFunc)
    {
        int index = list.FindIndex(x => match(x));

        if (index >= 0)
        {
            list[index] = updateFunc(list[index]);
        }
        else
        {
            list.Add(value);
        }
    }
}
