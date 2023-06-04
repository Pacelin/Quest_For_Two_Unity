using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PipesListView : CustomPopupWindow
{
	public PipeView[] PipesViews => _pipesViews.ToArray();
	public PipeView SelectedPipeView { get; private set; }
	public Pipe SelectedPipeData => SelectedPipeView == null ? default : _pipes[_pipesViews.IndexOf(SelectedPipeView)];

	private List<PipeView> _pipesViews;
	private List<Pipe> _pipes;
	private ScrollView _scroll;

	private static readonly Color SELECTED_COLOR = new Color32(161, 180, 196, 50);
	private static readonly Color HOVER_COLOR = new Color32(0, 0, 0, 30);
	private static readonly Color HOVER_SELECTED_COLOR = new Color32(161, 180, 196, 30);

	public PipesListView() : base("Pipes")
	{
		this.style.SetFlex(false, Align.Center);
		_scroll = new ScrollView(ScrollViewMode.Vertical);
		_scroll.style
			.SetFlex(false, Align.Center)
			.SetGrow()
			.SetWidthFill();
		this.Add(_scroll);

		_pipesViews = new List<PipeView>();
		_pipes = new List<Pipe>();
	}

	public bool AddPipe(Pipe pipe)
	{
		if (_pipes.Any(p => p.EdgesEquals(pipe))) 
		{
			Debug.LogWarning("Pipe already exists");
			return false;
		}

		_pipes.Add(pipe);

		var pipeView = new PipeView(pipe);
		_pipesViews.Add(pipeView);
		_scroll.Add(pipeView);
		pipeView.style
			.SetMarginBottom(5)
			.SetSize(110, 110);

		pipeView.TopPipe.style
			.SetTop(10)
			.paddingBottom = -10;
		pipeView.RightPipe.style
			.marginLeft = -10;
		pipeView.BottomPipe.style
			.marginTop = -10;
		pipeView.LeftPipe.style
			.SetLeft(10)
			.paddingRight = -10;

		pipeView.SetPipe(pipe);

		pipeView.RegisterCallback<MouseUpEvent, PipeView>(OnPipeMouseClick, pipeView);
		pipeView.RegisterCallback<MouseEnterEvent, PipeView>(OnPipeMouseEnter, pipeView);
		pipeView.RegisterCallback<MouseLeaveEvent, PipeView>(OnPipeMouseLeave, pipeView);
		return true;
	}

	public void RemovePipe(PipeView pipe)
	{
		_scroll.Remove(pipe);
		_pipes.RemoveAt(_pipesViews.IndexOf(pipe));
		_pipesViews.Remove(pipe);

		if (pipe == SelectedPipeView) SelectedPipeView = null;

		pipe.UnregisterCallback<MouseUpEvent, PipeView>(OnPipeMouseClick);
		pipe.UnregisterCallback<MouseEnterEvent, PipeView>(OnPipeMouseEnter);
		pipe.UnregisterCallback<MouseLeaveEvent, PipeView>(OnPipeMouseLeave);
	}

	private void OnPipeMouseClick(MouseUpEvent ev, PipeView view)
	{
		if (ev.button == (int) MouseButton.LeftMouse)
		{
			if (SelectedPipeView != null)
				SelectedPipeView.style.SetBackgroundColor(Color.clear);

			view.style.SetBackgroundColor(HOVER_SELECTED_COLOR);
			SelectedPipeView = view;
		}
		else if (ev.button == (int) MouseButton.RightMouse)
		{
			var menu = new UnityEditor.GenericMenu();
			menu.AddItem(new GUIContent("Remove Pipe"), false, () =>
			{
				RemovePipe(view);
			});
			menu.ShowAsContext();
		}
	}

	private void OnPipeMouseEnter(MouseEnterEvent ev, PipeView view)
	{
		if (view == SelectedPipeView)
			view.style.SetBackgroundColor(HOVER_SELECTED_COLOR);
		else
			view.style.SetBackgroundColor(HOVER_COLOR);
	}

	private void OnPipeMouseLeave(MouseLeaveEvent ev, PipeView view)
	{
		if (view == SelectedPipeView)
			view.style.SetBackgroundColor(SELECTED_COLOR);
		else
			view.style.SetBackgroundColor(Color.clear);
	}
}