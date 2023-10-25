using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MultiplayerManager : UnitySingleton<MultiplayerManager>
{
    public List<GameObject> players = new List<GameObject>();
    public Transform playersParent;
    public LevelManager currentLevelManager;
    [SerializeField] List<LevelManager> availableLevels = new List<LevelManager>();
    int id = 1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentLevelManager = null;
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

    public void InitStartLevelPrefab(Transform levelParent)
    {
        Instantiate(currentLevelManager.gameObject, levelParent);
    }
    public bool HasEnoughPlayer()
    {
        if (players.Count>=2 ) return true; return false;
    }

    public void SetStartLevel(int index)
    {
        currentLevelManager = availableLevels[index];
    }

    public void SetPlayerEventSystemFirstSelection(GameObject obj)
    {
        foreach (var player in players)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.eventSystem.SetSelectedGameObject(null);
            playerController.eventSystem.SetSelectedGameObject(obj);
        }
    }
}
