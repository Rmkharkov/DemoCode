using UnityEngine;

namespace Collide
{
    [System.Serializable]
    public class CollisionParserModel : BaseModel
    {
        [SerializeField] private Rigidbody body;
        public Rigidbody Body => body;
    }
}