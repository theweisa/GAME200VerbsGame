using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatant : BaseDamageable
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator OnDeath()
    {
        Debug.Log("player die");
        yield return base.OnDeath();
    }
}
