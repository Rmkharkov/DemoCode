using System;
using Characters.Fight;
using UnityEngine;

namespace Mines
{
    public class MineMakeHitPart : BaseView<MineMakeHitModel, MineMakeHitController>
    {
        private readonly MakeHit _makeHitUse = new MakeHit();

        private void OnTriggerEnter(Collider hit)
        {
            _makeHitUse.TryMakeHitOnCollider(hit);
        }
    }
}