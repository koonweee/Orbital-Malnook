﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; // Player's rigidbody.
    public float initialSpeed; // Player's speed of travel.
    public Camera cam; // Main camera.
    public bool isMoving; // Flag for when player is moving.
    public Animator animator;
    public Joystick moveStick, lookStick;
    public TileGenerator tileGenerator;
    public TrailersControl trailers;
    private Vector2 movement, look; // Movement and looking direction vectors.
    private float currentSpeed;

    void Start()
    {
        currentSpeed = initialSpeed;
        Vector2 spawn = new Vector2(tileGenerator.height / 2, tileGenerator.width / 2);
        SpawnPlayer(spawn);
    }

    void SpawnPlayer(Vector2 spawn)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawn, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Neutral" && collider.gameObject.tag != "Player")
            {
                SpawnPlayer(spawn + new Vector2(1, 0));
                return;
            }
        }
        transform.position = spawn;
    }
    void Update()
    {
        // For movement.
        movement.x = moveStick.Horizontal;
        movement.y = moveStick.Vertical;
        isMoving = movement != Vector2.zero;

        look.x = lookStick.Horizontal;
        look.y = lookStick.Vertical;

//        bool W = Input.GetKey("w");
//        bool A = Input.GetKey("a");
//        bool S = Input.GetKey("s");
//        bool D = Input.GetKey("d");
//
//        if (!W && !A && !S && !D) { movement = Vector2.zero; isMoving = false; }
//        else { isMoving = true; }
//
//        if (W) { movement.y = 1; }
//        if (S) { movement.y = -1; }
//        if (D) { movement.x = 1; }
//        if (A) { movement.x = -1; }
//
//        //For looking.
//        look = (Vector2) cam.ScreenToWorldPoint(Input.mousePosition) - rb.position;
//
        // Trailers.
        if (isMoving)
        {
            trailers.TrailerOn();
        }
        else
        {
            trailers.TrailerOff();
        }
    }

    void FixedUpdate()
    {
        // Move player.
        //rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        rb.AddForce(movement.normalized * currentSpeed * Time.deltaTime);

        // Rotate player.
        if (look != Vector2.zero)
        {
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;
            rb.MoveRotation(angle);
        }
    }

    public void SpeedUp(float percent)
    {
        animator.SetBool("SpedUp", true);
        currentSpeed = (1 + percent) * initialSpeed;
    }

    public void SlowDown(float percent)
    {
        animator.SetBool("Slowed", true);
        currentSpeed = (1 - percent) * initialSpeed;
    }

    public void ResetSpeed()
    {
        animator.SetBool("Slowed", false);
        animator.SetBool("SpedUp", false);
        currentSpeed = initialSpeed;
    }
}
