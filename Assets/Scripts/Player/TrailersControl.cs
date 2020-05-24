using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailersControl : MonoBehaviour
{
    public ParticleSystem leftTrailer, rightTrailer; // Left and right trailers' particles.
    private bool isOn; // Whether trailers are on.
    void Start()
    {
        // Initialize trailers to off.
        isOn = false;
        leftTrailer.Stop();
        rightTrailer.Stop();
    }

    void Update()
    {
        // Moving -> On, vice versa.
        if (gameObject.GetComponent<PlayerMovement>().isMoving)
        {
            TrailerOn();
        }
        else
        {
            TrailerOff();
        }
    }

    public void TrailerOn()
    {
        if (isOn) return;
        isOn = true;
        leftTrailer.Play();
        rightTrailer.Play();
    }

    public void TrailerOff()
    {
        if (!isOn) return;
        isOn = false;
        leftTrailer.Stop();
        rightTrailer.Stop();
    }
}
