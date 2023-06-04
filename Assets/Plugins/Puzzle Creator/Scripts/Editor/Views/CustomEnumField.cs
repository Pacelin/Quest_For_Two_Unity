using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomEnumField<T> : VisualElement where T : Enum
{
	public event System.Action<T> OnValueChanged;

	public T Value => (T) _field.value;

	private Label _label;
	private EnumField _field;

	public static Color BACKGROUND_COLOR = new Color32(40, 44, 48, 100);
	public static Color FOREGROUND_COLOR = new Color32(27, 198, 180, 255);
	public static Color VALUE_FOREGROUND_COLOR = new Color32(180, 240, 225, 255);

	public static Color FIELD_COLOR = new Color32(40, 44, 48, 180);

	public static StyleLength LABEL_FONT_SIZE = 14;

	public CustomEnumField(string label, Enum defaultEnum)
	{
		this.style
			.SetFlex(true, Align.Center)
			.SetPadding(10)
			.SetBackgroundColor(BACKGROUND_COLOR)
			.SetBorderRadius(10);

        if (InstallLabel(label))
            InstallField(defaultEnum, Length.Percent(60));
        else
            InstallField(defaultEnum, Length.Percent(100));
	}

    private bool InstallLabel(string label)
    {
        if (string.IsNullOrWhiteSpace(label)) return false;

        _label = new Label(label);
		_label.style
			.SetForegroundColor(FOREGROUND_COLOR)
			.SetFontSize(LABEL_FONT_SIZE)
			.SetWidth(Length.Percent(40));
		this.Add(_label);

        return true;
    }   

    private void InstallField(Enum defaultEnum, StyleLength width)
    {
		_field = new EnumField(defaultEnum);

		_field.style
			.SetWidth(width);
		_field.ElementAt(0).style
			.SetBackgroundColor(FIELD_COLOR);
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