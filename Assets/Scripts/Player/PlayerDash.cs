using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private bool effectOn;
    private GameObject spriteHolder;
    public Sprite playerSprite;

    void Start()
    {
        effectOn = false;
        spriteHolder = new GameObject("Holder");
        SpriteRenderer renderer = spriteHolder.AddComponent<SpriteRenderer>();
        renderer.sprite = playerSprite;
        renderer.color = new Color(1f, 1f, 1f, 0.3f);
    }

    void Update()
    {
        if (effectOn)
        {
            GameObject trail = Instantiate(spriteHolder, transform.position, transform.rotation);
            trail.transform.localScale = new Vector3(0.6f, 0.6f, 1);
            Destroy(trail, 0.1f);
        }
    }

    public void TrailEffect(float duration)
    {
        effectOn = true;
        StartCoroutine(TrailWait(duration));
    }

    IEnumerator TrailWait(float duration)
    {
        yield return new WaitForSeconds(duration);
        effectOn = false;
    }
}
