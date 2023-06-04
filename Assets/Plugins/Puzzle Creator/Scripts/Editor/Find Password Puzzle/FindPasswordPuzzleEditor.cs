using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FindPasswordPuzzleEditor : EditorWindow
{
	private FindPasswordPuzzleEditorView _view;

	[MenuItem("GameObject/Puzzles/Find Password Puzzle", false, 10)]
	public static void Open()
	{
		var window = GetWindow<FindPasswordPuzzleEditor>();

		window.InitWindow();
		window.Show();
	}
	
	private void InitWindow()
	{
		titleContent = new GUIContent("Find Password Puzzle");
		
		_view = new FindPasswordPuzzleEditorView();
		_view.StretchToParentSize();
		rootVisualElement.Add(_view);

		this.minSize = new Vector2(1000, 595);

		_view.PasswordView.CreatePuzzleButton.OnClick += GeneratePuzzle;
	}

	private void GeneratePuzzle()
	{
		_view.PasswordView.CreatePuzzleButton.OnClick -= GeneratePuzzle;

		var puzzleObject = GeneratePuzzleObject(out var serializedPuzzleObject, out var slotsArray);

		var slots = _view.PasswordView.Slots;
		var slotsObjects = new PasswordSlot[slots.Length];
		for (int i = 0; i < slots.Length; i++)
			slotsObjects[i] = GeneratePasswordSlot(puzzleObject.transform, slots[i].Item1, slots[i].Item2);

		slotsArray.SetArray(slotsObjects);
		serializedPuzzleObject.ApplyModifiedPropertiesWithoutUndo();
		this.Close();
	}

	private FindPasswordPuzzle GeneratePuzzleObject(out SerializedObject serialized, out SerializedProperty slotsArray)
	{
		var puzzleObject = 
			CustomObjectsUtility.CreatePrefab("Prefabs/Find Password/PasswordPuzzle")
			.GetComponent<FindPasswordPuzzle>();
		serialized = new SerializedObject(puzzleObject);
		slotsArray = serialized.FindProperty("_slots");

		return puzzleObject;
	}

	private PasswordSlot GeneratePasswordSlot(Transform parent, Sprite[] sprites, int correctValue)
	{
		var slotObject = 
			CustomObjectsUtility.CreatePrefab("Prefabs/Find Password/PasswordSlot", parent)
			.GetComponent<PasswordSlot>();

		var serialized = new SerializedObject(slotObject);
		var spritesProp = serialized.FindProperty("_sprites");
		var correctIndexProp = serialized.FindProperty("_correctSpriteIndex");
		
		spritesProp.SetArray(sprites);
		correctIndexProp.intValue = correctValue - 1;

		serialized.ApplyModifiedPropertiesWithoutUndo();
		slotObject.Reset();
		return slotObject;
	}
}
