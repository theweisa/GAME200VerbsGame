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
        AudioManager.Instance.Play("title");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorial()
    {
        AudioManager.Instance.Play("select", 1f, 0.1f);
        tutorialPanel.SetActive(true);
    }

    public void EndToutorial()
    {
        AudioManager.Instance.Play("select", 1f, 0.1f);
        tutorialPanel.SetActive(false );
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedButton);
    }
}
