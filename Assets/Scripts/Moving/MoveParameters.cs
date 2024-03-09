using System;
using UnityEngine;

namespace Moving
{
    [System.Serializable]
    public struct MoveParameters
    {
        [SerializeField] private float moveForce;
        public float MoveForce => moveForce;
        [SerializeField] private float jumpUpForce;
        public float JumpUpForce => moveForce;
        [SerializeField] private float jumpDelayInSec;
        public TimeSpan JumpDelay => TimeSpan.FromSeconds(jumpDelayInSec);
        
        [SerializeField] private EMoveType moveType;
        public EMoveType MoveType => moveType;
    }
}