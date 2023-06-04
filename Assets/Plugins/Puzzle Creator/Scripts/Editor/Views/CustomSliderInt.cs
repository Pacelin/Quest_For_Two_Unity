using UnityEngine;
using UnityEngine.UIElements;

public class CustomSliderInt : VisualElement
{
	public event System.Action<int> OnValueChanged;

	public int Value => _slider.value;

	private SliderInt _slider;
	private Label _label;
	private Label _labelValue;

	public static Color BACKGROUND_COLOR = new Color32(40, 44, 48, 100);
	public static Color FOREGROUND_COLOR = new Color32(27, 198, 180, 255);

	public static Color SLIDER_COLOR = new Color32(40, 44, 48, 255);
	public static Color KNOB_BORDER_COLOR = new Color32(40, 44, 48, 255);
	public static Color KNOB_COLOR = new Color32(27, 198, 180, 255);

	public static StyleLength LABEL_FONT_SIZE = 14;
	public static StyleLength LABEL_VALUE_FONT_SIZE = 16;

	public CustomSliderInt(int min, int max, string label)
	{
		_label = new Label(label);
		_slider = new SliderInt(min, max);
		_labelValue = new Label(min.ToString());

		this.style
			.SetBackgroundColor(BACKGROUND_COLOR)
			.SetFlex(false, Align.Center)
			.SetPadding(10)
			.SetBorderRadius(10);
		_label.style
			.SetForegroundColor(FOREGROUND_COLOR)
			.SetFontSize(LABEL_FONT_SIZE);
		_labelValue.style
			.SetForegroundColor(FOREGROUND_COLOR)
			.SetFontSize(LABEL_VALUE_FONT_SIZE);
		_slider.style
			.SetWidth(Length.Percent(100));

		
		var sliderChilds = _slider.ElementAt(0);
		sliderChilds.ElementAt(0).style
			.SetBackgroundColor(SLIDER_COLOR);
		//sliderChilds.ElementAt(1).style
		//	.SetBackgroundColor(KNOB_BORDER_COLOR);
		//sliderChilds.ElementAt(2).style
		//	.SetBackgroundColor(KNOB_COLOR);

		this.Add(_label);
		this.Add(_slider);
		this.Add(_labelValue);

		_slider.RegisterValueChangedCallback(ev =>
		{
			_labelValue.text = ev.newValue.ToString();
			OnValueChanged?.Invoke(ev.newValue);
		});
	}

	public void SetMax(int max)
	{
		_slider.highValue = max;
	}

	public void SetMin(int min)
	{
		_slider.lowValue = min;
	}

	public void SetValue(int value)
	{
		_slider.value = Mathf.Clamp(value, _slider.lowValue, _slider.highValue);
	}
}