using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelController : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {

        UIManager.Instance.pauseMenuPanel.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        UIManager.Instance.pauseMenuPanel.gameObject.SetActive(true);
    }

}
