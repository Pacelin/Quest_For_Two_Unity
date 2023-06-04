using System;

public class DefaultPipePuzzleValidator : PipePuzzleValidator
{
    private readonly Func<Pipe, PipeEdge>
        _top = (p) => p.TopEdge,
        _right = (p) => p.RightEdge,
        _bottom = (p) => p.BottomEdge,
        _left = (p) => p.LeftEdge;

    public override bool CheckSolvency(Pipe[,] pipes)
    {
        var height = pipes.GetLength(0);
        var width = pipes.GetLength(1);

        for (int y = 0; y < height; y++)
            for (int x = y % 2; x < width; x += 2)
                if (!CheckPipe(pipes, x, y))
                    return false;
        
        return true;
    }
    private bool CheckPipe(Pipe[,] pipes, int x, int y) =>
        CheckEdge(pipes, x, y, x - 1, y, _left, _right) &&
        CheckEdge(pipes, x, y, x + 1, y, _right, _left) &&
        CheckEdge(pipes, x, y, x, y - 1, _top, _bottom) &&
        CheckEdge(pipes, x, y, x, y + 1, _bottom, _top);

    private bool CheckEdge(Pipe[,] pipes, int x1, int y1, int x2, int y2,
        Func<Pipe, PipeEdge> edge1, Func<Pipe, PipeEdge> edge2)
    {
        if (y2 < 0 || y2 >= pipes.GetLength(0) ||
            x2 < 0 || x2 >= pipes.GetLength(1))
            return edge1(pipes[y1, x1]).MarkedEnd || edge1(pipes[y1, x1]).EdgeType == PipeEdgeType.NO_PIPE;
        
        return edge2(pipes[y2, x2]).MarkedEnd || edge1(pipes[y1, x1]).EdgeType == edge2(pipes[y2, x2]).EdgeType;
    }
}