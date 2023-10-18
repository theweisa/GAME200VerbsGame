using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageSource : MonoBehaviour
{
    public List<BaseDamageable> contactedDamageables = new List<BaseDamageable>();
    public float damage;
    public float knockbackForce;
    public float lifetime;
    private float lifetimeTimer;
    public bool destroyOnContact = false;
    public Vector2 direction;
    public BaseDamageable hostDamageable;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        lifetimeTimer = lifetime;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateLifetime();
    }

    public virtual void InitDamageSource(BaseDamageable damageable) {
        hostDamageable = damageable;
    }

    public virtual void ApplyDamage(BaseDamageable damageable) {
        if (contactedDamageables.Contains(damageable)) return;
        contactedDamageables.Add(damageable);
        Rigidbody2D dRb = Global.FindComponent<Rigidbody2D>(damageable.gameObject);
        if (dRb) {
            dRb.AddForce(direction*knockbackForce, ForceMode2D.Impulse);
        }
        damageable.Damage(this);
        if (destroyOnContact) {
            StartCoroutine(OnDeath());
        }
    }

    public void UpdateLifetime() {
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer > 0f) return;
        StartCoroutine(OnDeath());
    }

    public virtual IEnumerator OnDeath() {
        yield return null;
        Destroy(gameObject);
    }
}
