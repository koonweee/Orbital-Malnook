using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public GameObject bullet;
    public Transform weapon, firePoint;
    public float speed, distanceBeforeAttack, bulletForce, shootDelay;
    public EnemyBehaviour behaviour;
    private float shootTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shootTimer = shootDelay;
    }

    void Update()
    {
        if (behaviour.hostile)
        {
            Look();

            // Is it close enough to shoot?
            if (Vector2.Distance(player.position, transform.position) <= distanceBeforeAttack)
            {
                if (shootTimer <= 0)
                {
                    Shoot();
                    shootTimer = shootDelay;
                }
            }
            else
            {
                Follow();
            }
        }
    }

    void FixedUpdate()
    {
        shootTimer -= 0.1f;
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRB = bulletObj.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(firePoint.up * bulletForce);
    }

    void Follow()
    {
        // Get vector direction towards player.
        Vector2 dir = player.position - transform.position;

        // Apply force in that direction.
        rb.AddForce(dir.normalized * speed * Time.deltaTime);
    }

    void Look()
    {
        // Get vector direction towards player.
        Vector2 dir = player.position - transform.position;

        // Rotate weapon.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0, 0, angle);
    }
}
