using UnityEngine;

public class Location : Switchable
{
    public string Name => _name;
    [SerializeField] private string _name;

    private void Awake() => SwitchToOff();

    public override void Switch() => gameObject.SetActive(!gameObject.activeSelf);
    public override void SwitchToOn() => gameObject.SetActive(true);
    public override void SwitchToOff() => gameObject.SetActive(false);
}
