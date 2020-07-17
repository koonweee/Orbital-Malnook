using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileCollision : MonoBehaviour
{
    public float timeBeforeDestroy;
    public int damage;
    public float duration;
    public float slowAmount;
    public string type;
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
            Damage(collision.gameObject);
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

    void Damage(GameObject enemy)
    {
        enemy.GetComponent<EnemyHealth>().Damage(damage);

        if (type == "dot")
        {
            enemy.GetComponent<EnemyState>().DOT(duration, damage);
        }
        else if (type == "ice")
        {
            enemy.GetComponent<EnemyState>().Slow(duration, slowAmount);
        }
        else if (type == "lightning")
        {
            enemy.GetComponent<EnemyState>().Stun(duration);
        }
    }
}
