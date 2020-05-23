using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private Vector2 pos;

    void Update()
    {
        pos.x = player.position.x;
        pos.y = player.position.y + 0.7f;
        transform.position = pos;
    }
}
