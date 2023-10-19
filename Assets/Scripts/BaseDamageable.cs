using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageable : MonoBehaviour
{
    public Rigidbody2D rb;
    float hp = 1;
    protected virtual void Awake() {
        rb = rb ? rb : Global.FindComponent<Rigidbody2D>(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Damage(BaseDamageSource source) {
        hp -= source.damage;
        if (hp > 0) return;
        StartCoroutine(OnDeath());
    }

    public virtual IEnumerator OnDeath() {
        yield return null;
    }
}
