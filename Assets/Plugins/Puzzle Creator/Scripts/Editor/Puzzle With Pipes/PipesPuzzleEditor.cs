using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PipesPuzzleEditor : EditorWindow
{
	private PipesPuzzleEditorView _view;

	[MenuItem("GameObject/Puzzles/Puzzle With Pipes", false, 10)]
	public static void Open()
	{
		var window = GetWindow<PipesPuzzleEditor>();

		window.InitWindow();
		window.Show();
	}
	
	private void InitWindow()
	{
		titleContent = new GUIContent("Puzzle With Pipes");

		_view = new PipesPuzzleEditorView();
		_view.StretchToParentSize();
		rootVisualElement.Add(_view);

		this.minSize = new Vector2(1000, 595);
		_view.PipeSettingsView.GeneratePuzzleButton.OnClick += GeneratePuzzle;
	}

	private void GeneratePuzzle()
	{
		_view.PipeSettingsView.GeneratePuzzleButton.OnClick -= GeneratePuzzle;
		var grid = _view.PipesGridView.GetGrid();

		var puzzleObject = GeneratePuzzleObject(out var serializedPuzzleObject,
			out var columnsCount, out var rowsCount, out var buttonsArray);

		rowsCount.intValue = grid.GetLength(0);
		columnsCount.intValue = grid.GetLength(1);

		var buttons = new PipeButton[grid.GetLength(0) * grid.GetLength(1)];
		for (int y = 0; y < grid.GetLength(0); y++)
			for (int x = 0; x < grid.GetLength(1); x++)
				buttons[y * grid.GetLength(1) + x] = GeneratePipe(puzzleObject.transform, grid[y, x]);

		buttonsArray.SetArray(buttons);
		serializedPuzzleObject.ApplyModifiedPropertiesWithoutUndo();

		puzzleObject.SetLayout();
		this.Close();
	}

	private PipePuzzle GeneratePuzzleObject(out SerializedObject serialized,
		out SerializedProperty columnsCount,
		out SerializedProperty rowsCount,
		out SerializedProperty buttonsArray)
	{
		var puzzleObject = 
			CustomObjectsUtility.CreatePrefab("Prefabs/Pipes Puzzle/PipesPuzzle")
			.GetComponent<PipePuzzle>();
		serialized = new SerializedObject(puzzleObject);
		columnsCount = serialized.FindProperty("_columnsCount");
		rowsCount = serialized.FindProperty("_rowsCount");
		buttonsArray = serialized.FindProperty("_buttonsArray");

		return puzzleObject;
	}

	private PipeButton GeneratePipe(Transform parent, Pipe pipe)
	{
		var pipeObject = 
			CustomObjectsUtility.CreatePrefab("Prefabs/Pipes Puzzle/Pipe", parent)
			.GetComponent<PipeButton>();

		pipeObject.Init(pipe);

		return pipeObject;
	}
}
