using UnityEngine;

namespace BattleTalk
{
    [System.Serializable]
    public class BattleTalkManagerModel : BaseModel
    {
        [SerializeField] private Transform battleTalksLiveParent;
        public Transform BattleTalksLiveParent => battleTalksLiveParent;
        [SerializeField] private Transform battleTalksPoolParent;
        public Transform BattleTalksPoolParent => battleTalksPoolParent;
    }
}