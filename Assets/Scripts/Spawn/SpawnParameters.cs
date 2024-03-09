using Animals;
using Moving;
using UnityEngine;

namespace Spawn
{
    [System.Serializable]
    public struct SpawnParameters
    {
        [SerializeField] private AnimalConfig animalConfig;
        public AnimalConfig AnimalConfig => animalConfig;
    }
}