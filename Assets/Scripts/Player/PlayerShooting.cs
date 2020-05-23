using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet; // The projectile to fire.
    public Transform firePoint; // Point where bullet is spawned.
    public Animator animator;
    public float force; // Bullet's force.

    void Update()
    {
        // Shoot on left click.
        if (Input.GetMouseButtonDown(0))
        {
            GameObject spawnedBullet = Instantiate(bullet, firePoint.position, Quaternion.identity); // Spawn bullet.
            Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>(); // Get bullet's RB.
            rb.AddForce(transform.up * force);

            // Change state for animation.
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }
}
