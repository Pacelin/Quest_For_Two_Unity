using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PipesPuzzleEditorView : VisualElement
{
	public PipeSettingsView PipeSettingsView;
	public PipesListView PipesListView;
	public PipesGridView PipesGridView;

	public PipesPuzzleEditorView()
	{
		this.style
			.SetBackgroundColor(new Color32(32, 37, 41, 255))
			.SetPadding(10)
			.SetFlex(true, Align.Stretch);
		this.style.flexWrap = Wrap.Wrap;

		PipeSettingsView = new PipeSettingsView();
		PipeSettingsView.style
			.SetWidth(400)
			.marginRight = 10;
		
		PipesListView = new PipesListView();
		PipesListView.style
			.SetWidth(150)
			.marginRight = 10;

		PipesGridView = new PipesGridView();
		PipesGridView.style.SetGrow();

		this.Add(PipeSettingsView);
		this.Add(PipesListView);
		this.Add(PipesGridView);

		PipeSettingsView.AddPipeButton.OnClick += () =>
		{
			var data = PipeSettingsView.PipeView.Pipe.Clone();
			data.SetSprite(PipeSettingsView.SpriteField.Value);
			if (PipesListView.AddPipe(data))
				PipeSettingsView.ClearSettings();
		};

		PipesGridView.OnGenerate += () =>
		{
			PipeSettingsView.GeneratePuzzleButton.Enable();
		};

		PipesGridView.OnCellClick += (cell) =>
		{
			if (PipesListView.SelectedPipeView == null) return;
			var chosen = PipesListView.SelectedPipeData;
			var current = cell.Pipe;
			
			if (chosen.EdgesEquals(current))
				current.RotateClockwise();
			else
				cell.SetPipe(chosen.Clone());
		};

		PipesGridView.OnCellContextClick += (cell) =>
		{
			var menu = new GenericMenu();
			menu.AddItem(new GUIContent("Clear place"), false, () => cell.Pipe.Clear());

			if (cell.Pipe.Fixed)
				menu.AddItem(new GUIContent("Unfix"), false, () =>
				{
					cell.Pipe.Unfix();
				});
			else
				menu.AddItem(new GUIContent("Fix"), false, () => 
				{
					cell.Pipe.Fix();
					cell.style.SetBackgroundColor(Color.Lerp(Color.red, cell.style.backgroundColor.value, 0.7f));
				});
			if (cell.Pipe.TopEdge.MarkedEnd)
				menu.AddItem(new GUIContent("Unmark as end (Top)"), false, () => cell.Pipe.TopEdge.UnmarkEnd());
			else
				menu.AddItem(new GUIContent("Mark as end (Top)"), false, () => cell.Pipe.TopEdge.MarkEnd());

			if (cell.Pipe.RightEdge.MarkedEnd)
				menu.AddItem(new GUIContent("Unmark as end (Right)"), false, () => cell.Pipe.RightEdge.UnmarkEnd());
			else
				menu.AddItem(new GUIContent("Mark as end (Right)"), false, () => cell.Pipe.RightEdge.MarkEnd());

			if (cell.Pipe.BottomEdge.MarkedEnd)
				menu.AddItem(new GUIContent("Unmark as end (Bottom)"), false, () => cell.Pipe.BottomEdge.UnmarkEnd());
			else
				menu.AddItem(new GUIContent("Mark as end (Bottom)"), false, () => cell.Pipe.BottomEdge.MarkEnd());

			if (cell.Pipe.LeftEdge.MarkedEnd)
				menu.AddItem(new GUIContent("Unmark as end (Left)"), false, () => cell.Pipe.LeftEdge.UnmarkEnd());
			else
				menu.AddItem(new GUIContent("Mark as end (Left)"), false, () => cell.Pipe.LeftEdge.MarkEnd());

			menu.ShowAsContext();
		};
	}

}
