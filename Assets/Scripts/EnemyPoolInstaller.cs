using UnityEngine;
using Zenject;

public class EnemyPoolInstaller : MonoInstaller
{
    [SerializeField] private EnemyPool _enemyPoolInstance;

    public override void InstallBindings()
    {
        Container.Bind<EnemyPool>().FromInstance(_enemyPoolInstance).AsSingle().NonLazy();
    }
}
