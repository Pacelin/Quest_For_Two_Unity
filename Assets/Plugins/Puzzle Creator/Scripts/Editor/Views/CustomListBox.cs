using UnityEngine.UIElements;

public class CustomListBox : ScrollView 
{
	private int _spacing = 10;

	public CustomListBox()
	{
		this.style
			.SetFlex(false, Align.Center);

		this.style.paddingRight = 10;

		this.horizontalScroller.SetEnabled(false);
	}

	public void AddItem(VisualElement item)
	{
		if (this.childCount > 0)
			this.ElementAt(this.childCount - 1).style.SetMarginBottom(_spacing);

		this.Add(item);
	}

	public void RemoveItem(VisualElement item) =>
		this.RemoveItem(this.IndexOf(item));

	public void RemoveItem(int index)
	{
		if (index == this.childCount - 1 && this.childCount > 1)
			this.ElementAt(this.childCount - 2).style.SetMarginBottom(0);

		this.RemoveAt(index);
	}

	public void SetSpacing(int spacing)
	{
		_spacing = spacing;
		for (int i = 0; i < this.childCount - 1; i++)
			this.ElementAt(i).style.SetMarginBottom(spacing);
	}

}