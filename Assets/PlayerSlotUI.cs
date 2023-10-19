using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerSlotUI : MonoBehaviour
{
    public GameObject playerSprite;
    public GameObject buttonSprite;
    public GameObject backdrop;
    public TextMeshProUGUI playerID;

    Image _playerSprite;
    Image _buttonSprite;
    Image _backdrop;

    Color defaultColor = Color.black;
    Color[] colorList = { Color.blue, Color.green, Color.gray, Color.red };
    // Start is called before the first frame update
    void Start()
    {

        _playerSprite = playerSprite.GetComponent<Image>();
        _buttonSprite = buttonSprite.GetComponent<Image>();
        _backdrop = backdrop.GetComponent<Image>(); 

        _playerSprite.color = defaultColor;
    }

    public void SetPlayerSpriteColor(int id)
    {
        if (!_playerSprite) return;
        _playerSprite.color = colorList[id];
    }

    public void SetButtonSprite(Sprite sprite)
    {
        if (!_buttonSprite) return;
        _buttonSprite.sprite = sprite;
    }
    
    public void SetPlayerIDText(int id)
    {
        if (!playerID.gameObject.activeSelf)
        {
            playerID.gameObject.SetActive(true);
        }
        playerID.text = "P" + id.ToString();
    }
}
