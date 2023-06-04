using UnityEngine;
using UnityEngine.UIElements;

public class CustomButton: VisualElement
{
	public event System.Action OnClick;

	public bool Enabled { get; private set; }

	public static Color FOREGROUND_COLOR = new Color32(27, 198, 180, 255);
	public static Color DISABLED_FOREGROUND_COLOR = new Color32(27, 198, 180, 120);
	public static StyleLength FONT_SIZE = 14;

	public static Color BACKGROUND_COLOR = new Color32(40, 44, 48, 150);
	public static Color BACKGROUND_HOVER_COLOR = new Color32(40, 44, 48, 170);
	public static Color BACKGROUND_DOWN_COLOR = new Color32(40, 44, 48, 255);
	public static Color DISABLED_BACKGROUND_COLOR = new Color32(40, 44, 48, 50);

	public static Color BORDER_COLOR = new Color32(40, 44, 48, 200);
	public static Color BORDER_HOVER_COLOR = new Color32(40, 44, 48, 180);
	public static Color BORDER_DOWN_COLOR = new Color32(40, 44, 48, 255);
	public static Color DISABLED_BORDER_COLOR = new Color32(40, 44, 48, 50);

	private Label _label;

	private bool _down = false;

	public CustomButton(string text)
	{
		_label = new Label(text);
		_label.style
			.SetFontSize(FONT_SIZE)
			.SetSize(Length.Percent(100), Length.Percent(100));
		_label.style.unityTextAlign = TextAnchor.MiddleCenter;
		this.Add(_label);

		this.style
			.SetPadding(10)
			.SetBorderWidth(2)
			.SetBorderRadius(5);

		Enable();
	}

	public void Enable()
	{
		Enabled = true;
		this.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
		this.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
		this.RegisterCallback<MouseDownEvent>(OnMouseDown);
		this.RegisterCallback<MouseUpEvent>(OnMouseUp);

		this.style
			.SetBorderColor(BORDER_COLOR)
			.SetBackgroundColor(BACKGROUND_COLOR);

		_label.style.SetForegroundColor(FOREGROUND_COLOR);
	}

	public void Disable()
	{
		Enabled = false;
		this.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
		this.UnregisterCallback<MouseLeaveEvent>(OnMouseLeave);
		this.UnregisterCallback<MouseDownEvent>(OnMouseDown);
		this.UnregisterCallback<MouseUpEvent>(OnMouseUp);

		this.style
			.SetBorderColor(DISABLED_BORDER_COLOR)
			.SetBackgroundColor(DISABLED_BACKGROUND_COLOR);

		_label.style.SetForegroundColor(DISABLED_FOREGROUND_COLOR);
	}

	private void OnMouseEnter(MouseEnterEvent ev)
	{
		this.style
			.SetBackgroundColor(BACKGROUND_HOVER_COLOR)
			.SetBorderColor(BORDER_HOVER_COLOR);
	}

	private void OnMouseLeave(MouseLeaveEvent ev)
	{
		_down = false;

		this.style
			.SetBackgroundColor(BACKGROUND_COLOR)
			.SetBorderColor(BORDER_COLOR);
	}

	private void OnMouseDown(MouseDownEvent ev)
	{
		if (ev.button != (int)MouseButton.LeftMouse) return;

		_down = true;

		this.style
			.SetBackgroundColor(BACKGROUND_DOWN_COLOR)
			.SetBorderColor(BORDER_DOWN_COLOR);
	}
	private void OnMouseUp(MouseUpEvent ev)
	{
		this.style
			.SetBackgroundColor(BACKGROUND_HOVER_COLOR)
			.SetBorderColor(BORDER_HOVER_COLOR);

		if (_down)
			OnClick?.Invoke();

		_down = false;
	}
}
