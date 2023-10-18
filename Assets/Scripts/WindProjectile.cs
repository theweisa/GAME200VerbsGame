using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProjectile : BaseDamageSource
{
    protected override void Start() {
        base.Start();
        destroyOnContact = false;
    }
    public override void ApplyDamage(BaseDamageable damageable)
    {
        if (contactedDamageables.Contains(damageable)) return;
        contactedDamageables.Add(damageable);
        if (!damageable.rb) return;
        damageable.rb.AddForce(knockbackForce*direction, ForceMode2D.Impulse);
    }
}
