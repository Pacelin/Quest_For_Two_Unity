using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class ClickableArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InputAction[] ActionsOnLMB;
    [SerializeField] private InputAction[] ActionsOnRMB;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            foreach (var action in ActionsOnLMB)
                action?.ApplyAction();
        else if (eventData.button == PointerEventData.InputButton.Right)
            foreach (var action in ActionsOnRMB)
                action?.ApplyAction();
    }
}