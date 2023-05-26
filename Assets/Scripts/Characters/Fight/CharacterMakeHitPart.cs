using UnityEngine;

namespace Characters.Fight
{
    public class CharacterMakeHitPart : BaseView<CharacterMakeHitModel, CharacterMakeHitController>
    {
        private void OnTriggerEnter(Collider hit)
        {
            IMakeHit.TryMakeHitOnCollider(hit);
        }
    }
}