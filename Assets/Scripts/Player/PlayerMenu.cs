using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public TrailersControl trailers;
    public float force;
    public bool ready;
    private Vector2 initPos;

    void Start()
    {
        ready = false;
        trailers.TrailerOn();
        initPos = transform.position;
        GetComponent<Rigidbody2D>().AddForce(transform.up * force);
    }

    void OnBecameInvisible()
    {
        ready = true;
    }

    public void ResetPos()
    {
        transform.position = initPos;
        ready = false;
    }
}
