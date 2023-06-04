using UnityEngine;

[System.Serializable]
public class PipeEdge : System.IEquatable<PipeEdge>
{
    public System.Action<PipeEdge> OnEdgeChanged;

    [field: SerializeField] public bool MarkedEnd { get; private set; }
    [field: SerializeField] public PipeEdgeType EdgeType { get; private set; }

    public PipeEdge(PipeEdgeType edgeType, bool end = false)
    {
        EdgeType = edgeType;
        MarkedEnd = end;
    }

    public void MarkEnd()
    {
        if (MarkedEnd) return;
        MarkedEnd = true;
        OnEdgeChanged?.Invoke(this);
    }

    public void UnmarkEnd()
    {
        if (!MarkedEnd) return;
        MarkedEnd = false;
        OnEdgeChanged?.Invoke(this);
    }

    public void SetType(PipeEdgeType type)
    {
        if (EdgeType == type) return;
        EdgeType = type;
        OnEdgeChanged?.Invoke(this);
    }

    public bool Equals(PipeEdge other) => 
        this.MarkedEnd == other.MarkedEnd && this.EdgeType == other.EdgeType;
    public PipeEdge Clone() => 
        new PipeEdge(EdgeType, MarkedEnd);
}
