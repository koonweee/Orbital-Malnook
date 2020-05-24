using UnityEngine;

public class ShortGrass : MonoBehaviour
{
    public float speedAmount;
    public StateController controller;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            controller.SpeedUp(speedAmount);
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
