using UnityEngine;

[CreateAssetMenu(menuName = "Cursor Data")]
public class CursorData : ScriptableObject
{
    [SerializeField] private Texture2D _texture;
    [SerializeField] private Vector2 _hotspot;

    public void ApplyCursor() =>
        Cursor.SetCursor(_texture, _hotspot, CursorMode.Auto);
}