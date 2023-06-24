using System;

namespace Characters.Fight
{
    public class CharacterGetHitPart : BaseView<CharacterGetHitModel, CharacterGetHitController>, IDamageable
    {
        public event Action OnDeathEvent;
        public void ApplyDamage()
        {
            OnDeathEvent?.Invoke();
        }
    }
}