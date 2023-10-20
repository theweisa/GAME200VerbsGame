using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatant : BaseDamageable
{
    public Transform playerSpawnsRef;
    public int id;
    public PlayerCombatant lastPlayerHitBy;
    public int points;
    public PlayerController controller;
    public Vector3 spawnLocation;
    // Start is called before the first frame update
    protected override void Awake()
    {
        // needs to be changed to happen only when in level scene
        if (!(playerSpawnsRef = GameObject.Find("LevelManager").transform.Find("PlayerSpawns")))
        {
            Debug.Log("ERROR: Could not find PlayerSpawns");
        }

        points = 0;
        base.Awake();
        controller = controller ? controller : Global.FindComponent<PlayerController>(gameObject);
    }
    

    // Update is called once per frame
    protected override void Update()
    {

    }

    public void InitPlayer(int newId, PlayerInput input) {

        // needs to be changed to happen only when in level scene
        if (!(playerSpawnsRef = GameObject.Find("LevelManager").transform.Find("PlayerSpawns")))
        {
            Debug.Log("ERROR: Could not find PlayerSpawns");
        }

        id = newId;
        MultiplayerManager.Instance.AddPlayerPrefab(gameObject);
        UIManager.Instance.selectPlayerUIPanel.ActivateSlot(id);
        controller.input = input;
        CameraManager.Instance.AddGroupTarget(transform, 2, 13);
        Debug.Log($"Player {id} Joined: {input.currentControlScheme}");
        
        spawnLocation = playerSpawnsRef.Find($"Player{id}Spawn").position;
        SpawnPlayer();
    }

    public void SpawnPlayer() {
        transform.position = spawnLocation;
        rb.velocity = Vector3.zero;
        controller.windMeter.ResetMeter();
    }

    public override IEnumerator OnDeath()
    {
        Debug.Log("player die");
        yield return base.OnDeath();
        if (lastPlayerHitBy)
        {
            lastPlayerHitBy.points++;
            Debug.Log($"Player {lastPlayerHitBy.id} points: {lastPlayerHitBy.points}");
            lastPlayerHitBy = null;
        }
        SpawnPlayer();
    }
}
