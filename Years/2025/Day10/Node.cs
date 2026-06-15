namespace AoC.Y2025.Day10;

record Node<T>(Node<T>? Parent, int OperationId, T[] State)
{
    public int Depth => Parent is null ? 0 : Parent.Depth + 1;
}