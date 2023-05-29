using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Location : MonoBehaviour
{
    public string Name => _name;
    public CanvasGroup CanvasGroup => _canvasGroup;

    [SerializeField] private string _name;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }
}
