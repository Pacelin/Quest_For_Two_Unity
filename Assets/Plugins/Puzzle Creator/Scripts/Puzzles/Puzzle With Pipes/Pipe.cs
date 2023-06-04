using UnityEngine;

[System.Serializable]
public class Pipe
{
    public bool IsEmpty =>
        TopEdge.EdgeType == PipeEdgeType.NO_PIPE &&
        RightEdge.EdgeType == PipeEdgeType.NO_PIPE &&
        BottomEdge.EdgeType == PipeEdgeType.NO_PIPE &&
        LeftEdge.EdgeType == PipeEdgeType.NO_PIPE;

    public System.Action<Pipe> OnPipeChanged;
	[field: SerializeField] public Sprite PipeSprite { get; private set; }
    
    [field: SerializeField] public bool Fixed { get; private set; }
    [field: SerializeField] public int RotationsCount { get; private set; }

    [field: SerializeField] public PipeEdge TopEdge { get; private set; }
    [field: SerializeField] public PipeEdge RightEdge { get; private set; }
    [field: SerializeField] public PipeEdge BottomEdge { get; private set; }
    [field: SerializeField] public PipeEdge LeftEdge { get; private set; }
    
    public Pipe()
    {
        TopEdge = new PipeEdge(PipeEdgeType.NO_PIPE);
        RightEdge = new PipeEdge(PipeEdgeType.NO_PIPE);
        BottomEdge = new PipeEdge(PipeEdgeType.NO_PIPE);
        LeftEdge = new PipeEdge(PipeEdgeType.NO_PIPE);
        
        Fixed = false;

        TopEdge.OnEdgeChanged += OnEdgeChanged;
        RightEdge.OnEdgeChanged += OnEdgeChanged;
        BottomEdge.OnEdgeChanged += OnEdgeChanged;
        LeftEdge.OnEdgeChanged += OnEdgeChanged;
    }

    public Pipe(PipeEdge top, PipeEdge right, PipeEdge bottom, PipeEdge left)
    {
        TopEdge = top;
        RightEdge = right;
        BottomEdge = bottom;
        LeftEdge = left;
        
        Fixed = false;

        TopEdge.OnEdgeChanged += OnEdgeChanged;
        RightEdge.OnEdgeChanged += OnEdgeChanged;
        BottomEdge.OnEdgeChanged += OnEdgeChanged;
        LeftEdge.OnEdgeChanged += OnEdgeChanged;
    }

    public void SetSprite(Sprite sprite)
    {
        if (PipeSprite == sprite) return;
        PipeSprite = sprite;
        OnPipeChanged?.Invoke(this);
    }

    public void Fix()
    {
        if (Fixed) return;
        Fixed = true;
        OnPipeChanged?.Invoke(this);
    }

    public void Unfix()
    {
        if (!Fixed) return;
        Fixed = false;
        OnPipeChanged?.Invoke(this);
    }

    public void RotateClockwise()
    {
        var temp = TopEdge;
        TopEdge = LeftEdge;
        LeftEdge = BottomEdge;
        BottomEdge = RightEdge;
        RightEdge = temp;
        RotationsCount = (RotationsCount + 1) % 4;

        OnPipeChanged?.Invoke(this);
    }

    public void RotateCounterClockwise()
    {
        var temp = TopEdge;
        TopEdge = RightEdge;
        RightEdge = BottomEdge;
        BottomEdge = LeftEdge;
        LeftEdge = temp;
        RotationsCount = (RotationsCount + 3) % 4;

        OnPipeChanged?.Invoke(this);
    }

    public bool EdgesEquals(Pipe other)
    {
        return 
            (TopEdge.Equals(other.TopEdge) &&
            RightEdge.Equals(other.RightEdge) &&
            BottomEdge.Equals(other.BottomEdge) &&
            LeftEdge.Equals(other.LeftEdge)) ||
            (TopEdge.Equals(other.RightEdge) &&
            RightEdge.Equals(other.BottomEdge) &&
            BottomEdge.Equals(other.LeftEdge) &&
            LeftEdge.Equals(other.TopEdge)) ||
            (TopEdge.Equals(other.BottomEdge) &&
            RightEdge.Equals(other.LeftEdge) &&
            BottomEdge.Equals(other.TopEdge) &&
            LeftEdge.Equals(other.RightEdge)) ||
            (TopEdge.Equals(other.LeftEdge) &&
            RightEdge.Equals(other.TopEdge) &&
            BottomEdge.Equals(other.RightEdge) &&
            LeftEdge.Equals(other.BottomEdge));
    }

    public Pipe Clone()
    {
        var pipe = new Pipe(TopEdge.Clone(), RightEdge.Clone(), BottomEdge.Clone(), LeftEdge.Clone())
        {
            PipeSprite = this.PipeSprite,
            Fixed = this.Fixed
        };

        return pipe;
    }

    public void Clear()
    {
        TopEdge.SetType(PipeEdgeType.NO_PIPE);
        RightEdge.SetType(PipeEdgeType.NO_PIPE);
        BottomEdge.SetType(PipeEdgeType.NO_PIPE);
        LeftEdge.SetType(PipeEdgeType.NO_PIPE);
    }

    private void OnEdgeChanged(PipeEdge edge)
    {
        OnPipeChanged?.Invoke(this);
    }
}