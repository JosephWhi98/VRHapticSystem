using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HapticEvent : ScriptableObject
{

    public AnimationCurve curveFrequency;
    public AnimationCurve curveAmplitude;
    public float curveTime;

    private void Start()
    {
        curveTime = curveFrequency.keys[curveFrequency.length - 1].time;
    }
}
