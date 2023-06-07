using Characters.Moving;
using UnityEngine;

namespace Regulators
{
    [System.Serializable]
    public class PlayerMovingRegulatorModel : BaseModel
    {
        [HideInInspector] public IMovable PlayerMove;
        [HideInInspector] public bool CanMove;
    }
}