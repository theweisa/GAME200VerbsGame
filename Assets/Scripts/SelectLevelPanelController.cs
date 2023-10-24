using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelPanelController : MonoBehaviour
{
    public List<LevelSlotUI> levelSlots = new List<LevelSlotUI>();
    public Transform levelSlotsParent;


    private LevelSlotUI currentSelectedSlot;
    // Start is called before the first frame update

    public void SetCurrentSelectedSlot(LevelSlotUI slot)
    {
        currentSelectedSlot = slot;
        Debug.Log("Current Slot: " + currentSelectedSlot.name + ",index: " + currentSelectedSlot.slotIndex);
    }
    public LevelSlotUI GetCurrentSelectedSlot()
    {
        return currentSelectedSlot;
    }
    public void InitStartLevel()
    {
       
        MultiplayerManager.Instance.SetStartLevel(currentSelectedSlot.slotIndex);
    }

}
