using Moving;
using UnityEngine;

namespace Animals
{
    [CreateAssetMenu(fileName = "AnimalConfig", menuName = "Configs/AnimalConfig", order = 0)]
    public class AnimalConfig : ScriptableObject
    {
        [SerializeField] private EAnimalSide animalSide;
        public EAnimalSide AnimalSide => animalSide;
        
        [SerializeField] private EAnimalType animalType;
        public EAnimalType AnimalType => animalType;
    }
}