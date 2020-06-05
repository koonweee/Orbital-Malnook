using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet; // The projectile to fire.
    public Transform firePoint; // Point where bullet is spawned.
    public Animator animator;
    public MuzzleFlash flash;
    public Button shoot;
    public float force, recoilForce; // Bullet's force and recoil.

    void Start()
    {
        shoot.onClick.AddListener(Shoot);
    }

    void Update()
    {
        // Shoot on left click.
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Muzzle flash.
        flash.Flash(5, 0.05f);

        GameObject spawnedBullet = Instantiate(bullet, firePoint.position, Quaternion.identity); // Spawn bullet.
        Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>(); // Get bullet's RB.
        rb.AddForce(transform.up * force);

        // Recoil.
        gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * recoilForce * Time.deltaTime);
    }
}
