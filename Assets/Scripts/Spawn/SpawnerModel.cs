using UnityEngine;

namespace Spawn
{
    [System.Serializable]
    public class SpawnerModel : BaseModel
    {
        [SerializeField] private Transform animalsLiveParent;
        public Transform AnimalsLiveParent => animalsLiveParent;
        
        [SerializeField] private Transform animalsPoolParent;
        public Transform AnimalsPoolParent => animalsPoolParent;
    }
}