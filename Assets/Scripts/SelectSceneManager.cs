using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectSceneManager : UnitySingleton<SelectSceneManager>
{

    public SelectPlayerPanelController selectPlayerUIPanel;
    public SelectLevelPanelController selectLevelUIPanel;

    public void StartSelectLevel()
    {
        selectLevelUIPanel.gameObject.SetActive(true);
        selectPlayerUIPanel.gameObject.SetActive(false);
    }
}
