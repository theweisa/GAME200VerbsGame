using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelSlotUI : MonoBehaviour,ISubmitHandler,IPointerClickHandler
{
    public TextMeshProUGUI levelText;
    public GameObject stageSprite;
    public GameObject backdrop;
    public int slotIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("On click");
        SetSlotIndex();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log("On submit");
        SetSlotIndex();
    }

    public void SetSlotIndex()
    {
        SelectSceneManager.Instance.selectLevelUIPanel.SetCurrentSelectedSlot(this);
    }
}
