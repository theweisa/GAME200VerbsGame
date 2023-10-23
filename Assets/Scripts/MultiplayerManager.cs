using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MultiplayerManager : UnitySingleton<MultiplayerManager>
{
    public List<GameObject> players = new List<GameObject>();
    public Transform playersParent;
    public LevelManager currentLevelManager;
    int id = 1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableAllPlayerMovement()
    {
        foreach (var player in players)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.ToggleMovement(true);
        }
    }
    public void AddPlayerPrefab(GameObject playerPrefab)
    {

        players.Add(playerPrefab);
        playerPrefab.transform.parent = playersParent;

    }

    public void InitPlayer()
    {

        foreach (var player in players)
        {
            PlayerCombatant playerCombatant = player.GetComponent<PlayerCombatant>();
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.ToggleMovement(true);
            playerCombatant.InitPlayer(id);
            
            id++;
        }
        //PlayerCombatant playerCombatant = Global.FindComponent<PlayerCombatant>(player.gameObject);

        //if (!playerCombatant) return;
        //playerCombatant.InitPlayer(id, player);
        //id++;
    }
    public bool HasEnoughPlayer()
    {
        if (players.Count>=2 ) return true; return false;
    }

}
