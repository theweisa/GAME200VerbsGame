using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SelectSceneManager : UnitySingleton<SelectSceneManager>
{

    public SelectPlayerPanelController selectPlayerUIPanel;
    public SelectLevelPanelController selectLevelUIPanel;
    [SerializeField] bool shouldCheckPlayerNumber;
    public InputManager inputManager;

    public void StartSelectLevel()
    {
        if (shouldCheckPlayerNumber)
        {
            if (!MultiplayerManager.Instance.HasEnoughPlayer())
            {
                Debug.Log("Need at least two players");
                return;
            }
        }
        selectPlayerUIPanel.gameObject.SetActive(false);
        MultiplayerManager.Instance.SetPlayerEventSystemFirstSelection(inputManager.LevelMenuFirstSelection);
        selectLevelUIPanel.gameObject.SetActive(true);


    }



}
