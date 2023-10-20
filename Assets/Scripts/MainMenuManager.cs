using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : UnitySingleton<MainMenuManager>
{
    public SelectPlayerPanelController selectPanel;
    public bool ShouldCheckPlayerNumbers = true;
    // Start is called before the first frame update




    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //GameManager.Instance.TogglePause(true);
        SceneManager.LoadScene(1);
    }

}
