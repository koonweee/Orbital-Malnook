using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public EnemyHealth health;
    public EnemyFollow follow;
    public ParticleSystem particle;
    private bool slowed, dotted, stunned;

    void Start()
    {
        health = GetComponent<EnemyHealth>();
        follow = GetComponent<EnemyFollow>();
        slowed = false;
        dotted = false;
    }

    public void DOT(float duration, int damage)
    {
        if (dotted) return;
        StartCoroutine(DOTCoroutine(duration, damage));
    }

    public void Slow(float duration, float percent)
    {
        if (slowed) return;
        StartCoroutine(SlowCoroutine(duration, percent));
    }

    public void Stun(float duration)
    {
        if (stunned) return;
        StartCoroutine(StunCoroutine(duration));
    }

    IEnumerator SlowCoroutine(float duration, float percent)
    {
        slowed = true;

        // Particle effect
        var main = particle.main;
        main.startColor = new ParticleSystem.MinMaxGradient(Color.blue, Color.cyan);
        particle.Play();

        float initSpeed = follow.speed;
        

        follow.speed *= percent;
        yield return new WaitForSeconds(duration);
        follow.speed = initSpeed;

        slowed = false;

        particle.Stop();
    }

    IEnumerator DOTCoroutine(float duration, int damage)
    {
        dotted = true;

        // Particle effect
        var main = particle.main;
        main.startColor = new ParticleSystem.MinMaxGradient(Color.red, Color.yellow);
        particle.Play();

        while (duration > 0)
        {
            health.Damage(damage);
            --duration;
            yield return new WaitForSeconds(1f);
        }

        dotted = false;

        particle.Stop();
    }

    IEnumerator StunCoroutine(float duration)
    {
        stunned = true;

        // Particle effect
        var main = particle.main;
        main.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.yellow);
        particle.Play();
        float initSpeed = follow.speed;
        follow.speed = 0f;
        while (duration > 0)
        {            
            --duration;
            yield return new WaitForSeconds(1f);
        }
        follow.speed = initSpeed;

        stunned = false;

        particle.Stop();
    }
}
