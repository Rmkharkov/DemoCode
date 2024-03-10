using System.Threading;
using System.Threading.Tasks;
using Animals;
using Battle;
using Cysharp.Threading.Tasks;
using Moving;
using UniRx;
using UnityEngine;
using Zenject;

namespace BattleTalk
{
    public partial class BattleTalkManagerController : BaseController<BattleTalkManagerModel>
    {
        private DiContainer _container;
        private CancellationToken _token;
        private BattleTalksConfig UsedBattleTalksConfig => BattleTalksConfig.Instance;

        public void Init(DiContainer container, IDamageManager damageManager, CancellationToken token,
            GameObject disposable)
        {
            _container = container;
            _token = token;
            damageManager.WinnerAnimal.Subscribe(OnSomeoneWin).AddTo(disposable);
        }

        private void OnSomeoneWin(IAnimalLinks winnerAnimal)
        {
            PlaceWinnerTalk(winnerAnimal).Forget();
        }

        private async UniTaskVoid PlaceWinnerTalk(IAnimalLinks target)
        {
            await WinnerTalkFlowTask(target);
        }

        private async UniTask WinnerTalkFlowTask(IAnimalLinks target)
        {
            var talk = await GetTalk();
            talk.SetParent(Model.BattleTalksLiveParent);
            var startTime = Time.time;
            while (startTime + UsedBattleTalksConfig.TalkLiveTime > Time.time && target != null &&
                   target.Damageable.IsItLive && !_token.IsCancellationRequested)
            {
                talk.position = Camera.main.WorldToScreenPoint(target.Movable.Position);
                await Task.Yield();
            }

            HideTalk(talk);
        }
    }
}