using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject bars;
    public GameObject hpBarObj;
    private Bar hpBar;
    public Animator animator;
    public ParticleSystem explosion;
    public GameObject sprite;
    public TrailersControl trailers;
    public AudioSource playerAudio;
    public AudioClip playerHurt, playerDeath;
    public float knockbackForce;
    public int maxHP, invulTime;
    public SceneLoader scene;
    public int hp;
    public PlayerSaveLoad loader;
    private bool isInvul, isDead;
    
    // Initialize HP Bar and set invuln to false.
    void Start()
    {
        hpBar = hpBarObj.GetComponent<Bar>();
        hpBar.SetMax(maxHP);
        hp = maxHP;
        isInvul = false;
        isDead = false;

        loader.LoadPlayer();
    }

    // Updates HP Bar.
    void Update()
    {
        hpBar.UpdateVal(hp);
    }

    public void IncreaseMaxHP(int hp)
    {
        maxHP += hp;
        hpBar.SetMax(maxHP);
    }

    // Public method to damage player.
    public void Damage(int amount)
    {
        // Don't take damage during IFrames.
        if (isInvul || isDead)
        {
            return;
        }

        // Trigger flashing red animation.
        animator.SetTrigger("Damaged");

        // Sound.
        playerAudio.clip = playerHurt;
        playerAudio.Play();

        // Decrease health and check death.
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }

        // Trigger knockback.
        KnockBack();

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

    // Heal to full.
    public void MaxHeal()
    {
        hp = maxHP;
    }

    // Knockback
    void KnockBack()
    {
        Vector2 direction = Random.insideUnitCircle;
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce);
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

        // Sound.
        playerAudio.clip = playerDeath;
        playerAudio.Play();

        // Turn off trailers.
        trailers.TrailerOff();

        // Hacky way to kill player, turn its renderer off and destroy all its scripts.
        sprite.GetComponent<Renderer>().enabled = false;

        foreach (MonoBehaviour script in gameObject.GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }

        // Destroy bars.
        bars.SetActive(false);

        // Set death flag.
        isDead = true;

        // Go to menu.
        scene.LoadScene("Menu");
    }
}
