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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePause(bool shouldPause)
    {
        Time.timeScale = shouldPause ? 1.0f : 0.0f;
    }

    public void StartGame()
    {
        //GameManager.Instance.TogglePause(true);
        SceneManager.LoadScene(1);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(2);
        MultiplayerManager.Instance.InitPlayer();
    }
}
