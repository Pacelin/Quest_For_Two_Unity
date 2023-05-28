using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CursorArea : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorImage;

    private bool _inArea;

    protected virtual void Update()
    {
        var mousePosition = Input.mousePosition;
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform) transform, mousePosition))
        {
            if (!_inArea)
            {
                _inArea = true;
                Cursor.SetCursor(_cursorImage, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            if (_inArea)
            {
                _inArea = false;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }
}
