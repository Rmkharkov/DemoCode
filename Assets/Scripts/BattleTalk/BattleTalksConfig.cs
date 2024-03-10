using UnityEngine;

namespace BattleTalk
{
    [CreateAssetMenu(fileName = "BattleTalksConfig", menuName = "Configs/BattleTalksConfig", order = 0)]
    public class BattleTalksConfig : ScriptableObject
    {
        private static BattleTalksConfig _instance;

        public static BattleTalksConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<BattleTalksConfig>("BattleTalksConfig");
                }
                return _instance;
            }
        }

        [SerializeField] private float talkLiveTime;
        public float TalkLiveTime => talkLiveTime;
    }
}