using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(AudioController.FadeIn(GetComponent<AudioSource>(), 1f));
    }
}
