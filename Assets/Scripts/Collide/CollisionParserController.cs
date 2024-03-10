using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Animals;
using Battle;
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
        
        public ReactiveCommand CollideWithFloor { get; } = new ReactiveCommand();
        private IDamageManager _damageManager;
        private ICollide _collide;

        public async UniTaskVoid Init(IDamageManager damageManager, ICollide collide, CancellationToken token)
        {
            _damageManager = damageManager;
            _collide = collide;
            await LoopSaveLastVelocity(token);
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Names.WallTag))
            {
                ReflectFromPoint(other.GetContact(0).point);
            }
            if (other.gameObject.CompareTag(Names.FloorTag))
            {
                CollideWithFloor.Execute();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Names.AnimalTag))
            {
                var links = other.gameObject.GetComponent<IAnimalLinks>();
                if (links != null)
                {
                    _damageManager.TryDamageOneOf(_collide, links.Collide);
                }
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
            UsedBody.transform.LookAt(curVelocity);
        }

        private async UniTask LoopSaveLastVelocity(CancellationToken token)
        {
            while (!token.IsCancellationRequested && UsedBody != null)
            {
                _lastVelocity = UsedBody.velocity;
                await UniTask.Yield();
            }
        }
    }
}