using System.Collections.Generic;
using System.Linq;
using Characters;
using Placing;
using UnityEngine;

namespace Regulators.ItemNumberRegulator
{
    public class EnemyRegulatorView : BaseView<EnemyRegulatorModel, EnemyRegulatorController>, IEnemyRegulatorView
    {
        private static EnemyRegulatorView _instance;
        public static IEnemyRegulatorView Instance => _instance;
        
        [SerializeField] private PlacingView placingView;
        [SerializeField] private EnemiesMovingRegulator enemiesMovingRegulator;
        private IPlacingView PlacingView => placingView;
        private IEnemiesMoveRegulator EnemiesMovingRegulator => enemiesMovingRegulator;
        
        private readonly List<GameplayItemLinks> _spawnedEnemies = new List<GameplayItemLinks>();
        public List<GameplayItemLinks> SpawnedItems => _spawnedEnemies;

        public override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        private void Start()
        {            
            Controller.SubscribeSpawnAccess();
            GameplayTurnRegulator.Instance.OnCleanMatchEvent += RemoveEnemies;
        }

        private void Update()
        {
            TrySpawnNextEnemy();
            TrySpawnNextMine();
        }
        
        private void TrySpawnNextEnemy()
        {
            var enemy = Controller.TrySpawnNextEnemy(
                _spawnedEnemies.Count(c => c.ItemType == EGameItem.Enemy), PlacingView);
            if (enemy != null)
            {
                _spawnedEnemies.Add(enemy);
                EnemiesMovingRegulator.UpdateEnemiesMoves(Enemies);
            }
        }

        private void TrySpawnNextMine()
        {
            var mine = Controller.TrySpawnNextMine(
                _spawnedEnemies.Count(c => c.ItemType == EGameItem.Mine), PlacingView);
            if (mine != null)
            {
                _spawnedEnemies.Add(mine);
                EnemiesMovingRegulator.UpdateEnemiesMoves(Enemies);
            }
        }

        private void RemoveEnemies()
        {
            _spawnedEnemies.ForEach(c => PlacingView.Remove(c));
            _spawnedEnemies.Clear();
        }

        private GameplayItemLinks[] Enemies
        {
            get
            {
                var toReturn = new List<GameplayItemLinks>();
                _spawnedEnemies.ForEach(c => toReturn.Add(c));
                return toReturn.ToArray();
            }
        }
    }
}