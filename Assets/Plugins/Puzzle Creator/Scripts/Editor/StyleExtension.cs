using UnityEngine;
using UnityEngine.UIElements;

public static class StyleExtension
{
	public static IStyle SetPositionAbsolute(this IStyle style)
	{
		style.position = Position.Absolute;
		return style;
	}

	public static IStyle SetTop(this IStyle style, StyleLength top)
	{
		style.top = top;
		return style;
	}

	public static IStyle SetRight(this IStyle style, StyleLength right)
	{
		style.right = right;
		return style;
	}

	public static IStyle SetBottom(this IStyle style, StyleLength bottom)
	{
		style.bottom = bottom;
		return style;
	}

	public static IStyle SetLeft(this IStyle style, StyleLength left)
	{
		style.left = left;
		return style;
	}

	public static IStyle SetSize(this IStyle style, StyleLength width, StyleLength height)
	{
		style.width = width;
		style.height = height;
		return style;
	}
	public static IStyle SetWidth(this IStyle style, StyleLength width)
	{
		style.width = width;
		return style;
	}
	public static IStyle SetHeight(this IStyle style, StyleLength height)
	{
		style.height = height;
		return style;
	}

	public static IStyle SetFlex(this IStyle style, bool row, Align align)
	{
		style.display = DisplayStyle.Flex;
		style.alignItems = align;
		style.flexDirection = row ? FlexDirection.Row : FlexDirection.Column;
		return style;
	}

	public static IStyle SetPadding(this IStyle style, StyleLength padding)
	{
		style.paddingTop = padding;
		style.paddingRight = padding;
		style.paddingBottom = padding;
		style.paddingLeft = padding;
		return style;
	}

	public static IStyle SetMargin(this IStyle style, StyleLength margin)
	{
		style.marginTop = margin;
		style.marginRight = margin;
		style.marginBottom = margin;
		style.marginLeft = margin;
		return style;
	}

	public static IStyle SetBorderWidth(this IStyle style, float borderWidth)
	{
		style.borderTopWidth = borderWidth;
		style.borderRightWidth = borderWidth;
		style.borderBottomWidth = borderWidth;
		style.borderLeftWidth = borderWidth;
		return style;
	}
	public static IStyle SetBorderColor(this IStyle style, Color borderColor)
	{
		style.borderTopColor = borderColor;
		style.borderRightColor = borderColor;
		style.borderBottomColor = borderColor;
		style.borderLeftColor = borderColor;
		return style;
	}

	public static IStyle SetBorderRadius(this IStyle style, StyleLength radius)
	{
		style.borderBottomLeftRadius = radius;
		style.borderBottomRightRadius = radius;
		style.borderTopLeftRadius = radius;
		style.borderTopRightRadius = radius;
		return style;
	}

	public static IStyle SetFontSize(this IStyle style, StyleLength size)
	{
		style.fontSize = size;
		return style;
	}

	public static IStyle SetForegroundColor(this IStyle style, Color color)
	{
		style.color = color;
		return style;
	}
	public static IStyle SetBackgroundColor(this IStyle style, Color color)
	{
		style.backgroundColor = color;
		return style;
	}

	public static IStyle SetVisible(this IStyle style)
	{
		style.visibility = Visibility.Visible;
		return style;
	}
	public static IStyle SetInvisible(this IStyle style)
	{
		style.visibility = Visibility.Hidden;
		return style;
	}

	public static IStyle SetGrow(this IStyle style)
	{
		style.flexGrow = 1;
		return style;
	}

	public static IStyle SetMarginBottom(this IStyle style, int margin)
	{
		style.marginBottom = margin;
		return style;
	}

	public static IStyle SetWidthFill(this IStyle style)
	{
		style.width = Length.Percent(100);
		return style;
	}
}