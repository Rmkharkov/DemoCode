using UnityEngine;

namespace Characters.Fight
{
    public class CharacterMakeHitPart : BaseView<CharacterMakeHitModel, CharacterMakeHitController>
    {
        private readonly MakeHit _makeHitUse = new MakeHit();

        private void OnTriggerEnter(Collider hit)
        {
            _makeHitUse.TryMakeHitOnCollider(hit);
        }
    }
}