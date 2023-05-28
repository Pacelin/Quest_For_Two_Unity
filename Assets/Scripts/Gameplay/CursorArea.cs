using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(RectTransform))]
public class CursorArea : MonoBehaviour
{
    [SerializeField] private CursorData _cursorData;
    [Inject] private CursorManipulator _cursor;

    private bool _inArea;

    protected virtual void Update()
    {
        var mousePosition = Input.mousePosition;
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform) transform, mousePosition))
        {
            if (!_inArea)
            {
                _inArea = true;
                _cursor.AddCursor(_cursorData);
            }
        }
        else
        {
            if (_inArea)
            {
                _inArea = false;
                _cursor.RemoveCursor(_cursorData);
            }
        }
    }
}