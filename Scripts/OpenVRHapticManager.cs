using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class OpenVRHapticManager : HapticManager
{
    public SteamVR_Action_Vibration steamVRHaptics;


    public override void TriggerHapticEvent(HapticEvent hapticEvent)
    {
        StartCoroutine(TriggerHapticEvent(SteamVR_Input_Sources.LeftHand, hapticEvent));
        StartCoroutine(TriggerHapticEvent(SteamVR_Input_Sources.RightHand, hapticEvent));
    }

    public  IEnumerator TriggerHapticEvent(SteamVR_Input_Sources hand, HapticEvent hapticEvent)
    {
        float startTime = Time.time;
        float currentTime = Time.time;

        while (currentTime - startTime < hapticEvent.curveTime)
        {
            steamVRHaptics.Execute(0, 0, hapticEvent.curveFrequency.Evaluate(currentTime - startTime), hapticEvent.curveAmplitude.Evaluate(currentTime - startTime), hand);
            currentTime = Time.time;
            yield return null; 
        }

        steamVRHaptics.Execute(0, 0, 0, 0, hand);
        yield return null; 
    }

    public override void TriggerHapticPulse(float amplitude, float frequency, float duration)
    {
        StartCoroutine(TriggerHapticPulse(amplitude, frequency, duration, SteamVR_Input_Sources.LeftHand));
        StartCoroutine(TriggerHapticPulse(amplitude, frequency, duration, SteamVR_Input_Sources.RightHand));
    }


    public IEnumerator TriggerHapticPulse(float amplitude, float frequency, float duration, SteamVR_Input_Sources hand)
    {
        float startTime = Time.time;
        float currentTime = Time.time;

        while (currentTime - startTime < duration)
        {
            steamVRHaptics.Execute(0, 0, frequency, amplitude, hand);
            currentTime = Time.time;
            yield return null;
        }

        steamVRHaptics.Execute(0, 0, 0, 0, hand);
        yield return null;
    }

    public override void TriggerContinousHapticPulse(float startAmplitude, float startFrequency)
    {
        ContinousHapticPulseFrequency = startFrequency;
        ContinousHapticPulseAmplitude = startAmplitude;

        StartCoroutine(ContinousHapticPulse(SteamVR_Input_Sources.RightHand));
        StartCoroutine(ContinousHapticPulse(SteamVR_Input_Sources.RightHand));
        runningContinous = true;
    }


    public IEnumerator ContinousHapticPulse(SteamVR_Input_Sources hand)
    {
        while (ContinousHapticPulseActive)
        {
            steamVRHaptics.Execute(0, 0, ContinousHapticPulseFrequency, ContinousHapticPulseAmplitude, hand);
            yield return null; 
        }
        runningContinous = false; 
    }

    public override void StopAllHapticPulses()
    {
        StopAllCoroutines();

        steamVRHaptics.Execute(0, 0, 0, 0, SteamVR_Input_Sources.LeftHand);
        steamVRHaptics.Execute(0, 0, 0, 0, SteamVR_Input_Sources.RightHand);
    }
}
