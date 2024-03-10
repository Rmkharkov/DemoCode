using UniRx;
using UnityEngine;

namespace Moving
{
    public interface IMovable
    {
        void SetFeltEvent(ReactiveCommand collideWithFloor);
        void SetDeadEvent(ReactiveCommand dead);
        void SetParameters(MoveParameters moveParameters);
        void SetPositionAndParent(Vector3 position, Transform parent);
        void RandomMove();
        Vector3 Position { get; }
    }
}