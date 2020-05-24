using UnityEngine;

public class Fire : MonoBehaviour
{
    public int damage;
    public StateController controller;
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            controller.Damage(damage);
        }
    }
}
