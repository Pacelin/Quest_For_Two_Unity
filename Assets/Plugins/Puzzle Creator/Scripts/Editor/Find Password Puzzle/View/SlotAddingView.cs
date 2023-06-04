using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SlotAddingView : CustomPopupWindow
{
	public event System.Action<Sprite[], int> OnAddSlot;

	public CustomSliderInt SymbolsCountSlider;
	public CustomSliderInt CorrectSymbolSlider;
	public CustomListBox SymbolsListBox;
	public CustomButton AddSlotButton;

	private CustomPopupWindow _slotSettingsWindow;
	private CustomPopupWindow _symbolsListWindow;

	private List<CustomObjectField<Sprite>> _symbolsSpriteFields;

	public SlotAddingView() : base("Password Slot Adding")
	{
		_symbolsSpriteFields = new List<CustomObjectField<Sprite>>();

		_slotSettingsWindow = new CustomPopupWindow("Slot Settings", true);
		_slotSettingsWindow.style
			.SetWidthFill()
			.SetMarginBottom(10);
		
		_symbolsListWindow = new CustomPopupWindow("Symbols List", true);
		_symbolsListWindow.style
			.SetWidthFill()
			.SetMarginBottom(10)
			.SetGrow()
			.SetHeight(StyleKeyword.Undefined);

		SymbolsCountSlider = new CustomSliderInt(1, 10, "Symbols Count");
		SymbolsCountSlider.style
			.SetWidthFill()
			.SetMarginBottom(10);
		
		CorrectSymbolSlider = new CustomSliderInt(1, 1, "Correct Symbol Index");
		CorrectSymbolSlider.style.SetWidth(Length.Percent(100));

		SymbolsListBox = new CustomListBox();
		SymbolsListBox.style
			.SetWidthFill();

		AddSlotButton = new CustomButton("Add Slot In Password");
		AddSlotButton.style.SetWidth(Length.Percent(100));
		AddSlotButton.Disable();

		AddSpriteField();

		this.Add(_slotSettingsWindow);
		this.Add(_symbolsListWindow);
		this.Add(AddSlotButton);

		_slotSettingsWindow.Add(SymbolsCountSlider);
		_slotSettingsWindow.Add(CorrectSymbolSlider);

		_symbolsListWindow.Add(SymbolsListBox);

		SymbolsCountSlider.OnValueChanged += SymbolsCountSlider_OnValueChanged;
		AddSlotButton.OnClick += AddSlotButton_OnClick;
	}

	private void AddSlotButton_OnClick()
	{
		OnAddSlot?.Invoke(
			_symbolsSpriteFields.Select(field => field.Value).ToArray(), 
			CorrectSymbolSlider.Value);

		SymbolsCountSlider.SetValue(1);
		_symbolsSpriteFields[0].SetValue(null);
	}

	private void AddSpriteField()
	{
		var field = new CustomObjectField<Sprite>("Symbol " + (_symbolsSpriteFields.Count + 1));
		field.style.SetWidth(Length.Percent(100));
		field.OnValueChanged += SpriteField_OnValueChanged;
		SymbolsListBox.AddItem(field);
		_symbolsSpriteFields.Add(field);

		AddSlotButton.Disable();
	}

	private void RemoveSpriteField()
	{
		var field = _symbolsSpriteFields[_symbolsSpriteFields.Count - 1];
		field.OnValueChanged -= SpriteField_OnValueChanged;
		SymbolsListBox.RemoveItem(field);
		_symbolsSpriteFields.Remove(field);

		CheckFilling();
	}

	private void SymbolsCountSlider_OnValueChanged(int value)
	{
		CorrectSymbolSlider.SetMax(value);

		while (_symbolsSpriteFields.Count < value)
			AddSpriteField();
		while (_symbolsSpriteFields.Count > value)
			RemoveSpriteField();
	}

	private void SpriteField_OnValueChanged(Sprite value)
	{
		CheckFilling();
	}

	private void CheckFilling()
	{
		foreach (var field in _symbolsSpriteFields)
			if (field.Value == null) { AddSlotButton.Disable(); return; }

		AddSlotButton.Enable();
	}
}