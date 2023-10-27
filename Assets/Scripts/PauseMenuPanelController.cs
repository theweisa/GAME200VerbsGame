using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPanelController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        MultiplayerManager.Instance.SetPlayerEventSystemFirstSelection(UIManager.Instance.mainsceneFirstSelection);
    }
    public void ExitPauseMenu()
    {
        GameManager.Instance.TogglePause(false);
        gameObject.SetActive(false);
    }
}
