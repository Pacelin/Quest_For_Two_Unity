using System;
using UnityEngine;
using UnityEngine.UI;

public class PipePuzzle : MonoBehaviour
{
    public event Action OnSolve;

    [SerializeField] private int _rowsCount;
    [SerializeField] private int _columnsCount;
    [SerializeField] private PipeButton[] _buttonsArray;
    [Space]
    [SerializeField] private GridLayoutGroup _layout;
    [SerializeField] private PipePuzzleValidator _validator;

    private PipeButton[,] _buttons;
    private bool _solved = false;

    private void Awake()
    {
        _buttons = new PipeButton[_rowsCount, _columnsCount];
        for (int x = 0; x < _columnsCount; x++)
            for (int y = 0; y < _rowsCount; y++)
                _buttons[y, x] = _buttonsArray[y * _columnsCount + x];
    }

    private void OnEnable()
    {
        ForeachButton((x, y, b) => b.AssociatedPipe.OnPipeChanged += OnPuzzleStateChanged);
    }
    private void OnDisable()
    {
        ForeachButton((x, y, b) => b.AssociatedPipe.OnPipeChanged -= OnPuzzleStateChanged);
    }

    [ExecuteInEditMode]
    public void SetLayout()
    {
        _layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _layout.constraintCount = _columnsCount;
    }

    private void OnPuzzleStateChanged(Pipe pipe)
    {
        if (_solved) return;
        if (_validator.CheckSolvency(GetPipes())) 
        {
            Debug.Log("Puzzle Solved");
            OnSolve?.Invoke();
        }
    }

    private void ForeachButton(Action<int, int, PipeButton> action)
    {
        for (int y = 0; y < _buttons.GetLength(0); y++)
            for (int x = 0; x < _buttons.GetLength(1); x++)
                action(x, y, _buttons[y, x]);
    }

    private Pipe[,] GetPipes()
    {
        var pipes = new Pipe[_buttons.GetLength(0), _buttons.GetLength(1)];
        for (int y = 0; y < _buttons.GetLength(0); y++)
            for (int x = 0; x < _buttons.GetLength(1); x++)
                pipes[y, x] = _buttons[y, x].AssociatedPipe;
        return pipes;
    }
}
