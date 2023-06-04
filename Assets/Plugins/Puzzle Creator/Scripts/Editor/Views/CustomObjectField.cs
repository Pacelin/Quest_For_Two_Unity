using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomObjectField<T> : VisualElement where T : Object
{
	public event System.Action<T> OnValueChanged;

	public T Value => (T) _field.value;

	private Label _label;
	private ObjectField _field;

	public static Color BACKGROUND_COLOR = new Color32(40, 44, 48, 100);
	public static Color FOREGROUND_COLOR = new Color32(27, 198, 180, 255);
	public static Color VALUE_FOREGROUND_COLOR = new Color32(180, 240, 225, 255);

	public static Color FIELD_COLOR = new Color32(40, 44, 48, 180);

	public static StyleLength LABEL_FONT_SIZE = 14;

	public CustomObjectField(string label)
	{
		this.style
			.SetFlex(true, Align.Center)
			.SetPadding(10)
			.SetBackgroundColor(BACKGROUND_COLOR)
			.SetBorderRadius(10);

		_label = new Label(label);
		_label.style
			.SetForegroundColor(FOREGROUND_COLOR)
			.SetFontSize(LABEL_FONT_SIZE)
			.SetWidth(Length.Percent(40));

		_field = new ObjectField();
		_field.objectType = typeof(T);

		_field.style
			.SetWidth(Length.Percent(60));
		_field.ElementAt(0).style
			.SetBackgroundColor(FIELD_COLOR);
		_field.ElementAt(0).ElementAt(0).ElementAt(1).style
			.SetForegroundColor(VALUE_FOREGROUND_COLOR);

		this.Add(_label);
		this.Add(_field);

		_field.RegisterValueChangedCallback(ev =>
		{
			OnValueChanged?.Invoke((T) ev.newValue);
		});
	}

	public void SetValue(T value)
	{
		_field.value = value;
	}
}