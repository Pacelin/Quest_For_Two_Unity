using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class FindPasswordPuzzleEditorView : VisualElement
{
	public SlotAddingView SlotAddingView;
	public PasswordView PasswordView;

	public FindPasswordPuzzleEditorView()
	{
		this.style
			.SetBackgroundColor(new Color32(32, 37, 41, 255))
			.SetPadding(10)
			.SetFlex(true, Align.Stretch);

		SlotAddingView = new SlotAddingView();
		SlotAddingView.style.SetWidth(400);
		SlotAddingView.style.marginRight = 10;

		PasswordView = new PasswordView();
		PasswordView.style.SetGrow();

		this.Add(SlotAddingView);
		this.Add(PasswordView);

		SlotAddingView.OnAddSlot += PasswordView.AddPasswordSlot;
	}
}
