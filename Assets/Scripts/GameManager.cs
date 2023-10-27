using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : UnitySingleton<GameManager>
{
    //public bool ShouldCheckPlayerNumbers = true;
    public Transform instanceManager;
    public Transform levelParent;
    public bool isGameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        MultiplayerManager.Instance.InitStartLevelPrefab(levelParent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePause(bool shouldPause)
    {
        AudioManager.Instance.Play("select", 1f, 0.1f);
        Time.timeScale = shouldPause ? 0.0f : 1.0f;
    }

    public void StartGame()
    {
        AudioManager.Instance.Play("select", 1f, 0.1f);
        TogglePause(false);
        //GameManager.Instance.TogglePause(true);
        isGameEnd = false;
        SceneManager.LoadScene(1);

    }

    public void StartLevel()
    {
        AudioManager.Instance.Play("select", 1f, 0.1f);
        SelectSceneManager.Instance.selectLevelUIPanel.InitStartLevel();
        if (MultiplayerManager.Instance.currentLevelManager == null)
        {
            Debug.Log("Didn't select level");
            return;
        }
        SceneManager.LoadScene(2);
        MultiplayerManager.Instance.InitPlayer();
        

    }

    public void ExitLevel()
    {

        TogglePause(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        AudioManager.Instance.Play("select", 1f, 0.1f);
        Application.Quit();
    }

    public void CheckGameState()
    {
        if (MultiplayerManager.Instance.players.Count > 1) return;
        AudioManager.Instance.Stop("game", true);
        Debug.Log(MultiplayerManager.Instance.players[0].name + "wins");
        isGameEnd = true;
        UIManager.Instance.StartWinPanel();
    }
}
