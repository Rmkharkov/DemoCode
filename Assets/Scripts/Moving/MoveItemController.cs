using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Moving
{
    public class MoveItemController : BaseController<MoveItemModel>
    {
        private MoveParameters _moveParameters;
        private Transform UsedTransform => Model.Body.transform;
        private Rigidbody UsedBody => Model.Body;

        private ReactiveProperty<EMoveState> MoveState { get; } = new ReactiveProperty<EMoveState>();

        public void SetFeltEvent(ReactiveCommand collideWithFloor, GameObject disposable)
        {
            collideWithFloor.Subscribe(_ => FeltOnFloor()).AddTo(disposable);
        }
        
        public void SetParameters(MoveParameters parameters)
        {
            _moveParameters = parameters;
        }

        public async UniTaskVoid RandomMove(CancellationToken token)
        {
            StopBody();
            
            var rotateAngle = UnityEngine.Random.Range(0f, 360f);
            UsedTransform.Rotate(Vector3.up, rotateAngle);

            switch (_moveParameters.MoveType)
            {
                case EMoveType.Linear:
                    MoveState.SetValueAndForceNotify(EMoveState.Linear);
                    var addForce = UsedTransform.forward * _moveParameters.MoveForce;
                    UsedBody.AddForce(addForce);
                    break;
                case EMoveType.Jump:
                    while (!token.IsCancellationRequested)
                    {
                        MoveState.SetValueAndForceNotify(EMoveState.Jump);
                        await SingleJump(token);
                        await UniTask.Delay(_moveParameters.JumpDelay, cancellationToken: token);
                    }
                    break;
            }
        }

        private async UniTask SingleJump(CancellationToken token)
        {
            var addForce = UsedTransform.forward * _moveParameters.MoveForce + UsedTransform.up * _moveParameters.JumpUpForce;
            UsedBody.AddForce(addForce);
            do
            {
                await UniTask.Yield();
            } while (MoveState.Value == EMoveState.Jump);
            StopBody();
        }

        private void StopBody()
        {
            UsedBody.velocity = Vector3.zero;
            UsedBody.angularVelocity = Vector3.zero;
        }

        private void FeltOnFloor()
        {
            MoveState.SetValueAndForceNotify(EMoveState.Stand);
        }
    }
}