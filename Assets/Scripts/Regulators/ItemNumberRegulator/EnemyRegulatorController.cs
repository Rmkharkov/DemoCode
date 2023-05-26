using System.Collections.Generic;
using System.Linq;
using Characters;
using Placing;
using UnityEngine;

namespace Regulators.ItemNumberRegulator
{
    public class EnemyRegulatorController : BaseController<EnemyRegulatorModel>
    {
        private float _lastEnemySpawnTime;
        private float _lastMineSpawnTime;
        private bool _canSpawn;
        public GameplayItemLinks TrySpawnNextEnemy(int currentCount, IPlacingView placingView)
        {
            if (!_canSpawn) return null;
            
            var goodPassedTime = _lastEnemySpawnTime 
                                 < Time.time - 
                                 Model.EnemiesSpawnDelayInSec;
            
            if (currentCount < Model.MaxEnemiesNumber && goodPassedTime)
            {
                _lastEnemySpawnTime = Time.time;
                var setPosition = RandomEnemyPosition;
                if (setPosition != Vector3.positiveInfinity)
                    return placingView.Place(EGameItem.Enemy, setPosition);
            }

            return null;
        }
        
        public GameplayItemLinks TrySpawnNextMine(int currentCount, IPlacingView placingView)
        {
            if (!_canSpawn) return null;
            
            var goodPassedTime = _lastMineSpawnTime < Time.time - Model.MinesSpawnDelayInSec;
            
            if (currentCount < Model.MaxMinesNumber && goodPassedTime)
            {
                _lastMineSpawnTime = Time.time;
                var setPosition = RandomEnemyPosition;
                if (setPosition != Vector3.positiveInfinity)
                    return placingView.Place(EGameItem.Mine, setPosition);
            }

            return null;
        }

        public void SubscribeSpawnAccess()
        {
            GameplayTurnRegulator.Instance.StartMatchEvent.AddListener(EnableSpawn);
            GameplayTurnRegulator.Instance.StopMatchEvent.AddListener(DisableSpawn);
        }
        
        private void EnableSpawn()
        {
            _canSpawn = true;
            _lastEnemySpawnTime = Time.time;
            _lastMineSpawnTime = Time.time;
        }
        
        private void DisableSpawn()
        {
            _canSpawn = false;
        }
        
        private Vector3 RandomEnemyPosition
        {
            get
            {
                var totalItems = new List<GameplayItemLinks>();
                totalItems.AddRange(EnemyRegulatorView.Instance.SpawnedItems);
                totalItems.AddRange(PlayerRegulatorView.Instance.SpawnedItems);

                var toReturn = Vector3.zero;
                var maxDistX = GameplayConfig.Instance.maxScreenDistanceX;
                var maxDistZ = GameplayConfig.Instance.maxScreenDistanceZ;
                var breakCounter = 10000;
                while (!GoodDistanceFromAll(toReturn, totalItems) && breakCounter > 0)
                {
                    toReturn = new Vector3(
                        UnityEngine.Random.Range(maxDistX.x, maxDistX.y), 
                        0f,
                        UnityEngine.Random.Range(maxDistZ.x, maxDistZ.y));
                    breakCounter--;
                }
                return breakCounter > 0 ? toReturn : Vector3.positiveInfinity;
            }
        }

        private bool GoodDistanceFromAll(Vector3 position, List<GameplayItemLinks> items)
        {
            foreach (var item in items)
            {
                var distance = Vector3.Distance(position, item.ItemTransform.localPosition);
                if (distance < Model.MinimalSpawnDistance)
                {
                    return false;
                }
            }
            return true;
        }
    }
}