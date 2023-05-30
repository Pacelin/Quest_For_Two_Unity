using UnityEngine;
using UnityEngine.UI;

public class StateSwitcher : InputAction
{
    public event System.Action OnChange;
    
    public bool IsActive => _isActive;
    [SerializeField] private bool _isActive;

    public override void ApplyAction()
    {
        _isActive = !_isActive;
        OnChange?.Invoke();
    }
}