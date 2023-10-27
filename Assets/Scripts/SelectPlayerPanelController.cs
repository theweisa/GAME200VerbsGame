using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SelectPlayerPanelController : MonoBehaviour
{
    public List<PlayerSlotUI> playerSlots = new List<PlayerSlotUI>();
    public Transform playerSlotsParent;

    // Start is called before the first frame update

    public void ActivateSlot(int index)
    {
        PlayerSlotUI slotUI = playerSlots[index-1];
        slotUI.SetPlayerSpriteColor(index-1);
        slotUI.SetPlayerIDText(index);
    }
}
