using System;
using System.Threading;
using Animals;
using Battle;
using UniRx;
using UnityEngine;
using Zenject;

namespace Collide
{
    public class CollisionParserView : BaseView<CollisionParserModel, CollisionParserController>, ICollide
    {
        public ReactiveCommand CollideWithFloor => Controller.CollideWithFloor;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private IDamageManager _damageManager;

        [Inject]
        private void Construct(IDamageManager damageManager)
        {
            _damageManager = damageManager;
        }

        private void Start()
        {
            Controller.Init(_damageManager, this, _cts.Token).Forget();
        }

        private void OnCollisionEnter(Collision other) => Controller.OnCollisionEnter(other);

        private void OnTriggerEnter(Collider other) => Controller.OnTriggerEnter(other);

        private void OnApplicationQuit()
        {
            _cts.Cancel();
        }

    }
}