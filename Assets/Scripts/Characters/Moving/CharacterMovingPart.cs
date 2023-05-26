using UnityEngine;

namespace Characters.Moving
{
    public interface IMovable
    {
        void MoveTo(Vector3 target);
        float MoveSpeed { get; }
        Transform Transform { get; }
    }
    public class CharacterMovingPart : BaseView<CharacterMoveModel, CharacterMoveController>, IMovable
    {
        public float MoveSpeed => Model.MoveSpeed;
        public Transform Transform => transform;
        
        public void MoveTo(Vector3 target)
        {
            transform.position = target;
        }
    }
}