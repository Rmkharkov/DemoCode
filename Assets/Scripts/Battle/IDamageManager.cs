using Collide;
using UniRx;

namespace Battle
{
    public interface IDamageManager
    {
        void TryDamageOneOf(ICollide me, ICollide enemy);
        bool IsBothPrey(ICollide me, ICollide enemy);
        ReactiveCommand<IDamageable> Damaged { get; }
    }
}