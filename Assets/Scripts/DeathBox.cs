using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{

    public enum DangerType { BlastZone, Spike };
    public DangerType dangerType = DangerType.BlastZone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll) {
        PlayerCombatant player = Global.FindComponent<PlayerCombatant>(coll.gameObject);
        if (coll.gameObject.tag != "Player" || !player) return;
        StartCoroutine(player.OnDeath());
    }
}
