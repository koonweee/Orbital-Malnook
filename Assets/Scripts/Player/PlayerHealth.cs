using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar hpBar;
    public Animator animator;
    public ParticleSystem explosion;
    public int maxHP, invulTime;
    private int hp;
    private bool isInvul;
    // Initialize HP Bar and set invuln to false.
    void Start()
    {
        hpBar.SetMaxHP(maxHP);
        hp = maxHP;
        isInvul = false;
    }

    // Updates HP Bar.
    void Update()
    {
        // FOR TESTING.
        if (Input.GetMouseButtonDown(1)) Damage(20);
        if (Input.GetMouseButtonDown(2)) Heal(10);

        hpBar.UpdateHP(hp);
    }

    // Public method to damage player.
    public void Damage(int amount)
    {
        // Don't take damage during IFrames.
        if (isInvul)
        {
            return;
        }

        // Trigger flashing red animation.
        animator.SetTrigger("Damaged");

        // Decrease health and check death.
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }

        // Trigger invul.
        StartCoroutine(Invul(invulTime));
    }

    // Public method to heal.
    public void Heal(int amount)
    {
        hp += amount;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
    }

    // Timer method for invuln.
    IEnumerator Invul(int time)
    {
        isInvul = true;
        yield return new WaitForSeconds(time);
        isInvul = false;
    }

    // Death method.
    public void Die()
    {
        // Explosion.
        ParticleSystem explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);

        // Destroy HP Bar.
        Destroy(hpBar.gameObject);

        // Hacky way to kill player, turn its renderer off and destroy all its scripts.
        gameObject.GetComponent<Renderer>().enabled = false;

        foreach (MonoBehaviour script in gameObject.GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }
    }
}
