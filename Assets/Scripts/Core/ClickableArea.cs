using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class ClickableArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InputAction ActionOnLMB;
    [SerializeField] private InputAction ActionOnRMB;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            ActionOnLMB?.ApplyAction();
        else if (eventData.button == PointerEventData.InputButton.Right)
            ActionOnRMB?.ApplyAction();
    }
}