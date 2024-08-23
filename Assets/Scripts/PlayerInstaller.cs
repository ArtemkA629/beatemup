using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _playerInstance;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(_playerInstance).AsSingle().NonLazy();
    }
}
