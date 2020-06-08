using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileCollision : MonoBehaviour
{
    public float timeBeforeDestroy;
    public int damage;
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }

        // Destroy projectile on collision with anything except projectiles;
        if (collision.gameObject.tag == "Projectile")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else {
            Destroy(gameObject);
        }

    }
}
