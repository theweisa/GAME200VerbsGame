using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePause(bool shouldPause)
    {
        Time.timeScale = shouldPause ? 1.0f : 0.0f;
    }
}
