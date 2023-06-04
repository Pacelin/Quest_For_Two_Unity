using UnityEngine;
using UnityEngine.UIElements;

public class PipeView : VisualElement
{
    private static readonly Color DEFAULT_PIPE_COLOR = new Color32(161, 180, 196, 255);
    private static readonly Color CUSTOM_PIPE_COLOR_0 = new Color32(196, 106, 134, 255);
    private static readonly Color CUSTOM_PIPE_COLOR_1 = new Color32(101, 183, 102, 255);
    private static readonly Color CUSTOM_PIPE_COLOR_2 = new Color32(116, 112, 179, 255);

    public Pipe Pipe { get; private set; }

    public VisualElement CenterPipe;

    public VisualElement TopPipe;
    public VisualElement RightPipe;
    public VisualElement BottomPipe;
    public VisualElement LeftPipe;

    public PipeView(Pipe pipe)
    {
        InitView();
        SetPipe(pipe);
    }

    public PipeView()
    {
        InitView();
        SetPipe(new Pipe());
    }

    public void SetPipe(Pipe pipe)
    {
        if (Pipe != null) Pipe.OnPipeChanged -= OnPipeChanged;
        Pipe = pipe;
        Pipe.OnPipeChanged += OnPipeChanged;
        Refresh();
    }

    public void OnPipeChanged(Pipe pipe) => Refresh();

    private void Refresh()
    {
        if (Pipe.IsEmpty)
            CenterPipe.style.SetInvisible();
        else
            CenterPipe.style.SetVisible();
            
        TopPipe.style.SetBackgroundColor(GetColor(Pipe.TopEdge.EdgeType));
        RightPipe.style.SetBackgroundColor(GetColor(Pipe.RightEdge.EdgeType));
        BottomPipe.style.SetBackgroundColor(GetColor(Pipe.BottomEdge.EdgeType));
        LeftPipe.style.SetBackgroundColor(GetColor(Pipe.LeftEdge.EdgeType));
    }

    private Color GetColor(PipeEdgeType type)
    {
        switch (type)
        {
            case PipeEdgeType.DEFAULT_PIPE: return DEFAULT_PIPE_COLOR;
            case PipeEdgeType.CUSTOM_PIPE_0: return CUSTOM_PIPE_COLOR_0;
            case PipeEdgeType.CUSTOM_PIPE_1: return CUSTOM_PIPE_COLOR_1;
            case PipeEdgeType.CUSTOM_PIPE_2: return CUSTOM_PIPE_COLOR_2;
            default: return Color.clear;
        };
    }

    private void InitView()
    {
		var pipe_width = Length.Percent(20);
		var small_offset = Length.Percent(40);
		var half = Length.Percent(50);

        CenterPipe = new VisualElement();
        CenterPipe.style
            .SetPositionAbsolute()
            .SetBackgroundColor(DEFAULT_PIPE_COLOR)
            .SetSize(pipe_width, pipe_width)
            .SetLeft(small_offset)
            .SetTop(small_offset)
            .SetBorderRadius(5)
            .SetVisible();

        TopPipe = new VisualElement();
        TopPipe.style
            .SetPositionAbsolute()
            .SetSize(pipe_width, half)
            .SetLeft(small_offset)
            .SetTop(0)
            .SetBorderRadius(5);

        RightPipe = new VisualElement();
        RightPipe.style
            .SetPositionAbsolute()
            .SetSize(half, pipe_width)
            .SetLeft(half)
            .SetTop(small_offset)
            .SetBorderRadius(5);

        BottomPipe = new VisualElement();
        BottomPipe.style
            .SetPositionAbsolute()
            .SetSize(pipe_width, half)
            .SetLeft(small_offset)
            .SetTop(half)
            .SetBorderRadius(5);

        LeftPipe = new VisualElement();
        LeftPipe.style
            .SetPositionAbsolute()
            .SetSize(half, pipe_width)
            .SetLeft(0)
            .SetTop(small_offset)
            .SetBorderRadius(5);

        this.Add(TopPipe);
        this.Add(RightPipe);
        this.Add(BottomPipe);
        this.Add(LeftPipe);
        this.Add(CenterPipe);
    }
}