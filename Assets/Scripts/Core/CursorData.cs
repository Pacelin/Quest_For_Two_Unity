using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cursor Data")]
public class CursorData : ScriptableObject
{
    [SerializeField] private Texture2D _texture;
    [SerializeField] private Vector2 _hotspot;

    private static List<CursorData> _cursors = new List<CursorData>(4);

    public void ApplyCursor()
    {
        SetCursor(this);
        _cursors.Add(this);
    }
    
    public void DenyCursor()
    {
        if (!_cursors.Contains(this)) return;

        var index = _cursors.IndexOf(this);
        _cursors.Remove(this);

        if (index == _cursors.Count)
        {
            if (_cursors.Count == 0)
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            else
                SetCursor(_cursors[index - 1]);
        }
    }

    private void SetCursor(CursorData data) =>
        Cursor.SetCursor(data._texture, data._hotspot, CursorMode.Auto);
}