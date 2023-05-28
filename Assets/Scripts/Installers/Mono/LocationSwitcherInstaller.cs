using System.Diagnostics;
using UnityEngine;
using Zenject;

public class LocationSwitcherInstaller : MonoInstaller
{
    [SerializeField] private Switch _locationSwitch;
    public override void InstallBindings()
    {
        Container.BindInstance(_locationSwitch).AsSingle().NonLazy();
    }
}