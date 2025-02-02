﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject hpBarObj;
    public GameObject parentObj;
    private Bar hpBar;
    public Animator animator;
    public ParticleSystem explosion;
    public EnemyExp exp;
    public MobSpawner spawner;
    public AudioSource enemyAudio;
    public AudioClip enemyHurt;
    public int maxHP;
    public bool damaged;
    private int hp;
    private bool isDead;
    void Start()
    {
        isDead = false;
        hpBar = hpBarObj.GetComponent<Bar>();
        hpBar.SetMax(maxHP);
        hp = maxHP;
        damaged = false;
        animator = GetComponent<Animator>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<MobSpawner>();
        enemyAudio = GetComponent<AudioSource>();
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
        // Trigger flashing red animation.
        animator.SetTrigger("Damaged");

        // Decrease health and check death.
        hp -= amount;
        if (hp <= 0)
        {
            Die();
            return;
        }

        damaged = true;

        // Sound.
        if (gameObject != null) 
        {
            enemyAudio.clip = enemyHurt;
            enemyAudio.Play();
        }
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

    // Death method.
    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // Report death to spawner.
        spawner.DecrementMobCount();

        // Give exp.
        exp.GiveExp();

        // Explosion.
        ParticleSystem explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);

        // Destroy object.
        Destroy(parentObj);
    }
}
