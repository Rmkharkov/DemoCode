using UnityEngine;
using UnityEngine.Events;

namespace Characters.Fight
{
    public interface IDamageable
    {
        void ApplyDamage();
        UnityEvent DeathEvent { get; }
    }
    public class CharacterGetHitPart : BaseView<CharacterGetHitModel, CharacterGetHitController>, IDamageable
    {

        private readonly UnityEvent _deathEvent = new UnityEvent();
        public UnityEvent DeathEvent => _deathEvent;
        public void ApplyDamage()
        {
            _deathEvent.Invoke();
        }
    }
}