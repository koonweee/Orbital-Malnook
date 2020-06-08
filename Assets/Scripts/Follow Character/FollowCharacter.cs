using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Transform character;
    public float yOffset;
    private Vector2 pos;

    void Update()
    {
        pos.x = character.position.x;
        pos.y = character.position.y + yOffset;
        transform.position = pos;
    }
}
