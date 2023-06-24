using UnityEngine;

namespace Characters.Moving
{
    public interface IMovableData
    {
        float MoveSpeed { get; }
        Transform Transform { get; }
    }
}