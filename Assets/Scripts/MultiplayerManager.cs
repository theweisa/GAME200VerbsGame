using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : UnitySingleton<MultiplayerManager>
{
    public List<GameObject> players = new List<GameObject>();
    public Transform playersParent;
    // Start is called before the first frame update
    void Start()
    {
        
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

    public bool HasEnoughPlayer()
    {
        if (players.Count>=2 ) return true; return false;
    }
}
