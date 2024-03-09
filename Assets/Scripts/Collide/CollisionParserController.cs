using System;
using System.Threading;
using Animals;
using Cysharp.Threading.Tasks;
using Misc;
using UniRx;
using UnityEngine;

namespace Collide
{
    public class CollisionParserController : BaseController<CollisionParserModel>
    {
        private Vector3 _lastVelocity;
        private Rigidbody UsedBody => Model.Body;
        private Transform UsedTransform => Model.Body.transform;
        private CollisionsConfig UsedCollisionsConfig => CollisionsConfig.Instance;
        
        public ReactiveCommand<IAnimalLinks> CollidedWithAnimal { get; } = new ReactiveCommand<IAnimalLinks>();
        public ReactiveCommand CollideWithFloor { get; } = new ReactiveCommand();

        public async UniTaskVoid Init(CancellationToken token)
        {
            await LoopSaveLastVelocity(token);
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Names.WallTag))
            {
                ReflectFromPoint(other.GetContact(0).point);
            }
            
            if (other.gameObject.CompareTag(Names.AnimalTag))
            {
                var links = other.gameObject.GetComponent<IAnimalLinks>();
                if (links != null)
                {
                    CollidedWithAnimal.Execute(links);
                }
            }
            
            if (other.gameObject.CompareTag(Names.FloorTag))
            {
                CollideWithFloor.Execute();
            }
        }

        private void ReflectFromPoint(Vector3 point)
        {
            var position = UsedTransform.position;
            var curVelocity = _lastVelocity;
            if (Mathf.Abs(Mathf.Abs(position.x) - Mathf.Abs(point.x)) > UsedCollisionsConfig.MinReflectDistance)
            {
                curVelocity.x *= -1f;
            }

            if (Mathf.Abs(Mathf.Abs(position.z) - Mathf.Abs(point.z)) > UsedCollisionsConfig.MinReflectDistance)
            {
                curVelocity.z *= -1f;
            }

            UsedBody.velocity = curVelocity;
        }

        private async UniTask LoopSaveLastVelocity(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _lastVelocity = UsedBody.velocity;
                await UniTask.Yield();
            }
        }
    }
}