using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public float timeBeforeDestroy;
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        // Destroy projectile on collision with anything except projectiles.
        if (collider.gameObject.tag != "Projectile")
        {
            Destroy(gameObject);
        }
    }
}
