using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameUIPanelController : MonoBehaviour
{
    public List<TextMeshProUGUI> playerPointTexts = new List<TextMeshProUGUI>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPointText(int point, int id)
    {
        playerPointTexts[id-1].text = "P" + id.ToString() + ": " + point.ToString();
    }

    public void EnablePointText(int index)
    {
        playerPointTexts[index].gameObject.SetActive(true);
    }
}
