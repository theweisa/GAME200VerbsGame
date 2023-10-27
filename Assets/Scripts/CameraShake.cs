using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0f;
    public float baseDuration = 1f;
    public float strength = 1f;
    public bool permaShake = false;
    public bool shaking = false;
    public float frequency = 1f;
    [HideInInspector]
    CinemachineBasicMultiChannelPerlin cameraShake;

    public void Awake() {
        //cameraShake = CameraController.Instance.currCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        duration = 0f;
    }
    public void StartShake(float str=1f, float dur=1f, float freq=1f, bool perma=false) {
        cameraShake = CameraManager.Instance.currentCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (cameraShake == null) return;
        if (duration > 0) {
            return;
        }
        else {
            strength = str;
            baseDuration = dur;
            duration = dur;
        }
        permaShake = perma;
        if (permaShake) {
            baseDuration = 1f;
            duration = 1f;
        }
        cameraShake.m_AmplitudeGain = strength;
        cameraShake.m_FrequencyGain = frequency;
    }
    public void Update() {
        if (duration > 0) {
            duration = Mathf.Max(permaShake ? baseDuration : duration - Time.unscaledDeltaTime, 0f);
            //Debug.Log("duration: "+duration);
            //cameraShake.m_AmplitudeGain = Mathf.Lerp(strength, 0f, duration/baseDuration);
            cameraShake.m_AmplitudeGain = strength * duration/baseDuration;
        }
        
    }
    public void StopShake(float dur=0.75f) {
        permaShake = false;
        duration = Mathf.Max(dur, Time.deltaTime);
        baseDuration = duration;
    }
    public bool Shaking() {
        return duration > 0;
    }
}