using UnityEngine;
using Zenject;

public class LocationTransfer : InputAction
{
    [SerializeField] private Location _nextLocation;
    [Inject] private ISwitch<Location> _locationSwitch;

    public override void ApplyAction() =>
        _locationSwitch.Switch(to: _nextLocation);
}