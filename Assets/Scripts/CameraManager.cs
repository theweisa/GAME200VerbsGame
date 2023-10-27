using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : UnitySingleton<CameraManager>
{
    public CinemachineVirtualCamera currentCamera;
    public CinemachineTargetGroup targetGroup;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[4];
    }
    public void AddGroupTarget(Transform newTarget, float weight=2, float radius=13) {
        CinemachineTargetGroup.Target target;
        target.target = newTarget;
        target.weight = weight;
        target.radius = radius;
        for (int i = 0; i < targetGroup.m_Targets.Length; i++) {
            if (targetGroup.m_Targets[i].target == null) {
                targetGroup.m_Targets.SetValue(target, i);
                return;
            }
        }
    }
        
    public void StartShake(float str=1f, float dur=1f, float freq=1f, bool perma=false) {
        CameraShake shake = currentCamera.GetComponent<CameraShake>();
        if (shake) {
            shake.StartShake(str, dur, freq, perma);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
