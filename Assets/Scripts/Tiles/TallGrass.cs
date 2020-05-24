using UnityEngine;

public class TallGrass : MonoBehaviour
{
    public StateController controller;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            controller.Stealth();
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            controller.Unstealth();
        }
    }
}
