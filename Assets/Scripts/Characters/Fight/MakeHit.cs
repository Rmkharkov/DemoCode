using UnityEngine;

namespace Characters.Fight
{
    public class MakeHit
    {
        public void TryMakeHitOnCollider(Collider hit)
        {
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage();
            }
        }

        public MakeHit() : base()
        {
            
        }
    }
}