using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;


public class OculusHapticManager : HapticManager
{
    public override void TriggerHapticEvent(HapticEvent hapticEvent)
    {
        StartCoroutine(TriggerHapticEvent(OVRInput.Controller.LTouch, hapticEvent));
        StartCoroutine(TriggerHapticEvent(OVRInput.Controller.RTouch, hapticEvent));
    }

    public IEnumerator TriggerHapticEvent(OVRInput.Controller hand, HapticEvent hapticEvent)
    {
        float startTime = Time.time;
        float currentTime = Time.time;

        while (currentTime - startTime < hapticEvent.curveTime)
        {
            OVRInput.SetControllerVibration(hapticEvent.curveFrequency.Evaluate(currentTime - startTime), hapticEvent.curveAmplitude.Evaluate(currentTime - startTime), hand);
            currentTime = Time.time;
            yield return null;
        }

        OVRInput.SetControllerVibration(0, 0, hand);
        yield return null;
    }

    public override void TriggerHapticPulse(float amplitude, float frequency, float duration)
    {
        StartCoroutine(TriggerHapticPulse(amplitude, frequency, duration, OVRInput.Controller.LTouch));
        StartCoroutine(TriggerHapticPulse(amplitude, frequency, duration, OVRInput.Controller.RTouch));
    }

    public IEnumerator TriggerHapticPulse(float amplitude, float frequency, float duration, OVRInput.Controller hand)
    {
        float startTime = Time.time;
        float currentTime = Time.time;

        while (currentTime - startTime < duration)
        {
            OVRInput.SetControllerVibration(amplitude, frequency, hand);
            currentTime = Time.time;
            yield return null;
        }

        OVRInput.SetControllerVibration(0, 0, hand);
        yield return null;
    }

    public override void TriggerContinousHapticPulse(float startAmplitude, float startFrequency)
    {
        ContinousHapticPulseFrequency = startFrequency;
        ContinousHapticPulseAmplitude = startAmplitude;

        StartCoroutine(ContinousHapticPulse(OVRInput.Controller.LTouch));
        StartCoroutine(ContinousHapticPulse(OVRInput.Controller.RTouch));
        runningContinous = true;
    }


    public IEnumerator ContinousHapticPulse(OVRInput.Controller hand)
    {
        while (ContinousHapticPulseActive)
        {
            OVRInput.SetControllerVibration(ContinousHapticPulseAmplitude, ContinousHapticPulseFrequency, hand);
            yield return null;
        }
        runningContinous = false;
    }

    public override void StopAllHapticPulses()
    {
        StopAllCoroutines();
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
