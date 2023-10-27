using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UnitySingleton<UIManager>
{
    public GameUIPanelController gameUIPanel;
    public PauseMenuPanelController pauseMenuPanel;
    public WinPanelController winPanel;
    public TutorialPanelController tutorialPanel;
    public GameObject menuFirstSelection;
    public GameObject tutorailFirtsSelection;
    public GameObject mainsceneFirstSelection;
    public GameObject winPanelFirstSelection;
    // Start is called before the first frame update

    private void Start()
    {
        tutorialPanel.gameObject.SetActive(false);
        for (int i = 0; i< MultiplayerManager.Instance.players.Count; i++)
        {
            gameUIPanel.EnablePointText(i);
        }

    }

    public void TogglePanel(GameObject panel, bool state)
    {
        panel.SetActive(state);
    }

    public void StartPauseMenu()
    {
        if (pauseMenuPanel.gameObject.activeSelf) return;
        pauseMenuPanel.gameObject.SetActive(true);
        GameManager.Instance.TogglePause(true);
    }

    public void StopPauseMenu()
    {
        GameManager.Instance.TogglePause(false);
        pauseMenuPanel.gameObject.SetActive(false);
    }
    public void StartTutroial()
    {
        MultiplayerManager.Instance.SetPlayerEventSystemFirstSelection(tutorailFirtsSelection);
        tutorialPanel.gameObject.SetActive(true);
    }

    public void StopTutroial()
    {
        tutorialPanel.gameObject.SetActive(false);
        MultiplayerManager.Instance.SetPlayerEventSystemFirstSelection(menuFirstSelection);
    }

    public void StartWinPanel()
    {
        PlayerCombatant winner = MultiplayerManager.Instance.players[0].GetComponent<PlayerCombatant>();
        winPanel.SetWinText("P" + winner.id + " Wins!");
        MultiplayerManager.Instance.SetPlayerEventSystemFirstSelection(winPanelFirstSelection);
        winPanel.gameObject.SetActive(true);
        GameManager.Instance.TogglePause(true);
    }
    public void OnDisable()
    {

        MultiplayerManager.Instance.ResetGame();
        Debug.Log("UI Manager is Disabled");
    }
}
