using UnityEngine;

public abstract class Switchable : MonoBehaviour
{
    public abstract void Switch();
    public abstract void SwitchToOn();
    public abstract void SwitchToOff();
}