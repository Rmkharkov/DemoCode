using UnityEngine;

namespace Moving
{
    [System.Serializable]
    public class MoveItemModel : BaseModel
    {
        [SerializeField] private Rigidbody body;
        public Rigidbody Body => body;
    }
}