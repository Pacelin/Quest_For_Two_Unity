using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Location : MonoBehaviour
{
    [field: SerializeField] public bool IsActive { get; private set; }
    
    private CanvasGroup _canvasGroup;
    private bool _inAction;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = IsActive ? 1 : 0;
        _canvasGroup.blocksRaycasts = IsActive;
        _canvasGroup.interactable = IsActive;
        _inAction = false;
    }

    public IEnumerator Show(float delay, float duration)
    {
        if (IsActive) yield break;
        
        yield return new WaitWhile(() => _inAction);

        _inAction = true;

        IsActive = true;
        yield return _canvasGroup.FadeAlpha(delay, duration, 0, 1);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        _inAction = false;
    }

    public IEnumerator Hide(float delay, float duration)
    {
        if (!IsActive) yield break;
        
        yield return new WaitWhile(() => _inAction);

        _inAction = true;

        IsActive = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        yield return _canvasGroup.FadeAlpha(delay, duration, 1, 0);

        _inAction = false;
    }
}
