﻿using System;
using System.Threading;
using Animals;
using Collide;
using Spawn;
using UniRx;
using Zenject;

namespace Battle
{
    public class DamageManagerView : BaseView<DamageManagerModel, DamageManagerController>, IDamageManager
    {
        public void TryDamageOneOf(ICollide me, ICollide enemy) => Controller.TryDamageOneOf(me, enemy);

        public ReactiveCommand<IDamageable> Damaged => Controller.Damaged;
        public ReactiveCommand<IAnimalLinks> WinnerAnimal => Controller.WinnerAnimal;

        private ISpawner _spawner;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        [Inject]
        private void Construct(ISpawner spawner)
        {
            _spawner = spawner;
        }

        private void Start()
        {
            Controller.Init(_spawner, _cts.Token).Forget();
        }
    }
}