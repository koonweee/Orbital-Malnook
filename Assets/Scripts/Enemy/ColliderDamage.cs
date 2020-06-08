using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    public StateController controller;
    public bool collided;
    public int damage;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<StateController>();
        collided = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collided = true;
            controller.Damage(damage);
        }
    }
}
