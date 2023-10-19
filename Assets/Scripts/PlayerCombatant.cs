using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatant : BaseDamageable
{
    public int playerNumber;
    public PlayerCombatant lastPlayerHitBy;
    public int points;
    public PlayerController controller;
    // Start is called before the first frame update
    protected override void Awake()
    {
        points = 0;
        base.Awake();
        controller = controller ? controller : Global.FindComponent<PlayerController>(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator OnDeath()
    {
        Debug.Log("player die");
        yield return base.OnDeath();
        if (lastPlayerHitBy)
        {
            lastPlayerHitBy.points++;
            lastPlayerHitBy = null;
            Debug.Log($"Player {lastPlayerHitBy.playerNumber} points: {lastPlayerHitBy.points}");
        }
    }
}
