using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PasswordView : CustomPopupWindow
{
	public (Sprite[], int)[] Slots => _slots.ToArray();
	public CustomButton CreatePuzzleButton;

	private CustomPopupWindow _passwordSettingsWindow;
	private CustomPopupWindow _passwordDrawingWindow;
	private CustomPopupWindow _passwordDrawingPanel;

	private ScrollView _drawScroll;

	private List<CustomPopupWindow> _slotsWindows;
	private List<(Sprite[], int)> _slots;

	public PasswordView() : base("Password Settings")
	{
		_slotsWindows = new List<CustomPopupWindow>();
		_slots = new List<(Sprite[], int)>();

		_passwordSettingsWindow = new CustomPopupWindow("Password", true);
		_passwordSettingsWindow.style
			.SetWidthFill()
			.SetMarginBottom(10)
			.SetGrow();

		CreatePuzzleButton = new CustomButton("Create Puzzle");
		CreatePuzzleButton.style.SetWidthFill();

		this.Add(_passwordSettingsWindow);
		this.Add(CreatePuzzleButton);

		_passwordDrawingWindow = new CustomPopupWindow("", true);
		_passwordDrawingWindow.style
			.SetWidthFill()
			.SetGrow()
			.SetFlex(false, Align.Center);
		_passwordDrawingWindow.style.justifyContent = Justify.Center;

		_passwordSettingsWindow.Add(_passwordDrawingWindow);

		_drawScroll = new ScrollView(ScrollViewMode.VerticalAndHorizontal);
		_drawScroll.style.SetSize(Length.Percent(100), Length.Percent(100));
		_drawScroll.style.justifyContent = Justify.Center;
		_drawScroll.style.alignSelf = Align.Center;
		_drawScroll.style.alignItems = Align.Center;

		_passwordDrawingPanel = new CustomPopupWindow("", true);
		_passwordDrawingPanel.style
			.SetFlex(true, Align.FlexStart)
			.SetInvisible();

		_passwordDrawingWindow.Add(_drawScroll);
		_drawScroll.contentContainer.Add(_passwordDrawingPanel);
	}

	public void AddPasswordSlot(Sprite[] symbols, int correctIndex)
	{
		var window = new CustomPopupWindow("", true);
		window.style.SetPadding(3);

		for (int i = 0; i < symbols.Length; i++)
		{
			var image = new Image();
			image.style
				.SetSize(40, 40)
				.SetBorderWidth(2)
				.SetPadding(2)
				.SetBorderRadius(2)
				.SetMarginBottom(5);

			image.sprite = symbols[i];

			if (i == symbols.Length - 1) 
				image.style.SetMarginBottom(0);

			if (i == correctIndex - 1)
				image.style.SetBorderColor(new Color32(27, 198, 180, 255));

			window.Add(image);
		}

		_passwordDrawingPanel.Add(window);

		_slotsWindows.Add(window);
		_slots.Add((symbols, correctIndex));

		if (_slotsWindows.Count > 1) 
			_slotsWindows[_slotsWindows.Count - 2].style.marginRight = 5;

		UpdateDrawingPanel();
	}

	private void UpdateDrawingPanel()
	{
		if (_slots.Count == 0) _passwordDrawingPanel.style.SetInvisible();
		else _passwordDrawingPanel.style.SetVisible();

		const int TILE_SIZE = 45;

		var top = _slots.Select(slot => slot.Item2 - 1).ToArray();
		var bottom = _slots.Select(slot => slot.Item1.Length - slot.Item2).ToArray();
		var maxTop = top.Max();
		var height = maxTop + bottom.Max() + 1;

		_passwordDrawingPanel.style.SetHeight(TILE_SIZE * height + 20);
		_passwordDrawingPanel.style.SetWidth(TILE_SIZE * _slots.Count + 20);

		for (int i = 0; i < _slots.Count; i++)
			_slotsWindows[i].style.SetTop((maxTop - top[i]) * TILE_SIZE);

	}
}