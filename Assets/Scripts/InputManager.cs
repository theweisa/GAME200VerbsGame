using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.Users;

public class InputManager : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    public TextMeshProUGUI text;
    int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        //playerInputManager.DisableJoining();
        
    }

    private void Awake()
    {

        //foreach (var item in InputSystem.devices)
        //{
        //    if (item is Mouse)
        //    {
        //        InputSystem.DisableDevice(item);
        //    }
        //    Debug.Log(item.description);
        //    Debug.Log(item.enabled);
        //}

        //text.text = "Controllers: " + InputSystem.devices.Count.ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void AfterPlayerJoined(PlayerInput player)
    {
        GameObject playerPrefab = player.gameObject.transform.parent.gameObject;
        MultiplayerManager.Instance.AddPlayerPrefab(playerPrefab);
        SelectPlayerPanelController selectPlayerPanelController = UIManager.Instance.selectPlayerUIPanel;
        selectPlayerPanelController.ActivateSlot(id);
        id++;
    }
}
