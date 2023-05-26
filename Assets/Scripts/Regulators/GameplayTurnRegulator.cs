using System;
using System.Threading.Tasks;
using Characters;
using Regulators.ItemNumberRegulator;
using UI.ResultScreen;
using UnityEngine;
using UnityEngine.Events;

namespace Regulators
{
    public interface IGameplayTurnRegulator
    {
        UnityEvent StartMatchEvent { get; }
        UnityEvent CleanMatchEvent { get; }
        UnityEvent StopMatchEvent { get; }
    }
    public class GameplayTurnRegulator : MonoBehaviour, IGameplayTurnRegulator
    {
        public static IGameplayTurnRegulator Instance => _instance;
        private static GameplayTurnRegulator _instance;

        private readonly UnityEvent _startMatch = new UnityEvent();
        public UnityEvent StartMatchEvent => _startMatch;
        
        private readonly UnityEvent _cleanMatch = new UnityEvent();
        public UnityEvent CleanMatchEvent => _cleanMatch;
        
        private readonly UnityEvent _stopMatch = new UnityEvent();
        public UnityEvent StopMatchEvent => _stopMatch;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            PlayerRegulatorView.Instance.PlayerUpdated.AddListener(SubscribeOnPlayerDeathEvent);
            UIResultScreenView.Instance.ResultPanelClosed.AddListener(RestartMatch);

            RestartMatch();
        }

        private void SubscribeOnPlayerDeathEvent(GameplayItemLinks player)
        {
            player.DeathFlow.DeathStart.AddListener(StopMatch);
            player.DeathFlow.DeathEnd.AddListener(ResultMatch);
        }

        private void StopMatch()
        {
            _stopMatch.Invoke();
        }

        private void ResultMatch()
        {
            _cleanMatch.Invoke();
        }

        private async void RestartMatch()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5f));
            _startMatch.Invoke();
        }
    }
}