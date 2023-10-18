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

    public override void OnTriggerEnter2D(Collider2D coll) {
        Rigidbody2D objRb = Global.FindComponent<Rigidbody2D>(coll.gameObject);
        if (!objRb) return;
        BaseDamageable dmgObj = Global.FindComponent<BaseDamageable>(coll.gameObject);
        if (dmgObj && (dmgObj == hostDamageable || contactedDamageables.Contains(dmgObj))) return;
        objRb.AddForce(knockbackForce*direction, ForceMode2D.Impulse);
    }
}
