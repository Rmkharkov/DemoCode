using System;
using System.Collections.Generic;
using Animals;
using UnityEngine;

namespace Spawn
{
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Configs/SpawnConfig", order = 0)]
    public class SpawnConfig : ScriptableObject
    {
        private static SpawnConfig _instance;

        public static SpawnConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<SpawnConfig>("SpawnConfig");
                }
                return _instance;
            }
        }

        [SerializeField] private float minDistanceToSpawnFromOtherObstacles;
        public float MinDistanceToSpawnFromOtherObstacles => minDistanceToSpawnFromOtherObstacles;
        
        [SerializeField] private Vector2 extremeCoordsForSpawn;
        public Vector2 ExtremeCoordsForSpawn => extremeCoordsForSpawn;

        [SerializeField] private Vector2 spawnDelays;
        public Vector2 SpawnDelays => spawnDelays;
        public TimeSpan RandomSpawnDelay => TimeSpan.FromSeconds(UnityEngine.Random.Range(spawnDelays.x, spawnDelays.y));
        
        [SerializeField] private List<SpawnParameters> spawnParameters = new List<SpawnParameters>();

        public SpawnParameters GetParametersByType(EAnimalType type)
        {
            return spawnParameters.Find(c => c.AnimalConfig.AnimalType == type);
        }
    }
}