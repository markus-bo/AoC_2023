namespace AoC_Toolbox.Pathfinding;

public record NodeAStar<T> where T : Node
{
    public long GCost { get; set; }

    public long HCost { get; set; }

    public NodeAStar<T>? ParentNode { get; set; }

    public long FCost => GCost + HCost;

    public T NodePosition { get; init; }

    public NodeAStar(T position, NodeAStar<T>? parentNode = null, long gcost = 0, long hcost = 0)
    {
        this.NodePosition = position;

        this.ParentNode = parentNode;

        this.GCost = gcost;
        this.HCost = hcost;
    }

    public override string ToString()
    {
        return this.NodePosition.ToString();
    }
}
