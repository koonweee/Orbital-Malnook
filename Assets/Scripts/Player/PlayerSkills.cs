using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    private Skill[] skills;
    private Skill skillA, skillB;
    public Button skillAButton, skillBButton;
    public GameObject shotgunBullet;
    public PlayerShooting shooter;
    public PlayerDash dashEffect;
    public int shotgunSpread, spreadAngle, shotgunForce;
    public float blinkSpeed;
    public AudioClip blinkSound;

    void Start()
    {
        // Init skills pool here.
        skills = new Skill[]{new Shotgun(shotgunBullet, shotgunSpread, spreadAngle, shooter, shotgunForce),
                             new Blink(gameObject, blinkSpeed, dashEffect, blinkSound)};

        // FOR TESTING, DEFAULT SKILLS.
        LockInSkill('A', 0);
        LockInSkill('B', 1);

        // Skills joystick buttons.
        skillAButton.onClick.AddListener(() => skillA.Activate());
        skillBButton.onClick.AddListener(() => skillB.Activate());
    }

    public void LockInSkill(char skill, int choice)
    {
        if (skill == 'A') skillA = skills[choice];
        if (skill == 'B') skillB = skills[choice];
    }

    void Update()
    {
        // FOR TESTING, REPLACE WITH JOYSTICK BUTTONS.
        //if (Input.GetMouseButtonDown(1)) skillA.Activate();
        //if (Input.GetKeyDown(KeyCode.Space)) skillB.Activate();
    }
    
}

interface Skill
{
    void Activate();
}

class Shotgun : Skill
{
    private GameObject shotgunBullet;
    private int spread, spreadAngle, shotgunForce;
    private PlayerShooting shooter;

    public Shotgun(GameObject shotgunBullet, int spread, int spreadAngle, PlayerShooting shooter, int shotgunForce)
    {
        this.shotgunBullet = shotgunBullet;
        this.spread = spread;
        this.spreadAngle = spreadAngle;
        this.shooter = shooter;
        this.shotgunForce = shotgunForce;
    }
    public void Activate()
    {
        float interval = (float) spreadAngle / (spread - 1);
        float relativeAngle = spreadAngle / 2f;
        for (int bulletNum = 0; bulletNum < spread; ++bulletNum)
        {
            shooter.Shoot(shotgunBullet, relativeAngle, shotgunForce);
            relativeAngle -= interval;
        }
    }
}

class Blink : Skill
{
    private float blinkSpeed;
    private GameObject player;
    private PlayerDash dashEffect;
    private AudioSource blinkSound;

    public Blink(GameObject player, float blinkSpeed, PlayerDash dashEffect, AudioClip blinkSound)
    {
        this.player = player;
        this.blinkSpeed = blinkSpeed;
        this.dashEffect = dashEffect;
        this.blinkSound = new GameObject().AddComponent<AudioSource>();
        this.blinkSound.clip = blinkSound;
    }
    public void Activate()
    {
        float initVol = blinkSound.volume;
        blinkSound.volume = 0.3f;
        blinkSound.Play();
        //blinkSound.volume = initVol;
        dashEffect.TrailEffect(0.5f);
        player.GetComponent<Rigidbody2D>().AddForce(player.transform.up * blinkSpeed);
    }
}