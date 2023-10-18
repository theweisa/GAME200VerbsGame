using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageSource : MonoBehaviour
{
    public List<BaseDamageable> contactedDamageables = new List<BaseDamageable>();
    public Rigidbody2D rb;
    public float damage;
    public float knockbackForce;
    public float projectileSpeed;
    public float lifetime;
    private float lifetimeTimer;
    public float velocityCap = 100f;
    public bool destroyOnContact = false;
    public Vector2 direction;
    public BaseDamageable hostDamageable;

    protected virtual void Awake() {
        rb = rb ? rb : Global.FindComponent<Rigidbody2D>(gameObject);
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        lifetimeTimer = lifetime;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateVelocity();
        UpdateLifetime();
    }

    public virtual void InitDamageSource(BaseDamageable damageable, Vector2 dir) {
        hostDamageable = damageable;
        direction = dir;
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

    public void UpdateVelocity() {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, velocityCap);
    }

    public virtual IEnumerator OnDeath() {
        yield return null;
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D coll) {
        BaseDamageable obj = Global.FindComponent<BaseDamageable>(coll.gameObject);
        if (!obj) return;
        ApplyDamage(obj);
    }
}
