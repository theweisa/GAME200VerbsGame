using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.Users;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    public TextMeshProUGUI text;
    public PlayerInput currentPlayer;
    public GameObject eventSystem;
    public GameObject playerMenuFirstSelection;
    public GameObject LevelMenuFirstSelection;
    int id = 1;
    // Start is called before the first frame update
    void Start()
    {
        //DisableEventSystemInput();
        //playerInputManager.DisableJoining();
        //InputAction joinAction = SelectSceneManager.Instance.eventSystem.gameObject.GetComponent<PlayerInput>().actions.FindAction("Join");
        //joinAction.performed += JoinPlayer;
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

    public void JoinPlayer(InputAction.CallbackContext context)
    {
        Debug.Log("player joined");
        InputDevice[] inputDevices = {Keyboard.current,Mouse.current};
        
        //var player = PlayerInput.Instantiate(playerPrefab, id, controlScheme: "Mouse&Keyboard", -1, inputDevices);
        //p
        //print(playerInput.currentControlScheme);
        //playerInputManager.JoinPlayer(id,);

    }
    public void AfterPlayerJoined(PlayerInput player)
    {
        // player.transform.parent.gameObject.transform.SetParent(MultiplayerManager.Instance.playersParent);
        //print(player.currentControlScheme);
        if (!player.gameObject.CompareTag("Player")) return;
        player.GetComponent<EventSystem>().SetSelectedGameObject(playerMenuFirstSelection);
        MultiplayerManager.Instance.AddPlayerPrefab(player.transform.parent.gameObject);
        SelectSceneManager.Instance.selectPlayerUIPanel.ActivateSlot(id);
        id++;
    }

}
