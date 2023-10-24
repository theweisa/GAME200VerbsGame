using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UnitySingleton<UIManager>
{
    public GameUIPanelController gameUIPanel;
    public PauseMenuPanelController pauseMenuPanel;
    public GameObject tutorialPanel;
    // Start is called before the first frame update

    private void Start()
    {
        tutorialPanel.SetActive(false);
        for (int i = 0; i< MultiplayerManager.Instance.players.Count; i++)
        {
            gameUIPanel.EnablePointText(i);
        }

    }

    public void TogglePanel(GameObject panel, bool state)
    {
        panel.SetActive(state);
    }

    public void StartTutroial()
    {
        tutorialPanel.SetActive(true);
    }

    public void StopTutroial()
    {
        tutorialPanel.SetActive(false);
    }
}
