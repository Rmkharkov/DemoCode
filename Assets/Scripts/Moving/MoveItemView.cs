using System;
using System.Threading;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace Moving
{
    public class MoveItemView : BaseView<MoveItemModel, MoveItemController>, IMovable
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();
        
        public void SetFeltEvent(ReactiveCommand collideWithFloor) => Controller.SetFeltEvent(collideWithFloor, gameObject);
        
        public void SetDeadEvent(ReactiveCommand dead)
        {
            dead.Subscribe(_ => OnDeath()).AddTo(this);
        }

        private void OnDeath()
        {
            _cts.Cancel();
            Controller.OnDead();
        }

        public void SetParameters(MoveParameters moveParameters) => Controller.SetParameters(moveParameters);

        public void SetPositionAndParent(Vector3 position, Transform parent)
        {
            transform.position = position;
            transform.SetParent(parent);
        }

        public void RandomMove()
        {
            _cts = new CancellationTokenSource();
            Controller.RandomMove(_cts.Token).Forget();
        }
        public Vector3 Position => Model.Body.position;
    }
}