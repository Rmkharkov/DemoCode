using Animals;
using UniRx;

namespace Collide
{
    public interface ICollide
    {
        ReactiveCommand CollideWithFloor { get; }
    }
}