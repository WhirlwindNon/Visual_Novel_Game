using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GeneralInstaller : MonoInstaller
{
    [SerializeField] private DialogInstaller _dialogInstaller;
    public override void InstallBindings()
    {
        BindDialogInstaller();
    }

    private void BindDialogInstaller()
    {
        Container.Bind<DialogInstaller>().FromInstance(_dialogInstaller).AsCached();
    }
}
