using System;
using System.Threading;
using Battle;
using Zenject;

namespace BattleTalk
{
    public class BattleTalkManagerView : BaseView<BattleTalkManagerModel, BattleTalkManagerController>
    {
        private IDamageManager _damageManager;
        private DiContainer _container;
        private CancellationTokenSource _cts;
        
        [Inject]
        private void Construct(IDamageManager damageManager, DiContainer container)
        {
            _damageManager = damageManager;
            _container = container;
        }

        private void Start()
        {
            _cts = new CancellationTokenSource();
            Controller.Init(_container, _damageManager, _cts.Token, gameObject);
        }

        private void OnApplicationQuit()
        {
            _cts.Cancel();
        }
    }
}