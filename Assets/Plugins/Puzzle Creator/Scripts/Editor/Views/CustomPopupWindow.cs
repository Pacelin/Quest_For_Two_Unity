using UnityEngine;
using UnityEngine.UIElements;

public class CustomPopupWindow: VisualElement
{
	public Label Label;

	public static Color BACKGROUND_COLOR = new Color32(59, 66, 71, 255);
	public static Color SUBGROUND_COLOR = new Color32(0, 0, 0, 20);
	public static Color FOREGROUND_COLOR = new Color32(27, 198, 180, 255);
	public static Color BORDER_COLOR = new Color32(40, 44, 48, 130);

	public static StyleLength LABEL_HEIGHT = 30;
	public static StyleLength LABEL_FONT_SIZE = 18;
	public static StyleLength SMALL_LABEL_HEIGHT = 24;
	public static StyleLength SMALL_LABEL_FONT_SIZE = 14;

	public static StyleFloat BORDER_WIDTH = 2;

	public CustomPopupWindow(string title, bool darker = false)
	{
		this.style
			.SetFlex(false, Align.Center)
			.SetPadding(10)
			.SetBackgroundColor(darker ? SUBGROUND_COLOR : BACKGROUND_COLOR)
			.SetBorderRadius(10);

		if (title != "")
		{
			Label = new Label(title);
			Label.style
				.SetSize(StyleKeyword.Auto, darker ? SMALL_LABEL_HEIGHT : LABEL_HEIGHT)
				.SetForegroundColor(FOREGROUND_COLOR)
				.SetFontSize(darker ? SMALL_LABEL_FONT_SIZE : LABEL_FONT_SIZE);
			Label.style.borderBottomWidth = BORDER_WIDTH;
			Label.style.borderBottomColor = BORDER_COLOR;
			Label.style.marginBottom = 10;
			Label.style.paddingLeft = 30;
			Label.style.paddingRight = 30;
			Label.style.unityTextAlign = TextAnchor.MiddleCenter;

			this.Add(Label);
		}
	}
}