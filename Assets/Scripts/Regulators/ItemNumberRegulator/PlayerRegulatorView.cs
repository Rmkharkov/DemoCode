using System;
using System.Collections.Generic;
using Characters;
using Placing;
using UnityEngine;
using UnityEngine.Events;

namespace Regulators.ItemNumberRegulator
{
    public interface IPlayerRegulatorView : IRegulatorView
    {
        event Action<GameplayItemLinks> OnPlayerUpdated;
    }
    public class PlayerRegulatorView : BaseView<PlayerRegulatorModel, PlayerRegulatorController>, IPlayerRegulatorView
    {
        private static PlayerRegulatorView _instance;
        public static IPlayerRegulatorView Instance => _instance;

        [SerializeField] private PlacingView placingView;
        [SerializeField] private PlayerMovingRegulator playerMovingRegulator;
        [SerializeField] private EnemiesMovingRegulator enemiesMovingRegulator;
        
        private readonly List<GameplayItemLinks> _spawned = new List<GameplayItemLinks>();
        public List<GameplayItemLinks> SpawnedItems => _spawned;
        public event Action<GameplayItemLinks> OnPlayerUpdated;
        
        public override void Awake()
        {
            _instance = this;
            base.Awake();
        }

        private void Start()
        {
            GameplayTurnRegulator.Instance.OnStartMatchEvent += SpawnPlayer;
            GameplayTurnRegulator.Instance.OnCleanMatchEvent += RemovePlayer;
        }
        
        private void SpawnPlayer()
        {
            var player = Controller.SpawnPlayer(placingView);
            if (player != null)
            {
                _spawned.Add(player);
                OnPlayerUpdated?.Invoke(player);
            }
        }

        private void RemovePlayer()
        {
            _spawned.ForEach(c => placingView.Remove(c));
            _spawned.Clear();
        }
    }
}