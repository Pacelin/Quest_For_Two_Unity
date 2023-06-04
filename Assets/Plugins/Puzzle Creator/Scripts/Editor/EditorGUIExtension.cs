using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public static class EditorGUIExtension
{
	public static void Blink(this IStyle style, StyleColor defaultColor, StyleColor blinkColor, int iterations, int msBlink)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < iterations; i++)
			{
				style.color = blinkColor;
				Task.Delay(msBlink).Wait();
				style.color = defaultColor;
				Task.Delay(msBlink).Wait();
			}
		});
	}



	public static Length Negative(this Length length)
	{
		return new Length(-length.value, length.unit);
	}

	public static void SetArray(this SerializedProperty prop, Object[] array)
	{
		prop.arraySize = array.Length;
		for (int i = 0; i < array.Length; i++)
			prop.GetArrayElementAtIndex(i).objectReferenceValue = array[i];
	}
}
