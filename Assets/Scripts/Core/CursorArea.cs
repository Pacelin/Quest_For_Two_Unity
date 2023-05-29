using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class CursorArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CursorData _cursorData;

    private void Awake() =>
        _cursorData = Instantiate(_cursorData);

    private void OnDisable() =>
        _cursorData.DenyCursor();

    public void OnPointerEnter(PointerEventData eventData) =>
        _cursorData.ApplyCursor();
    public void OnPointerExit(PointerEventData eventData) =>
        _cursorData.DenyCursor();
}