using UnityEngine;

namespace Moving
{
    [CreateAssetMenu(fileName = "MoveConfig", menuName = "Configs/MoveConfig", order = 0)]
    public class MoveConfig : ScriptableObject
    {
        [SerializeField] private MoveParameters moveParameters;
        public MoveParameters MoveParameters => moveParameters;
    }
}