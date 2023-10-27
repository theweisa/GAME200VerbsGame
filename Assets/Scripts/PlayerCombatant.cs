using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerCombatant : BaseDamageable
{
    public Transform playerSpawnsRef;
    public GameObject playerID;
    public int id;
    public PlayerCombatant lastPlayerHitBy;
    public List<GameObject> playerLivesUI = new List<GameObject>();
    public int deafultLives = 3;
    public int remainingLives;
    public int totalPoints = 0;
    public int kos = 0;
    public int falls = 0;
    public PlayerController controller;
    public Vector3 spawnLocation;
    // Start is called before the first frame update
    protected override void Awake()
    {
        //enabled = false;
        // needs to be changed to happen only when in level scene
        //if (!(playerSpawnsRef = GameObject.Find("LevelManager").transform.Find("PlayerSpawns")))
        //{
        //    Debug.Log("ERROR: Could not find PlayerSpawns");
        //}

        //points = 0;
        //base.Awake();
        //controller = controller ? controller : Global.FindComponent<PlayerController>(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        remainingLives = deafultLives;
    }
    // Update is called once per frame
    protected override void Update()
    {

    }

    public void InitPlayer(int newId/*, PlayerInput input*/) {

        // needs to be changed to happen only when in level scene

        id = newId;
        //if (!(playerSpawnsRef = GameObject.Find("LevelManager").transform.Find("PlayerSpawns")))
        //{
        //    Debug.Log("ERROR: Could not find PlayerSpawns");
        //}


        //MultiplayerManager.Instance.AddPlayerPrefab(gameObject);
        //UIManager.Instance.selectPlayerUIPanel.ActivateSlot(id);

        //controller.input = input;
        TextMeshPro playerIDText = playerID.GetComponent<TextMeshPro>();
        playerIDText.text = "P" + id.ToString();
        Debug.Log($"Player {id} Joined: {controller.input.currentControlScheme}");
        playerSpawnsRef = MultiplayerManager.Instance.currentLevelManager.playerSpawners[id-1];
        if (!playerSpawnsRef)
        {
            Debug.Log("ERROR: Could not find PlayerSpawns");
            return;
        }
        spawnLocation = playerSpawnsRef.position;
        //spawnLocation = playerSpawnsRef.Find($"Player{id}Spawn").position;
        SpawnPlayer();
    }
    public int GetTotalPoints()
    {
         totalPoints = kos - falls;
        return totalPoints;
    }
    public void SpawnPlayer() {
        if (remainingLives <= 0)
        {
            gameObject.SetActive(false);
            MultiplayerManager.Instance.players.Remove(gameObject);
            OnGameOver();

            return;
        }
       
        
        transform.position = spawnLocation;
        rb.velocity = Vector3.zero;
        controller.windMeter.ResetMeter();
    }

    public override IEnumerator OnDeath()
    {
        Debug.Log("player die");
        remainingLives--;
        playerLivesUI[remainingLives].SetActive(false);
        falls++;
        yield return base.OnDeath();
        if (lastPlayerHitBy)
        {
            lastPlayerHitBy.kos++;
            //lastPlayerHitBy.points++;
           UIManager.Instance.gameUIPanel.SetPointText(lastPlayerHitBy.GetTotalPoints(), lastPlayerHitBy.id);
            Debug.Log($"Player {id} is hit by player {lastPlayerHitBy.id}");
            lastPlayerHitBy = null;
        }
        UIManager.Instance.gameUIPanel.SetPointText(GetTotalPoints(), id);
        SpawnPlayer();
    }

    public void OnGameOver()
    {
        Debug.Log($"Player {id} is Game Over");
        GameManager.Instance.CheckGameState();
        return;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        lastPlayerHitBy = collision.gameObject.GetComponent<PlayerCombatant>();
        
    }
}
