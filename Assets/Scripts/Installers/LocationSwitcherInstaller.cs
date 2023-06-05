using UnityEngine;
using Zenject;

public class LocationSwitcherInstaller : MonoInstaller
{
    //[SerializeField] private LocationSwitch _locationSwitch;
    public override void InstallBindings()
    {
        //Container.BindInstance<ISwitch<Location>>(_locationSwitch).AsSingle().NonLazy();
    }
}