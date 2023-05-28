using System.Diagnostics;
using UnityEngine;
using Zenject;

public class CursorManipulatorInstaller : MonoInstaller
{
    [SerializeField] private CursorManipulator _manipulator;
    public override void InstallBindings()
    {
        Container.BindInstance(_manipulator).AsSingle().NonLazy();
    }
}