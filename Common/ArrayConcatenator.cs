// Original Source: https://www.tutorialpedia.org/blog/how-do-i-concatenate-two-arrays-in-c/
// Modified as needed

namespace AoC.Common;

public static class ArrayConcatenator
{
    public static T[] ConcatArrays<T>(T[] array1, T[] array2)
    {
        // Validate inputs
        if (array1 == null) throw new ArgumentNullException(nameof(array1));
        if (array2 == null) throw new ArgumentNullException(nameof(array2));

        // Step 1: Calculate total length
        int totalLength = array1.Length + array2.Length;

        // Step 2: Allocate result array
        T[] result = new T[totalLength];

        // Step 3: Copy elements from array1 and array2
        Array.Copy(array1, 0, result, 0, array1.Length); // Copy array1 to result[0..array1.Length-1]
        Array.Copy(array2, 0, result, array1.Length, array2.Length); // Copy array2 to result[array1.Length..totalLength-1]

        return result;
    }
    public static T[] ConcatArrays<T>(T[] array1, T[] array2, T[] array3)
    {
        // Validate inputs
        if (array1 == null) throw new ArgumentNullException(nameof(array1));
        if (array2 == null) throw new ArgumentNullException(nameof(array2));
        if (array3 == null) throw new ArgumentNullException(nameof(array3));

        // Step 1: Calculate total length
        int totalLength = array1.Length + array2.Length + array3.Length;

        // Step 2: Allocate result array
        T[] result = new T[totalLength];

        // Step 3: Copy elements from array1 and array2
        Array.Copy(array1, 0, result, 0, array1.Length); // Copy array1 to result[0..array1.Length-1]
        Array.Copy(array2, 0, result, array1.Length, array2.Length); // Copy array2 to result[array1.Length..totalLength-1]
        Array.Copy(array3, 0, result, array1.Length+array2.Length, array3.Length); // Copy array3 to result[array1.Length+array2.Length..totalLength-1]

        return result;
    }

    public static T[] ConcatArrays<T>(T[] array1, T[] array2, T[] array3, T[] array4)
    {
        // Validate inputs
        if (array1 == null) throw new ArgumentNullException(nameof(array1));
        if (array2 == null) throw new ArgumentNullException(nameof(array2));
        if (array3 == null) throw new ArgumentNullException(nameof(array3));
        if (array4 == null) throw new ArgumentNullException(nameof(array4));

        // Step 1: Calculate total length
        int totalLength = array1.Length + array2.Length + array3.Length + array4.Length;

        // Step 2: Allocate result array
        T[] result = new T[totalLength];

        // Step 3: Copy elements from array1 and array2
        Array.Copy(array1, 0, result, 0, array1.Length); // Copy array1 to result[0..array1.Length-1]
        Array.Copy(array2, 0, result, array1.Length, array2.Length); // Copy array2 to result[array1.Length..totalLength-1]
        Array.Copy(array3, 0, result, array1.Length+array2.Length, array3.Length); // Copy array3 to result[array1.Length+array2.Length..totalLength-1]
        Array.Copy(array4, 0, result, array1.Length+array2.Length+array3.Length, array4.Length); // Copy array4 to result[array1.Length+array2.Length+array3.Length..totalLength-1]

        return result;
    }
}