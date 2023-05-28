using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CursorManipulator : MonoBehaviour
{
    [SerializeField] private CursorData _defaultCursor;

    private List<CursorData> _datas = new List<CursorData>();

    public void AddCursor(CursorData data)
    {
        _datas.Add(data);
        data.ApplyCursor();
    }

    public void RemoveCursor(CursorData data)
    {
        _datas.Remove(data);
        
        if (_datas.Count > 0)
            _datas.Last().ApplyCursor();
        else
            _defaultCursor.ApplyCursor();
    }
}
