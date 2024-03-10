using Battle;
using Spawn;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private DamageManagerView damageManagerView;
        [SerializeField] private SpawnerView spawnerView;
        public override void InstallBindings()
        {
            Container.Bind<IDamageManager>().FromInstance(damageManagerView).AsSingle();
            Container.Bind<ISpawner>().FromInstance(spawnerView).AsSingle();
        }
    }
}