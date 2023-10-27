using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerCombatant : BaseDamageable
{
    public List<AudioManager.AudioObject> audioObjects = new List<AudioManager.AudioObject>();
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

    public void InitPlayer(int newId) {

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
        controller.dead = false;
        controller.sprite.gameObject.SetActive(true);
        controller.fanAnim.gameObject.SetActive(true);
        Global.Appear(controller.fanAnim.GetComponent<SpriteRenderer>(), 0.2f);
        Global.Appear(controller.sprite, 0.2f);
        if (remainingLives <= 0)
        {
            gameObject.SetActive(false);
            MultiplayerManager.Instance.players.Remove(gameObject);
            OnGameOver();

            return;
        }
       
        rb.constraints = controller.constraints;
        transform.position = spawnLocation;
        controller.windMeter.ResetMeter();
    }

    public override IEnumerator OnDeath()
    {
        if (!controller.dead) {
            Debug.Log("player die");
            controller.dead = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            controller.playerAnimator.Play("playerDeath");
            remainingLives--;
            playerLivesUI[remainingLives].SetActive(false);
            falls++;
            Play("death", 1f, 0.1f);
            yield return base.OnDeath();
            if (lastPlayerHitBy)
            {
                lastPlayerHitBy.kos++;
                //lastPlayerHitBy.points++;
            UIManager.Instance.gameUIPanel.SetPointText(lastPlayerHitBy.GetTotalPoints(), lastPlayerHitBy.id);
                Debug.Log($"Player {id} is hit by player {lastPlayerHitBy.id}");
                lastPlayerHitBy = null;
            }
            CameraManager.Instance.StartShake(10, 1f, 10);
            UIManager.Instance.gameUIPanel.SetPointText(GetTotalPoints(), id);
            Global.Fade(controller.fanAnim.GetComponent<SpriteRenderer>(), 0.15f);
            yield return new WaitForSeconds(0.3f);
            controller.sprite.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
            SpawnPlayer();   
        }
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
    public void Play(string key, float pitch=1, float pitchOffset=0f) {
        foreach (AudioManager.AudioObject obj in audioObjects) {
            if (obj.id == key) {
                obj.Play(pitch, pitchOffset);
                return;
            }
        }
    }
}
