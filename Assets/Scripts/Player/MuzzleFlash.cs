using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MuzzleFlash : MonoBehaviour
{
    public void Flash(float brightness, float duration)
    {
        StartCoroutine(FlashFor(brightness, duration));
    }

    IEnumerator FlashFor(float brightness, float duration)
    {
        gameObject.GetComponent<Light2D>().intensity = brightness;
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<Light2D>().intensity = 0;
    }
}
