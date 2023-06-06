using UnityEngine;
using UnityEngine.Events;

public class SpritesCombination : MonoBehaviour
{
    public UnityEvent OnSolve;
    [SerializeField] private SpriteSwitch[] _switches;
    [SerializeField] private int[] _correctSprites;

    public void Validate()
    {
        if (!this.gameObject.activeInHierarchy || !this.enabled) return;
        for (int i = 0; i < _switches.Length; i++)
            if (_correctSprites[i] != _switches[i].SpriteIndex)
                return;
        
        OnSolve?.Invoke();
    }
}
