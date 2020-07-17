using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    private Skill[] skills;
    private Skill skillA, skillB;
    public int skillAID, skillBID;
    public Sprite[] skillIcons;
    public Button skillAButton, skillBButton;
    public GameObject shotgunBullet;
    public PlayerShooting shooter;
    public PlayerDash dashEffect;
    public int shotgunSpread, spreadAngle, shotgunForce;
    public float blinkSpeed;
    public AudioClip blinkSound;
    public GameObject fireball;
    public int fireballForce;
    public GameObject iceball;
    public int iceballForce;

    public GameObject lightningball;
    public int lightningballForce;

    void Start()
    {
        // Init skills pool here.
        skills = new Skill[]{new Shotgun(shotgunBullet, shotgunSpread, spreadAngle, shooter, shotgunForce),
                             new Blink(gameObject, blinkSpeed, dashEffect, blinkSound),
                             new Fireball(fireball, shooter, fireballForce),
                             new Iceball(iceball, shooter, iceballForce),
                             new Lightningball(lightningball, shooter, lightningballForce)};

        // FOR TESTING, DEFAULT SKILLS.
        LockInSkill('A', 2);
        LockInSkill('B', 3);

        // Skills joystick buttons.
        skillAButton.onClick.AddListener(() => skillA.Activate());
        skillBButton.onClick.AddListener(() => skillB.Activate());
    }

    public void LockInSkill(char skill, int choice)
    {
        if (skill == 'A') 
        {
            if (skillBID == choice) return;
            skillAID = choice;
            skillA = skills[choice];
            skillAButton.GetComponent<Image>().sprite = skillIcons[choice];
        }
        if (skill == 'B') 
        {
            if (skillAID == choice) return;
            skillBID = choice;
            skillB = skills[choice];
            skillBButton.GetComponent<Image>().sprite = skillIcons[choice];
        }
    }

    void Update()
    {
        // FOR TESTING, REPLACE WITH JOYSTICK BUTTONS.
        //if (Input.GetMouseButtonDown(1)) skillA.Activate();
        //if (Input.GetMouseButtonDown(2)) skillB.Activate();
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
class Fireball : Skill
{
    private GameObject fireball;
    private int fireballForce;
    private PlayerShooting shooter;

    public Fireball(GameObject fireball, PlayerShooting shooter, int fireballForce)
    {
        this.fireball = fireball;
        this.shooter = shooter;
        this.fireballForce = fireballForce;
    }
    public void Activate()
    {
        shooter.Shoot(fireball, 0, fireballForce);
    }
}
class Iceball : Skill
{
    private GameObject iceball;
    private int iceballForce;
    private PlayerShooting shooter;

    public Iceball(GameObject iceball, PlayerShooting shooter, int iceballForce)
    {
        this.iceball = iceball;
        this.shooter = shooter;
        this.iceballForce = iceballForce;
    }
    public void Activate()
    {
        shooter.Shoot(iceball, 0, iceballForce);
    }
}

class Lightningball : Skill
{
    private GameObject lightningball;
    private int lightningballForce;
    private PlayerShooting shooter;

    public Lightningball(GameObject lightningball, PlayerShooting shooter, int lightningballForce)
    {
        this.lightningball = lightningball;
        this.shooter = shooter;
        this.lightningballForce = lightningballForce;
    }
    public void Activate()
    {
        shooter.Shoot(lightningball, 0, lightningballForce);
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