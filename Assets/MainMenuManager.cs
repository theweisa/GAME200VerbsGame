using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MainMenuManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject eventSystem;
    public GameObject firstSelectedButton;
    // Start is called before the first frame update
    void Start()
    {
        tutorialPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void EndToutorial()
    {
        tutorialPanel.SetActive(false );
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedButton);
    }
}
