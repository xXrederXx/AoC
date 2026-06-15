namespace AoC.Y2025.Day10;

record Node(Node? Parent, int OperationId, bool[] State)
{
    public int Depth => Parent is null ? 0 : Parent.Depth + 1;
}