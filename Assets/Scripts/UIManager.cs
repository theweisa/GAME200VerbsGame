using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UnitySingleton<UIManager>
{
    public SelectPlayerPanelController selectPlayerUIPanel;
    public GameUIPanelController gameUIPanel;
    public bool ShouldCheckPlayerNumbers = true;
    // Start is called before the first frame update


    public void StartLevel()
    {
        if (ShouldCheckPlayerNumbers){
            if (!MultiplayerManager.Instance.HasEnoughPlayer()){
            Debug.Log("Need at least two players");
            return;
            }
        }

        TogglePanel(selectPlayerUIPanel.gameObject, false);
        TogglePanel(gameUIPanel.gameObject, true);
        
    }
    public void TogglePanel(GameObject panel, bool state)
    {
        panel.SetActive(state);
    }
}
