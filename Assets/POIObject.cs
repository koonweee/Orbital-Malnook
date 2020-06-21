using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class POIObject : MonoBehaviour
{
    public Tile check;
    public Tilemap POImap;
    public StateController controller;
    public ParticleSystem explosion;
    public AudioSource sound;
    public int expAmount;
    private bool opened;

    void Start()
    {
        opened = false;
    }
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("BUMPED " + collider.gameObject.tag);
            if (collider.gameObject.tag == "Player" && !opened)
            {
                Seen();
                controller.GainExp(expAmount);
            } 
        }
    }

    public void Seen()
    {
        opened = true;
        explosion.Play();
        sound.Play();
        POImap.SetTile(Vector3Int.FloorToInt(transform.position), check);
    }

}
