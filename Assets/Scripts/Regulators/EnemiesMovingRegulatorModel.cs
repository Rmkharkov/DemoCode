using Characters.Moving;
using UnityEngine;

namespace Regulators
{
    [System.Serializable]
    public class EnemiesMovingRegulatorModel : BaseModel
    {
        [HideInInspector] public IMovable[] EnemiesMoves;
        [HideInInspector] public IMovable Player;
        [HideInInspector] public bool CanMove;
    }
}