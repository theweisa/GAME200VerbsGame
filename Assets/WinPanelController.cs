using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WinPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public void SetWinText(string text)
    {
        this.text.SetText(text);
    }
}
