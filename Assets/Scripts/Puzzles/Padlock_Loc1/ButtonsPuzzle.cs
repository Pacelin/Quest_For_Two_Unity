using UnityEngine;

public class ButtonsPuzzle : MonoBehaviour
{
    [SerializeField] private StateSwitcher[] _buttonStates;
    [SerializeField] private bool[] _correctStates;
    [Space]
    [SerializeField] private InputAction[] ActionsOnSolve;

    private void Awake()
    {
        foreach(var btn in _buttonStates)
            btn.OnChange += OnChangeButtonState;
    }

    private void OnChangeButtonState()
    {
        for (int i = 0; i < _buttonStates.Length; i++)
            if (_buttonStates[i].IsActive != _correctStates[i])
                return;
        
        foreach (var action in ActionsOnSolve)
            action?.ApplyAction();
    }

}
