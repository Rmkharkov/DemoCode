using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Placing
{
    [CreateAssetMenu(fileName = "GameplayPrefabs", menuName = "Configs/GameplayPrefabs", order = 0)]
    public class GameplayPrefabs : ScriptableObject
    {
        private static GameplayPrefabs _instance;

        public static GameplayPrefabs Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<GameplayPrefabs>("GameplayPrefabs");
                }

                return _instance;
            }
        }

        [SerializeField] private List<GameItemData> gameItems = new List<GameItemData>();

        public GameObject GetItemPrefab(EGameItem itemType)
        {
            return gameItems.First(c => c.itemType == itemType).prefab;
        }
    }
}