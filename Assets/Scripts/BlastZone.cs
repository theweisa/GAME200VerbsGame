using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("lmao");
        PlayerCombatant player = Global.FindComponent<PlayerCombatant>(coll.gameObject);
        if (coll.gameObject.tag != "Player" || !player) return;
        StartCoroutine(player.OnDeath());
        player.transform.position = new Vector3(0,0,0);
        player.rb.velocity = Vector3.zero;
    }
}
