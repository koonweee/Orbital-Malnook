using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour
{
    public float timeBeforeDestroy;
    public int damage;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage player.
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<StateController>().Damage(damage);
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
