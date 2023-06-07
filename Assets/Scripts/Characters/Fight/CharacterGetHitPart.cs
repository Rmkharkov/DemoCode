using System;
using UnityEngine.Events;

namespace Characters.Fight
{
    public interface IDamageable
    {
        void ApplyDamage();
        event Action OnDeathEvent;
    }
    public class CharacterGetHitPart : BaseView<CharacterGetHitModel, CharacterGetHitController>, IDamageable
    {
        public event Action OnDeathEvent;
        public void ApplyDamage()
        {
            OnDeathEvent?.Invoke();
        }
    }
}