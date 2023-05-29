using System.Collections;
using UnityEngine;

public class LocationSwitch : MonoBehaviour, ISwitch<Location>
{
    [SerializeField] private Location _startLocation;
    [Space]
    [SerializeField] private float _fadeOutTime;
    [Space]
    [SerializeField] private float _fadeInTime;
    [SerializeField] private float _fadeInOffset;

    private Location _current;

    private void Start()
    {
        _startLocation.CanvasGroup.alpha = 1;
        _startLocation.CanvasGroup.blocksRaycasts = true;
        _startLocation.CanvasGroup.interactable = true;
        _current = _startLocation;
    }

    public void Switch(Location to)
    {
        StartCoroutine(FadeOut(_current));
        StartCoroutine(FadeIn(to));
    }

    private IEnumerator FadeIn(Location inLoc)
    {
        yield return new WaitForSeconds(_fadeInOffset);

        for (float t = 0; t < _fadeInTime; t += Time.deltaTime)
        {
            inLoc.CanvasGroup.alpha = Mathf.Lerp(0, 1, t / _fadeInTime);
            yield return null;
        }
        inLoc.CanvasGroup.alpha = 1;

        inLoc.CanvasGroup.blocksRaycasts = true;
        inLoc.CanvasGroup.interactable = true;
        _current = inLoc;
    }

    private IEnumerator FadeOut(Location outLoc)
    {
        outLoc.CanvasGroup.blocksRaycasts = false;
        outLoc.CanvasGroup.interactable = false;

        for (float t = 0; t < _fadeOutTime; t += Time.deltaTime)
        {
            outLoc.CanvasGroup.alpha = Mathf.Lerp(1, 0, t / _fadeOutTime);
            yield return null;
        }
        outLoc.CanvasGroup.alpha = 0;
    }
}