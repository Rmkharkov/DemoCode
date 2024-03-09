using System;
using System.Threading;

namespace Spawn
{
    public class SpawnerView : BaseView<SpawnerModel, SpawnerController>
    {
        private CancellationTokenSource _cts;
        private void Start()
        {
            _cts = new CancellationTokenSource();
            Controller.StartSpawn(_cts.Token).Forget();
        }
    }
}