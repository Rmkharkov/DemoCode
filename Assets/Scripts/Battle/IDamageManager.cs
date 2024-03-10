using Animals;
using Collide;
using UniRx;

namespace Battle
{
    public interface IDamageManager
    {
        void TryDamageOneOf(ICollide me, ICollide enemy);
        ReactiveCommand<IDamageable> Damaged { get; }
        ReactiveCommand<IAnimalLinks> WinnerAnimal { get; }
    }
}