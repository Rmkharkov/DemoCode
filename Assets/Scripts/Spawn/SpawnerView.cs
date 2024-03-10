using System;
using System.Collections.Generic;
using System.Threading;
using Animals;
using Battle;
using UnityEngine;
using Zenject;

namespace Spawn
{
    public class SpawnerView : BaseView<SpawnerModel, SpawnerController>, ISpawner
    {
        public List<IAnimalLinks> LiveAnimals => Controller.liveAnimals;
        
        private CancellationTokenSource _cts;
        private DiContainer _container;
        private IDamageManager _damageManager;

        [Inject]
        private void Construct(DiContainer container, IDamageManager damageManager)
        {
            _container = container;
            _damageManager = damageManager;
        }
        
        private void Start()
        {
            Controller.Init(_container, _damageManager, gameObject);
            
            _cts = new CancellationTokenSource();
            Controller.StartSpawn(_cts.Token).Forget();
        }
    }
}