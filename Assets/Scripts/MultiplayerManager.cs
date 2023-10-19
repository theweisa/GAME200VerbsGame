using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : UnitySingleton<MultiplayerManager>
{
    public List<GameObject> playerPrefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void  AddPlayerPrefab(GameObject playerPrefab)
    {
        playerPrefabs.Add(playerPrefab);
    }

    public bool HasEnoughPlayer()
    {
        if (playerPrefabs.Count>=2 ) return true; return false;
    }
}
