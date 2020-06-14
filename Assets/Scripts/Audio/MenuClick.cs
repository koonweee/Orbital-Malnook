using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClick : MonoBehaviour
{
    public AudioClip click;
    public AudioSource source;

    public void PlayClick()
    {
        source.clip = click;
        source.Play();
    }
}
