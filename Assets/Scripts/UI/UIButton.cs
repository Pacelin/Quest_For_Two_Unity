using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerClickHandler
{
    public event System.Action<UIButton> OnLMBClick;
    public event System.Action<UIButton> OnRMBClick;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLMBClick?.Invoke(this);
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRMBClick?.Invoke(this);
    }
}
