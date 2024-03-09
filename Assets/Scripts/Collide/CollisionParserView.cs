using System;
using System.Threading;
using Animals;
using UniRx;
using UnityEngine;

namespace Collide
{
    public class CollisionParserView : BaseView<CollisionParserModel, CollisionParserController>, ICollide
    {
        public ReactiveCommand<IAnimalLinks> CollidedWithAnimal => Controller.CollidedWithAnimal;
        public ReactiveCommand CollideWithFloor => Controller.CollideWithFloor;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private void Start()
        {
            Controller.Init(_cts.Token).Forget();
        }

        private void OnCollisionEnter(Collision other) => Controller.OnCollisionEnter(other);

        private void OnApplicationQuit()
        {
            _cts.Cancel();
        }

    }
}