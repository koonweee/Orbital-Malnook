using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public GameObject bullet;
    public Transform weapon, firePoint, firePoint2;
    public float speed, distanceBeforeAttack, bulletForce, shootDelay;
    public EnemyBehaviour behaviour;
    public bool dualShooter;
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

        if (dualShooter)
        {
            GameObject bulletObj2 = Instantiate(bullet, firePoint2.position, Quaternion.identity);
            Rigidbody2D bulletRB2 = bulletObj2.GetComponent<Rigidbody2D>();
            bulletRB2.AddForce(firePoint.up * bulletForce);
        }
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
