using UnityEngine;

namespace Characters.Moving
{
    public interface IMovable
    {
        void MoveTo(Vector3 target);
    }
}