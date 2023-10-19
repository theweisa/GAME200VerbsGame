using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProjectile : BaseDamageSource
{
    public float minScale = 1f;
    public float maxScale = 2f;
    public float minBlowForce = 15f;
    public float maxBlowForce = 30f;
    protected override void Start() {
        base.Start();
        destroyOnContact = false;
    }
    public override void InitDamageSource(BaseDamageable damageable, Vector2 dir)
    {
        base.InitDamageSource(damageable, dir);
        LeanTween.value(gameObject, (float val) => {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, val);
        }, rb.velocity.magnitude, 0, lifetime).setEaseInQuart();
    }

    public void InitBlowProjectile(float chargeRatio) {
        knockbackForce = minBlowForce+(maxBlowForce-minBlowForce)*chargeRatio;
        minScale *= 1 + chargeRatio;
        maxScale *= 1 + chargeRatio;
        LeanTween.value(gameObject, (float val) => {
            transform.localScale = new Vector2(val, val);
        }, minScale, maxScale, lifetime).setEaseOutQuart();
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
