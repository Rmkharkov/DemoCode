using UniRx;

namespace Battle
{
    public interface IDamageable
    {
        ReactiveCommand GetDamaged { get; }
    }
}