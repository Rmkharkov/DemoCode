using Animals;
using UniRx;

namespace Collide
{
    public interface ICollide
    {
        ReactiveCommand<IAnimalLinks> CollidedWithAnimal { get; }
        ReactiveCommand CollideWithFloor { get; }
    }
}