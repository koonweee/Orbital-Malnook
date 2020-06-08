using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public bool hostile;
    public ColliderDamage coll;
    public EnemyHealth hp;
    public PlayerStealth stealth;
    public Animator animator;

    void Start()
    {
        hostile = false;
        stealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStealth>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (coll.collided || hp.damaged)
        {
            hostile = true;
            animator.SetBool("Hostile", true);
        }

        if (stealth.isStealth)
        {
            hostile = false;
            animator.SetBool("Hostile", false);
        }
    }

}
