using UnityEngine;

public class ShallowWater : MonoBehaviour
{
    public float slowAmount;
    public StateController controller;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            controller.SlowDown(slowAmount);
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            controller.ResetSpeed();
        }
    }
}
