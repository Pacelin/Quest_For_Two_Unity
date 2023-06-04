using System.Collections;
using UnityEngine;

public static class UITweens
{
    public static IEnumerator FadeAlpha(this CanvasGroup group, float delay, float duration,
        float startAlpha, float endAlpha)
    {
        yield return new WaitForSeconds(delay);

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            group.alpha = Mathf.Lerp(startAlpha, endAlpha, t / duration);
            yield return null;
        }
        group.alpha = endAlpha;
    }
}
