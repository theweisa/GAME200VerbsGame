using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : UnitySingleton<GameManager>
{
    //public bool ShouldCheckPlayerNumbers = true;
    public Transform instanceManager;
    public PlayerManager playerManager;
    public Transform levelParent;
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
        Time.timeScale = shouldPause ? 0.0f : 1.0f;
    }

    public void StartGame()
    {
        //GameManager.Instance.TogglePause(true);
        SceneManager.LoadScene(1);

    }

    public void StartLevel()
    {
        SelectSceneManager.Instance.selectLevelUIPanel.InitStartLevel();
        if (MultiplayerManager.Instance.currentLevelManager == null)
        {
            Debug.Log("Didn't select level");
            return;
        }
        SceneManager.LoadScene(2);
        MultiplayerManager.Instance.InitPlayer();
        

    }

}
