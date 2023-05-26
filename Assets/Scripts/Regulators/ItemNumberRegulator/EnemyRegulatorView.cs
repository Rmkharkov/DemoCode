using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Moving;
using Placing;
using Unity.VisualScripting;
using UnityEngine;

namespace Regulators.ItemNumberRegulator
{
    public interface IEnemyRegulatorView : IRegulatorView
    {
    }
    public class EnemyRegulatorView : BaseView<EnemyRegulatorModel, EnemyRegulatorController>, IEnemyRegulatorView
    {
        private static EnemyRegulatorView _instance;
        public static IEnemyRegulatorView Instance => _instance;
        
        [SerializeField] private PlacingView placingView;
        [SerializeField] private EnemiesMovingRegulator enemiesMovingRegulator;
        
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
            GameplayTurnRegulator.Instance.CleanMatchEvent.AddListener(RemoveEnemies);
        }

        private void Update()
        {
            TrySpawnNextEnemy();
            TrySpawnNextMine();
        }
        
        private void TrySpawnNextEnemy()
        {
            var enemy = Controller.TrySpawnNextEnemy(
                _spawnedEnemies.Count(c => c.ItemType == EGameItem.Enemy), placingView);
            if (enemy != null)
            {
                _spawnedEnemies.Add(enemy);
                enemiesMovingRegulator.UpdateEnemiesMoves(EnemiesMoves);
            }
        }

        private void TrySpawnNextMine()
        {
            var mine = Controller.TrySpawnNextMine(
                _spawnedEnemies.Count(c => c.ItemType == EGameItem.Mine), placingView);
            if (mine != null)
            {
                _spawnedEnemies.Add(mine);
                enemiesMovingRegulator.UpdateEnemiesMoves(EnemiesMoves);
            }
        }

        private void RemoveEnemies()
        {
            _spawnedEnemies.ForEach(c => placingView.Remove(c));
            _spawnedEnemies.Clear();
        }

        private IMovable[] EnemiesMoves
        {
            get
            {
                var toReturn = new List<IMovable>();
                _spawnedEnemies.ForEach(c => toReturn.Add(c.Moving));
                return toReturn.ToArray();
            }
        }
    }
}