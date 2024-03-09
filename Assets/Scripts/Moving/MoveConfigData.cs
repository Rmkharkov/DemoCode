using Animals;
using UnityEngine;

namespace Moving
{
    [System.Serializable]
    public struct MoveConfigData
    {
        [SerializeField] private EAnimalType animalType;
        public EAnimalType AnimalType => animalType;
        [SerializeField] private MoveConfig moveConfig;
        public MoveConfig MoveConfig => moveConfig;
    }
}