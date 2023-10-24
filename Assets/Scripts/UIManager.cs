using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UnitySingleton<UIManager>
{
    public GameUIPanelController gameUIPanel;

    // Start is called before the first frame update

    private void Start()
    {
        for (int i = 0; i< MultiplayerManager.Instance.players.Count; i++)
        {
            gameUIPanel.EnablePointText(i);
        }

    }

    public void TogglePanel(GameObject panel, bool state)
    {
        panel.SetActive(state);
    }


}
