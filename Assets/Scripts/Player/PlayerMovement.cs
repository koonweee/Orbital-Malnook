using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; // Player's rigidbody.
    public float speed; // Player's speed of travel.
    public Camera cam; // Main camera.
    public bool isMoving; // Flag for when player is moving.
    private Vector2 movement, look; // Movement and looking direction vectors.
    void Update()
    {
        // For movement.
        //movement.x = moveStick.Horizontal;
        //movement.y = moveStick.Vertical;
        //look.x = lookStick.Horizontal;
        //look.y = lookStick.Vertical;

        bool W = Input.GetKey("w");
        bool A = Input.GetKey("a");
        bool S = Input.GetKey("s");
        bool D = Input.GetKey("d");

        if (!W && !A && !S && !D) { movement = Vector2.zero; isMoving = false; }
        else { isMoving = true; }

        if (W) { movement.y = 1; }
        if (S) { movement.y = -1; }
        if (D) { movement.x = 1; }
        if (A) { movement.x = -1; }

        // For looking.
        look = (Vector2) cam.ScreenToWorldPoint(Input.mousePosition) - rb.position;
    }

    void FixedUpdate()
    {
        // Move player.
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        // Rotate player.
        if (look != Vector2.zero)
        {
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;
            rb.MoveRotation(angle);
        }
    }
}
