using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Moving;
using UnityEngine;

namespace Regulators
{
    [System.Serializable]
    public class EnemiesMovingRegulatorModel : BaseModel
    {
        [HideInInspector] public GameplayItemLinks[] Enemies;
        [HideInInspector] public IMovableData Player;
        [HideInInspector] public bool CanMove;
    }
}