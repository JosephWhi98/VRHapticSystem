using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleImplementation : MonoBehaviour
{
    public HapticEvent longFadeIn;
    public HapticEvent longFadeInOut;
    public HapticEvent lowRumble;


    private void Start()
    {
        StartCoroutine(HorrorEventExample());
    }


    IEnumerator HorrorEventExample()
    {
        yield return new WaitForSeconds(5f);

        HapticManager.instance.TriggerHapticEvent(longFadeIn);

        yield return new WaitForSeconds(longFadeIn.curveTime + 3f);

        HapticManager.instance.TriggerHapticEvent(longFadeInOut);

        yield return new WaitForSeconds(longFadeInOut.curveTime + 3f);

    
        HapticManager.instance.TriggerHapticEvent(lowRumble);

        yield return null; 
    }
}
