using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class InputManager : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        foreach (var item in InputSystem.devices)
        {
            if (item is Mouse)
            {
                InputSystem.DisableDevice(item);
            }
            Debug.Log(item.description);
            Debug.Log(item.enabled);
        }

        //text.text = "Controllers: " + InputSystem.devices.Count.ToString();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
