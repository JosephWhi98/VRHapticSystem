using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;


public class HapticManager : MonoBehaviour
{
    public static HapticManager instance;

    [Range(0, 1)] public float ContinousHapticPulseFrequency = 0;
    [Range(0, 1)] public float ContinousHapticPulseAmplitude = 0;
    public bool ContinousHapticPulseActive;
    [HideInInspector] public bool runningContinous;


    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            instance = null;
    }

    private void Update()
    {
        if (ContinousHapticPulseActive && !runningContinous)
            TriggerContinousHapticPulse(0, 0);
    }

    public virtual void TriggerHapticEvent(HapticEvent hapticEvent){}
    
    public virtual void TriggerHapticPulse(float amplitude, float frequency, float duration){}

    public virtual void TriggerContinousHapticPulse(float startAmplitude, float startFrequency){}

    public virtual void StopAllHapticPulses(){}

}
