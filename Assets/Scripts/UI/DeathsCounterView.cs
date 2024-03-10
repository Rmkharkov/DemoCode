using System;
using Battle;
using Spawn;
using Zenject;

namespace UI
{
    public class DeathsCounterView : BaseView<DeathsCounterModel, DeathsCounterController>
    {
        private IDamageManager _damageManager;
        private ISpawner _spawner;

        [Inject]
        private void Construct(IDamageManager damageManager, ISpawner spawner)
        {
            _damageManager = damageManager;
            _spawner = spawner;
        }

        private void Start()
        {
            Controller.Init(_damageManager, _spawner, gameObject);
        }
    }
}