using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMeter : MonoBehaviour
{
    public Transform windBar;
    public Transform backBar;
    public float totalMeter = 100;
    [HideInInspector] public float currentMeter = 100;
    public float meterRegenTime = 1.5f;
    private float meterRegenTimer;
    public float meterRegenPerSecond = 30f;
    // Start is called before the first frame update
    void Start()
    {
        currentMeter = totalMeter;
    }

    // Update is called once per frame
    void Update()
    {
        if (meterRegenTimer > 0) {
            meterRegenTimer -= Time.deltaTime;
        }
        else if (currentMeter < totalMeter) {
            currentMeter = Mathf.Min(currentMeter + Time.deltaTime * meterRegenPerSecond, totalMeter);
            windBar.localScale = new Vector2(currentMeter / totalMeter, windBar.localScale.y);
        }
    }

    public float GetCurrentMeter() {
        return currentMeter;
    }

    public IEnumerator DepleteMeter(float amt) {
        currentMeter = Mathf.Clamp(currentMeter-amt, 0, totalMeter);
        meterRegenTimer = meterRegenTime;
        float meterRatio = currentMeter / totalMeter;
        float prevScale = windBar.localScale.x;
        backBar.localScale = windBar.localScale;
        LeanTween.value(windBar.gameObject, (float val) => {
            windBar.localScale = new Vector2(val, windBar.localScale.y);
        }, prevScale, meterRatio, 0.3f).setEaseOutExpo();
        yield return new WaitForSeconds(0.5f);
        LeanTween.value(windBar.gameObject, (float val) => {
            backBar.localScale = new Vector2(val, backBar.localScale.y);
        }, prevScale, meterRatio, 0.3f).setEaseOutExpo();
    }

    public void ResetMeter() {
        LeanTween.value(windBar.gameObject, (float val) => {
            windBar.localScale = new Vector2(val, windBar.localScale.y);
        }, windBar.localScale.x, 1, 0.3f).setEaseOutExpo();
        backBar.localScale = new Vector2(1, backBar.localScale.y);
    }
}
