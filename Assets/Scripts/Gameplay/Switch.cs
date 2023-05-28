using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private Switchable _first;
    
    private Switchable _current;

    private void Start()
    {
        _current = _first;
        _current.SwitchToOn();
    }

    public void SwitchTo(Switchable to)
    {
        _current.SwitchToOff();
        _current = to;
        _current.SwitchToOn();
    }
}