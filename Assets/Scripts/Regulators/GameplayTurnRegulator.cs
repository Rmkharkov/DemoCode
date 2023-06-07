using System;
using System.Threading.Tasks;
using Characters;
using Regulators.ItemNumberRegulator;
using UI.ResultScreen;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Regulators
{
    public interface IGameplayTurnRegulator
    {
        event Action OnStartMatchEvent;
        event Action OnCleanMatchEvent;
        event Action OnStopMatchEvent;
    }
    public class GameplayTurnRegulator : BaseView<GameplayTurnRegulatorModel, GameplayTurnRegulatorController>, IGameplayTurnRegulator
    {
        public static IGameplayTurnRegulator Instance => _instance;
        private static GameplayTurnRegulator _instance;

        public event Action  OnStartMatchEvent;
        public event Action  OnCleanMatchEvent;
        public event Action  OnStopMatchEvent ;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            PlayerRegulatorView.Instance.OnPlayerUpdated += SubscribeOnPlayerDeathEvent;
            UIResultScreenView.Instance.OnResultPanelClosed += RestartMatch;

            RestartMatch();
        }

        private void SubscribeOnPlayerDeathEvent(GameplayItemLinks player)
        {
            player.DeathFlow.OnDeathStart += StopMatch;
            player.DeathFlow.OnDeathEnd += ResultMatch;
        }

        private void StopMatch()
        {
            OnStopMatchEvent?.Invoke();
        }

        private void ResultMatch()
        {
            OnCleanMatchEvent?.Invoke();
        }

        private async void RestartMatch()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5f));
            OnStartMatchEvent?.Invoke();
        }
    }
}