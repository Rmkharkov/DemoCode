using Characters.Fight;
using UnityEngine;

namespace Mines
{
    public class MineMakeHitPart : BaseView<MineMakeHitModel, MineMakeHitController>
    {
        private void OnTriggerEnter(Collider hit)
        {
            IMakeHit.TryMakeHitOnCollider(hit);
        }
    }
}