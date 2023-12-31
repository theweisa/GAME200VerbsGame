using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelPanelController : MonoBehaviour
{
    public List<LevelSlotUI> levelSlots = new List<LevelSlotUI>();
    public Transform levelSlotsParent;
    public GameObject UIFirstSelection;

    private LevelSlotUI currentSelectedSlot;
    // Start is called before the first frame update

    public void SetCurrentSelectedSlot(LevelSlotUI slot)
    {
        currentSelectedSlot = slot;
        Debug.Log("Current Slot: " + currentSelectedSlot.name + ",index: " + currentSelectedSlot.slotIndex);
        //InitStartLevel();
        //GameManager.Instance.StartLevel();
    }
    public LevelSlotUI GetCurrentSelectedSlot()
    {
        return currentSelectedSlot;
    }
    public void InitStartLevel()
    {
       if(currentSelectedSlot == null)
        {
            return;
        }
        MultiplayerManager.Instance.SetStartLevel(currentSelectedSlot.slotIndex);
    }

}
