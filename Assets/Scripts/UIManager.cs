using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UnitySingleton<UIManager>
{
    public GameUIPanelController gameUIPanel;
    //public bool ShouldCheckPlayerNumbers = true;
    // Start is called before the first frame update

    private void Start()
    {
        for (int i = 0; i< MultiplayerManager.Instance.players.Count; i++)
        {
            gameUIPanel.EnablePointText(i);
        }

    }
    //public void StartLevel()
    //{
    //    if (ShouldCheckPlayerNumbers){
    //        if (!MultiplayerManager.Instance.HasEnoughPlayer()){
    //        Debug.Log("Need at least two players");
    //        return;
    //        }
    //    }
    //    MultiplayerManager.Instance.EnableAllPlayerMovement();
    //    TogglePanel(selectPlayerUIPanel.gameObject, false);
    //    TogglePanel(gameUIPanel.gameObject, true);
        
    //}
    public void TogglePanel(GameObject panel, bool state)
    {
        panel.SetActive(state);
    }
}
