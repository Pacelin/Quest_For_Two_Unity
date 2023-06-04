using UnityEngine;
using UnityEngine.UIElements;

public class PipesGridView : CustomPopupWindow
{
	public event System.Action<PipeView> OnCellClick;
	public event System.Action<PipeView> OnCellContextClick;
	public event System.Action OnGenerate;

	public CustomSliderInt _gridWidthField;
	public CustomSliderInt _gridHeightField;
	public CustomButton _generateGridButton;

	private VisualElement[] _rows;
	private PipeView[,] _grid;

	private static readonly Color GRID_BORDER_COLOR = new Color32(0, 0, 0, 30);
	private static readonly Color HOVER_COLOR = new Color32(0, 0, 0, 30);

	public PipesGridView() : base("Grid")
	{
		_gridWidthField = new CustomSliderInt(3, 20, "Grid width");
		_gridWidthField.style
			.SetWidthFill()
			.SetMarginBottom(10);

		_gridHeightField = new CustomSliderInt(3, 20, "Grid height");
		_gridHeightField.style
			.SetWidthFill()
			.SetMarginBottom(10);
		_generateGridButton = new CustomButton("Generate grid (only once)");
		_generateGridButton.style.SetWidthFill();

		this.Add(_gridWidthField);
		this.Add(_gridHeightField);
		this.Add(_generateGridButton);

		_generateGridButton.OnClick += () => 
		{
			GenerateGrid(_gridWidthField.Value, _gridHeightField.Value);
			this.Remove(_gridWidthField);
			this.Remove(_gridHeightField);
			this.Remove(_generateGridButton);
			OnGenerate?.Invoke();
		};
	}

	public void GenerateGrid(int gridWidth, int gridHeight)
	{
		_rows = new VisualElement[gridHeight];
		_grid = new PipeView[gridHeight, gridWidth];

		for (int y = 0; y < gridHeight; y++)
		{
			_rows[y] = InitRow(gridHeight);

			this.Add(_rows[y]);
			for (int x = 0; x < gridWidth; x++)
			{
				_grid[y, x] = InitCell(gridWidth);
				_rows[y].Add(_grid[y, x]);
			}
		}
	}

	public Pipe[,] GetGrid()
	{
		var pipes = new Pipe[_grid.GetLength(0), _grid.GetLength(1)];
		for (int x = 0; x < _grid.GetLength(1); x++)
			for (int y = 0; y < _grid.GetLength(0); y++)
				pipes[y, x] = _grid[y, x].Pipe;
		return pipes;
	}

	private VisualElement InitRow(int rowsCount)
	{
		var row = new VisualElement();
		row.style
			.SetSize(Length.Percent(100), Length.Percent(100f / rowsCount))
			.SetFlex(true, Align.Center);

		return row;
	}

	private PipeView InitCell(int columnsCount)
	{
		var cell = new PipeView();
		cell.style
			.SetSize(Length.Percent(100f / columnsCount), Length.Percent(100))
			.SetBorderWidth(1)
			.SetBorderColor(GRID_BORDER_COLOR);

		cell.RegisterCallback<MouseEnterEvent, PipeView>(OnMouseEnterCell, cell);
		cell.RegisterCallback<MouseLeaveEvent, PipeView>(OnMouseLeaveCell, cell);
		cell.RegisterCallback<MouseUpEvent, PipeView>(OnClickCell, cell);

		return cell;
	}

	private void OnMouseEnterCell(MouseEnterEvent ev, PipeView cell)
	{
		var color = HOVER_COLOR;
		if (cell.Pipe.Fixed) color = Color.Lerp(Color.red, color, 0.7f);
		cell.style.backgroundColor = color;
	}

	private void OnMouseLeaveCell(MouseLeaveEvent ev, PipeView cell)
	{
		var color = Color.clear;
		if (cell.Pipe.Fixed) color = Color.Lerp(Color.red, color, 0.7f);
		cell.style.backgroundColor = color;
	}

	private void OnClickCell(MouseUpEvent ev, PipeView cell)
	{
		if (ev.button == 0)
			OnCellClick?.Invoke(cell);
		else if (ev.button == 1)
			OnCellContextClick?.Invoke(cell);
	}
}