using System;
using UniRx;
using Zenject;

namespace Battle
{
    public class DamageableView : BaseView<DamageableModel, DamageableController>, IDamageable
    {
        public ReactiveCommand GetDamaged => Controller.GetDamaged;

        private IDamageManager _damageManager;

        [Inject]
        private void Construct(IDamageManager damageManager)
        {
            _damageManager = damageManager;
        }

        private void Start()
        {
            _damageManager.Damaged.Subscribe(c => Controller.GetDamage(c, this)).AddTo(this);
        }
    }
}