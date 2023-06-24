using System;

namespace Characters.Fight
{
    public interface IDamageable
    {
        void ApplyDamage();
        event Action OnDeathEvent;
    }
}