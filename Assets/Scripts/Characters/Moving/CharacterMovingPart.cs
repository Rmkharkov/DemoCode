using UnityEngine;

namespace Characters.Moving
{
    public class CharacterMovingPart : BaseView<CharacterMoveModel, CharacterMoveController>, IMovable, IMovableData
    {
        public float MoveSpeed => Model.MoveSpeed;
        public Transform Transform => transform;
        
        public void MoveTo(Vector3 target)
        {
            transform.position = target;
        }
    }
}