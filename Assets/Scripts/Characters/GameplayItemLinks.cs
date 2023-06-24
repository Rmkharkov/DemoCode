using Characters.Death;
using Characters.Fight;
using Characters.Moving;
using Placing;
using UnityEngine;

namespace Characters
{
    public class GameplayItemLinks
    {
        public EGameItem ItemType;
        public Transform ItemTransform;
        public IMovable Moving;
        public IMovableData MovingData;
        public IDeathFlow DeathFlow;
    }
}