using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class CustomObjectsUtility
{
	public static GameObject CreatePrefab(string path)
	{
		GameObject obj;
		if (Selection.activeGameObject != null)
			obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load(path), Selection.activeGameObject.transform);
		else
			obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load(path));

		Prepare(obj);
		return obj;
	}

	public static GameObject CreatePrefab(string path, Transform parent)
	{
		var obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load(path), parent);
		Prepare(obj);
		return obj;
	}

	public static void Prepare(GameObject obj)
	{
		StageUtility.PlaceGameObjectInCurrentStage(obj);
		GameObjectUtility.EnsureUniqueNameForSibling(obj);

		Undo.RegisterCreatedObjectUndo(obj, "Create Object: " + obj.name);

		EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
	}
}