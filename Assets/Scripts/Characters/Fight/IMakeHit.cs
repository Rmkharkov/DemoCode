using UnityEngine;

namespace Characters.Fight
{
    public interface IMakeHit
    {
        public static void TryMakeHitOnCollider(Collider hit)
        {
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage();
            }
        }
    }
}