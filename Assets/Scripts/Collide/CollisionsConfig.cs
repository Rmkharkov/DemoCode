using UnityEngine;

namespace Collide
{
    [CreateAssetMenu(fileName = "CollisionsConfig", menuName = "Configs/CollisionsConfig", order = 0)]
    public class CollisionsConfig : ScriptableObject
    {
        private static CollisionsConfig _instance;

        public static CollisionsConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<CollisionsConfig>("CollisionsConfig");
                }
                return _instance;
            }
        }

        [SerializeField] private float minReflectDistance;
        public float MinReflectDistance => minReflectDistance;
    }
}