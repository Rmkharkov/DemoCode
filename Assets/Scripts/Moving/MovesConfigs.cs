using System.Collections.Generic;
using Animals;
using UnityEngine;

namespace Moving
{
    [CreateAssetMenu(fileName = "MovesConfigs", menuName = "Configs/MovesConfigs", order = 0)]
    public class MovesConfigs : ScriptableObject
    {
        private static MovesConfigs _instance;

        public static MovesConfigs Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<MovesConfigs>("MovesConfigs");
                }

                return _instance;
            }
        }

        [SerializeField] private List<MoveConfigData> moveConfigs = new List<MoveConfigData>();

        public MoveConfig GetConfigByAnimal(EAnimalType animalType)
        {
            return moveConfigs.Find(c => c.AnimalType == animalType).MoveConfig;
        }
    }
}