using UnityEngine;

namespace Placing
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig", order = 0)]
    public class GameplayConfig : ScriptableObject
    {
        private static GameplayConfig _instance;

        public static GameplayConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<GameplayConfig>("GameplayConfig");
                }

                return _instance;
            }
        }

        public Vector2 maxScreenDistanceX;
        public Vector2 maxScreenDistanceZ;
    }
}