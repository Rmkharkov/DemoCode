using Battle;
using Spawn;
using UniRx;
using UnityEngine;

namespace UI
{
    public class DeathsCounterController : BaseController<DeathsCounterModel>
    {
        private ISpawner _spawner;
        private int _count;
        public void Init(IDamageManager damageManager, ISpawner spawner, GameObject disposable)
        {
            _spawner = spawner;
            damageManager.Damaged.Subscribe(OnDeath).AddTo(disposable);
        }

        private void OnDeath(IDamageable damageable)
        {
            var animal = _spawner.LiveAnimals.Find(c => c.Damageable == damageable);
            if (animal.AnimalSide == Model.AnimalSide)
            {
                _count++;
                Model.CounterText.text = _count.ToString();
            }
        }
    }
}