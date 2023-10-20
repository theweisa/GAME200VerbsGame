using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatant : BaseDamageable
{
    public int id;
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
    protected override void Update()
    {

    }

    public void InitPlayer(int newId, PlayerInput input) {
        id = newId;
        MultiplayerManager.Instance.AddPlayerPrefab(gameObject);
        UIManager.Instance.selectPlayerUIPanel.ActivateSlot(id);
        controller.input = input;
        CameraManager.Instance.AddGroupTarget(transform, 2, 13);
        Debug.Log($"Player {id} Joined: {input.currentControlScheme}");
    }

    public void SpawnPlayer() {
        gameObject.SetActive(true);
    }
    public override IEnumerator OnDeath()
    {
        Debug.Log("player die");
        yield return base.OnDeath();
        transform.position = new Vector3(0,0,0);
        rb.velocity = Vector3.zero;
        controller.windMeter.ResetMeter();
        if (lastPlayerHitBy)
        {
            lastPlayerHitBy.points++;
            Debug.Log($"Player {lastPlayerHitBy.id} points: {lastPlayerHitBy.points}");
            lastPlayerHitBy = null;
        }
            
    }
}
